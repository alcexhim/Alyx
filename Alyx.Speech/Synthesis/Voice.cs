using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alyx.Speech.Synthesis
{
	public class Voice
	{
		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

		public Voice(string name)
		{
			mvarName = name;
		}
	}
}
