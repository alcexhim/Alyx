//
//  MindConnection.cs
//
//  Author:
//       beckermj <>
//
//  Copyright (c) 2016 beckermj
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

namespace Alyx.Thought
{
	public class MindConnection
	{
		public class MindConnectionCollection
			: System.Collections.ObjectModel.Collection<MindConnection>
		{
		}

		private IMindInput mvarInput = null;
		public IMindInput Input { get { return mvarInput; } set { mvarInput = value; } }

		private IMindOutput mvarOutput = null;
		public IMindOutput Output { get { return mvarOutput; } set { mvarOutput = value; } }

		public void Execute()
		{
			object param = mvarInput.GetValue ();
			mvarOutput.Execute (param);
		}

		public MindConnection(IMindInput input, IMindOutput output)
		{
			mvarInput = input;
			mvarOutput = output;
		}
	}
}

