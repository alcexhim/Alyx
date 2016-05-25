using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Alyx.ControlPanels.Configuration.Speech
{
	public partial class SpeechControlPanel : ControlPanel
	{
		public SpeechControlPanel()
		{
			InitializeComponent();
			if (Program.speaker != null) {
				Program.speaker.StateChanged += speaker_StateChanged;
			}
		}

		public override string[] Path
		{
			get
			{
				return new string[] { "Configuration", "Speech" };
			}
		}

		private void cmdSpeak_Click(object sender, EventArgs e)
		{
			cmdSpeak.Enabled = false;
			txtSpeak.ReadOnly = true;
			Program.speaker.Speak(txtSpeak.Text);
		}

		private void speaker_StateChanged(object sender, Alyx.Speech.Synthesis.SynthesisEngineStateChangedEventArgs e)
		{
			if (e.State == Alyx.Speech.Synthesis.SynthesisEngineState.Ready)
			{
				if (!IsHandleCreated) return;
				Invoke(new Action(delegate()
				{
					cmdSpeak.Enabled = true;
					txtSpeak.ReadOnly = false;
				}));
			}
			else if (e.State == Alyx.Speech.Synthesis.SynthesisEngineState.Speaking)
			{
				if (!IsHandleCreated) return;
				Invoke(new Action(delegate()
				{
					cmdSpeak.Enabled = false;
					txtSpeak.ReadOnly = true;
				}));
			}
		}

	}
}
