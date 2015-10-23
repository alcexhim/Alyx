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

		[DebuggerNonUserCode()]
		protected override void SpeakInternal(string text)
		{
			System.Diagnostics.Process p = new System.Diagnostics.Process();
			
			Dictionary<string, string> paramz = new Dictionary<string, string>();
			paramz.Add("speech/rate", "220"); // default is 170
			
			StringBuilder sb = new StringBuilder();
			foreach (KeyValuePair<string, string> kvp in paramz)
			{
				sb.Append("-p \"" + kvp.Key + "=" + kvp.Value + "\" ");
			}
			sb.Append(text);
			p.StartInfo = new System.Diagnostics.ProcessStartInfo("swift", sb.ToString());
			p.StartInfo.CreateNoWindow = true;

			try
			{
				p.Start();
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

		protected override Voice[] GetVoicesInternal()
		{
			return new Voice[0];
		}

		public override void SetVoiceInternal(Voice voice)
		{
			
		}

		#endregion
	}
}

