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

		private bool mvarEnabled = true;
		public bool Enabled { get { return mvarEnabled; } set { mvarEnabled = value; } }

		public Voice(string name, bool enabled = true)
		{
			mvarName = name;
			mvarEnabled = enabled;
		}
	}
}
