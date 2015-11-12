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
		public SynthesisEngineState State { get { return mvarState; } }

		private string mvarText = String.Empty;
		public string Text { get { return mvarText; } set { mvarText = value; } }

		public SynthesisEngineStateChangedEventArgs(SynthesisEngineState state, string text = null)
		{
			mvarState = state;
			if (text != null) mvarText = text;
		}
	}
}
