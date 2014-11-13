using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alyx.Speech.Synthesis
{
	public delegate void EngineStateChangedEventHandler(object sender, EngineStateChangedEventArgs e);
	public class EngineStateChangedEventArgs : EventArgs
	{
		private EngineState mvarState = EngineState.Ready;
		public EngineState State { get { return mvarState; } set { mvarState = value; } }

		public EngineStateChangedEventArgs(EngineState state)
		{
			mvarState = state;
		}
	}
}
