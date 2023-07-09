//
//  SensorsMindScript.cs
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
using Alyx.Thought.MindScripts;

namespace Alyx.ServerApplication.SampleMindScripts
{
	/// <summary>
	/// Runs a <see cref="ClockMindInput" /> that takes sensor readings from the "sensors" program every 30
	/// seconds.
	/// </summary>
	public class SensorsMindScript : ClockMindScript
	{
		public override Guid ID => new Guid("{a4a6c71f-654e-4dcc-a328-dab57e7cf5dc}");

		private class SENS_Adapter
		{
			private string mvarAdapterName = String.Empty;
			public string AdapterName { get { return mvarAdapterName; } set { mvarAdapterName = value; } }
			public List<SENS_Property> Properties { get; } = new List<SENS_Property>();
		}
		private class SENS_Property
		{
			private string mvarPropertyName = String.Empty;
			public string PropertyName { get { return mvarPropertyName; } }

			private string mvarValue = null;
			public string Value { get { return mvarValue; } }

			public static SENS_Property Parse(string line)
			{
				string[] propparts = line.Split(new char[] { ':' });
				string propName = propparts[0].Trim();
				string propVal = null;
				if (propparts.Length > 1)
				{
					propVal = propparts[1].Trim();
				}

				if (propVal.Contains("(") && propVal.Contains(")"))
				{
					string realval = propVal.Substring(0, propVal.IndexOf('(')).Trim();

					int indexOfParen = propVal.IndexOf('(') + 1;
					string comment = propVal.Substring(indexOfParen, propVal.Length - indexOfParen - 1);

					propVal = realval;
				}

				SENS_Property prop = new SENS_Property(propName, propVal);
				return prop;
			}

			public SENS_Property(string propertyName, string value)
			{
				mvarPropertyName = propertyName;
				mvarValue = value;
			}
		}

		private void GetSensorReadings()
		{
			System.Diagnostics.Process p = new System.Diagnostics.Process();
			p.StartInfo = new System.Diagnostics.ProcessStartInfo("sensors");
			p.StartInfo.RedirectStandardOutput = true;
			p.StartInfo.CreateNoWindow = true;
			p.StartInfo.UseShellExecute = false;

			try
			{
				p.Start();
				p.WaitForExit();
			}
			catch (Exception ex)
			{
				ConsoleColor fg = Console.ForegroundColor;
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine(ex.GetType().Name + ": " + ex.Message);
				Console.ForegroundColor = fg;
			}

			string s = p.StandardOutput.ReadToEnd();
			string[] lines = s.Split(new char[] { '\n' });

			List<SENS_Adapter> listAdapters = new List<SENS_Adapter>();

			{
				SENS_Adapter adapter = null;
				foreach (string line in lines)
				{
					if (String.IsNullOrEmpty(line))
					{
						if (adapter != null)
							listAdapters.Add(adapter);

						adapter = null;
						continue;
					}
					if (adapter == null)
					{
						adapter = new SENS_Adapter();
						adapter.AdapterName = line;
					}
					else
					{
						adapter.Properties.Add(SENS_Property.Parse(line));
					}
				}
			}
			/*
			foreach (SENS_Adapter adapter in listAdapters)
			{
				Console.WriteLine (adapter.AdapterName);
				foreach (SENS_Property prop in adapter.Properties) {
					Console.Write ("    ");
					Console.Write (prop.PropertyName);
					Console.Write (": ");
					Console.WriteLine (prop.Value);
				}
			}
			*/
		}

		protected override void OnInitialized(EventArgs e)
		{
			base.OnInitialized(e);
			GetSensorReadings();
		}

		private int sec = 1;
		protected override void OnSecondChanged(EventArgs e)
		{
			base.OnSecondChanged(e);

			sec++;
			if (sec == 30)
			{

				GetSensorReadings();
				sec = 1;
			}
		}
	}

}
