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
			cmdSpeak.Enabled = true;
			txtSpeak.ReadOnly = false;
		}

	}
}
