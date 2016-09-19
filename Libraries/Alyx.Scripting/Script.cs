//
//  MyClass.cs
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

namespace Alyx.Scripting
{
	public class Script
	{
		public class ScriptCollection
			: System.Collections.ObjectModel.Collection<Script>
		{
		}

		public Script()
		{
			mvarID = Guid.NewGuid ();
		}
		public Script(Guid id)
		{
			mvarID = id;
		}

		private Guid mvarID = Guid.Empty;
		public Guid ID { get { return mvarID; } }

		private Guid mvarInheritsScriptID = Guid.Empty;
		public Guid InheritsScriptID { get { return mvarInheritsScriptID; } set { mvarInheritsScriptID = value; } }

		private ScriptParameter.ScriptParameterCollection mvarParameters = new ScriptParameter.ScriptParameterCollection();
		public ScriptParameter.ScriptParameterCollection Parameters { get { return mvarParameters; } }
	}
}

