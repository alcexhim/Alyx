using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Alyx
{
	public partial class MainWindow : Form
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void cmdSpeak_Click(object sender, EventArgs e)
		{
			cmdSpeak.Enabled = false;
			txtSpeak.ReadOnly = true;
			Program.speaker.Speak(txtSpeak.Text);
			cmdSpeak.Enabled = true;
			txtSpeak.ReadOnly = false;
		}

		private void mnuFileClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void mnuFileExit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}
	}
}
