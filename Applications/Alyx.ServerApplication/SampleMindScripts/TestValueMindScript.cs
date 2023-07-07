//
//  TestValueMindScript.cs
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
	public class TestValueMindScript : MindScript
	{
		public static Guid SR_Entropy { get; } = new Guid("{5f9ea22d-f927-427e-bb85-02b3319da67c}");

		private Random rnd = new Random();
		protected override void OnInitialized(EventArgs e)
		{
			base.OnInitialized(e);
			Mind.SetPropertyName(SR_Entropy, "entropy");
		}
		public override int Interval => 100;
		protected override void ExecuteInternal()
		{
			base.ExecuteInternal();

			Mind.SetPropertyValue(SR_Entropy, rnd.NextDouble());
		}
	}
}
