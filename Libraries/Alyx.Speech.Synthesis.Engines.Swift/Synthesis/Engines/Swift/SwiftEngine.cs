//
//  SwiftEngine.cs
//
//  Author:
//       beckermj <${AuthorEmail}>
//
//  Copyright (c) 2014 beckermj
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

namespace Alyx.Speech.Synthesis.Engines.Swift
{
	public class SwiftEngine : SynthesisEngine
	{
		public SwiftEngine()
		{
		}

		private string mvarSwiftApplicationPath = "swift"; // @"C:\Program Files (x86)\Cepstral\bin\swift.exe";
		public string SwiftApplicationPath { get { return mvarSwiftApplicationPath; } set { mvarSwiftApplicationPath = value; } }


		#region implemented abstract members of SynthesisEngine

		private static SynthesisEngineReference _ser = null;
		public override SynthesisEngineReference MakeReference()
		{
			if (_ser == null)
			{
				_ser = new SynthesisEngineReference(GetType());
			}
			return _ser;
		}

		private System.Threading.Thread _tSpeak = null;
		private void _tSpeak_ParameterizedThreadStart(object value)
		{
			string text = (value as string);

			System.Diagnostics.Process p = new System.Diagnostics.Process();

			Dictionary<string, string> paramz = new Dictionary<string, string>();
			paramz.Add("speech/rate", "220"); // default is 170

			StringBuilder sb = new StringBuilder();
			if (Voice != null)
			{
				sb.Append("-n \"" + Voice.Name + "\"");
			}
			foreach (KeyValuePair<string, string> kvp in paramz)
			{
				sb.Append("-p \"" + kvp.Key + "=" + kvp.Value + "\" ");
			}
			sb.Append(text);

			p.StartInfo = CreateProcessStartInfo(sb.ToString());

			try
			{
				p.Start();
				OnStateChanged(new SynthesisEngineStateChangedEventArgs(SynthesisEngineState.Speaking));
				p.WaitForExit();
				OnStateChanged(new SynthesisEngineStateChangedEventArgs(SynthesisEngineState.Ready));
			}
			catch (System.ComponentModel.Win32Exception ex)
			{
				if (ex.NativeErrorCode == 2)
				{
					// file not found
					throw new SpeechEngineNotFoundException(GetType().FullName, ex);
				}
				throw ex;
			}
		}

		private ProcessStartInfo CreateProcessStartInfo(string arguments)
		{
			ProcessStartInfo psi = new ProcessStartInfo(mvarSwiftApplicationPath, arguments);
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

		protected override Voice[] GetVoicesInternal()
		{
			Process p = new Process();
			p.StartInfo = CreateProcessStartInfo("--voices");
			p.Start();
			p.WaitForExit();

			string output = p.StandardOutput.ReadToEnd();
			output = output.Replace("\r\n", "\n");

			int index = 0;
			string[] lines = output.Split(new char[] { '\n' });

			List<List<string>> rows = new List<List<string>>();

			foreach (string line in lines)
			{
				if (line.Contains("|"))
				{
					string[] parts = line.Split(new char[] { '|' });
					if (index == 0)
					{

					}
					else if (index == 1)
					{

					}
					else
					{
						List<string> columns = new List<string>();
						foreach (string part in parts)
						{
							columns.Add(part.Trim());
						}
						rows.Add(columns);
					}
					index++;
				}
			}

			List<Voice> voices = new List<Voice>();
			foreach (List<string> columns in rows)
			{
				voices.Add(new Voice(columns[0]));
			}

			return voices.ToArray();
		}

		public override void SetVoiceInternal(Voice voice)
		{
			
		}

		#endregion
	}
}

