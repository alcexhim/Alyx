//
//  ExternalSoftwareSynthesisEngine.cs
//
//  Author:
//       beckermj <>
//
//  Copyright (c) 2023 ${CopyrightHolder}
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Alyx.Speech.Synthesis.Engines
{
    public abstract class ExternalSoftwareSynthesisEngine : SynthesisEngine
    {
        protected abstract string ApplicationPath { get; }
        protected virtual bool RequirePulseAudioDSP { get; } = false;

        protected virtual string GetCommandLineArguments(string text)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\"");
            sb.Append(text.Replace("\"", "\\\""));
            sb.Append("\"");
            return sb.ToString();
        }

        protected override bool IsSupportedInternal()
        {
            Process p = new Process();
            p.StartInfo = CreateProcessStartInfo("", false);
            try
            {
                p.Start();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        private System.Threading.Thread _tSpeak = null;
        private void _tSpeak_ParameterizedThreadStart(object value)
        {
            string text = (value as string);

            Process p = new Process();

            string args = GetCommandLineArguments(text);
            p.StartInfo = CreateProcessStartInfo(args, true);

            try
            {
                p.Start();
                OnStateChanged(new SynthesisEngineStateChangedEventArgs(SynthesisEngineState.Speaking, text));
                p.WaitForExit();
                OnStateChanged(new SynthesisEngineStateChangedEventArgs(SynthesisEngineState.Ready));
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                if (ex.NativeErrorCode == 2)
                {
                    // file not found
                    if (SuppressSpeechEngineNotFound)
                        return;

                    throw new SpeechEngineNotFoundException(GetType().FullName, ex);
                }
                throw ex;
            }
        }

        protected ProcessStartInfo CreateProcessStartInfo(string arguments, bool audio)
        {
            string app = ApplicationPath;
            string args = arguments;

            if (audio && RequirePulseAudioDSP)
            {
                app = "padsp";
                args = String.Format("{0} {1}", ApplicationPath, args);
            }

            ProcessStartInfo psi = new ProcessStartInfo(app, args);
            psi.CreateNoWindow = true;
            psi.UseShellExecute = false;
            psi.RedirectStandardError = true;
            psi.RedirectStandardInput = true;
            psi.RedirectStandardOutput = true;
            return psi;
        }

        [DebuggerNonUserCode()]
        protected override void SpeakInternal(string text)
        {
            if (_tSpeak != null && _tSpeak.IsAlive) _tSpeak.Abort();
            _tSpeak = new System.Threading.Thread(_tSpeak_ParameterizedThreadStart);

            _tSpeak.Name = "Alyx Speech Swift Engine Thread";
            _tSpeak.Start(text);
        }


    }
}
