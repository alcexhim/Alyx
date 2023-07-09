//
//  InternetConnectivityCheckerMindScript.cs
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
using Alyx.Thought;

namespace Alyx.ServerApplication.SampleMindScripts
{
	public class InternetConnectivityCheckerMindScript : MindScript
	{
		public override Guid ID => new Guid("{3024b3a4-1fe2-48f0-840d-568a7f00eb44}");
		public override int Interval => 3000;

		public static Guid Prop_HasInternetAccess { get; } = new Guid("{dd3a8525-eb6d-45d8-b6d9-a57eff3b3ccd}");

		protected override void OnInitialized(EventArgs e)
		{
			base.OnInitialized(e);
			Mind.Properties.SetName(Prop_HasInternetAccess, "Has Internet Access");
		}

		protected virtual void OnInternetAccessStatusChange(EventArgs e)
		{
			if (Mind.Properties.GetValue<bool>(Prop_HasInternetAccess))
			{
				Mind.SendInput("TextMindInput", "I have Internet Access!");
			}
			else
			{
				Mind.SendInput("TextMindInput", "I lost my Internet Access!");
			}
			Mind.InvokeEvent("OnInternetAccessStatusChanged", e);
		}

		protected override void ExecuteInternal()
		{
			base.ExecuteInternal();

			string[] testDomains = new string[] { "www.google.com" };
			foreach (string testDomain in testDomains)
			{
				try
				{
					System.Net.IPAddress[] ipad = System.Net.Dns.GetHostAddresses(testDomain);
					bool changed = Mind.Properties.SetValue<bool>(Prop_HasInternetAccess, true);
					if (changed)
					{
						OnInternetAccessStatusChange(EventArgs.Empty);
					}
					break;
				}
				catch
				{
					bool changed = Mind.Properties.SetValue<bool>(Prop_HasInternetAccess, false);
					if (changed)
					{
						OnInternetAccessStatusChange(EventArgs.Empty);
					}
				}
			}
		}
	}
}
