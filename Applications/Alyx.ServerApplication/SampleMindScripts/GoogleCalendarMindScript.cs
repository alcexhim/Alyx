//
//  GoogleCalendarMindScript.cs
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
	public class GoogleCalendarMindScript : MindScript
	{
		public static Guid GooCal_ClientID { get; } = new Guid("{73590738-bcec-4947-8286-c7763b7e6e06}");
		public static Guid GooCal_ClientSecret { get; } = new Guid("{a0f8f3d9-ef59-4a03-ace3-3d801fadcd75}");
		public static Guid GooCal_ApiKey { get; } = new Guid("{de35a9ef-319f-4768-9e8d-375bba8d590b}");

		private string clientID, clientSecret, apiKey;

		private static string json = "{ \"installed\": { \"client_id\": \"$(CLIENT_ID)\", \"client_secret\": \"$(CLIENT_SECRET)\", \"redirect_uris\": [\"http://localhost\", \"urn:ietf:wg:oauth:2.0:oob\"], \"auth_uri\": \"https://accounts.google.com/o/oauth2/auth\", \"token_uri\": \"https://accounts.google.com/o/oauth2/token\" } }";

		public override Guid ID => new Guid("{15015ea5-a3ca-4154-9141-d57689fc8350}");

		protected override void OnInitialized(EventArgs e)
		{
			base.OnInitialized(e);

			// FIXME: should we use SettingsProviders for more organization?
			// how can we simplify the API for simply getting/setting settings
			// while simultaneously providing a rich UI experience with title
			// and other setting metadata provided by SettingsProviders?

			// right now the Application does the same thing, it uses its own
			// implementation of GetSettingValue/SetSettingValue in addition
			// to the SettingsProviders which display the UI...

			// this doesn't seem ideal for numerous reasons, most of which is
			// involving the synchronization of the SettingsProviders with the
			// actual data stored by GetSettingValue/SetSettingValue...

			clientID = this.Settings.GetValue<string>(GooCal_ClientID);
			clientSecret = this.Settings.GetValue<string>(GooCal_ClientSecret);
			apiKey = this.Settings.GetValue<string>(GooCal_ApiKey);
		}
	}
}
