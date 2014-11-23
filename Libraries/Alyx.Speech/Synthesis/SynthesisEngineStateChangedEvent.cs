using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alyx.Speech.Synthesis
{
	public delegate void SynthesisEngineStateChangedEventHandler(object sender, SynthesisEngineStateChangedEventArgs e);
	public class SynthesisEngineStateChangedEventArgs : EventArgs
	{
		private SynthesisEngineState mvarState = SynthesisEngineState.Ready;
		public SynthesisEngineState State { get { return mvarState; } set { mvarState = value; } }

		public SynthesisEngineStateChangedEventArgs(SynthesisEngineState state)
		{
			mvarState = state;
		}
	}
}
