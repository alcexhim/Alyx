//
//  SleepyMindScript.cs
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
using Alyx.Thought.MindScripts;

namespace Alyx.ServerApplication.SampleMindScripts
{
	/// <summary>
	/// This script runs every hour and checks to see if hour is 01, if it is it tells us we should go to bed.
	/// DISCLAIMER: I wrote this when I was tired and wanted to go to bed...
	/// </summary>
	public class SleepyMindScript : ClockMindScript
	{
		public override Guid ID => new Guid("{2a52ef28-d4a0-4fef-a211-b41eda898005}");
		protected override void OnHourChanged(EventArgs e)
		{
			base.OnHourChanged(e);

			if (DateTime.Now.Hour == 01)
			{
				// FIXME: how do we tell the mind to "speak" ?
				// this script has an output
				// output write sentence "It's getting late. You should probably get ready to go to sleep."
				// this script's output is connected to input of `text to speech` node
				// `text to speech` output is connected to speaker
				Mind.SendInput("TextMindInput", "It's getting late. You should probably get ready to go to sleep.");
			}
		}
	}
}
