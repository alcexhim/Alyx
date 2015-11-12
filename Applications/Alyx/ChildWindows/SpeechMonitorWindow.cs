using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Alyx.ChildWindows
{
	public partial class SpeechMonitorWindow : ChildWindow
	{
		public SpeechMonitorWindow()
		{
			InitializeComponent();
		}

		public void AddOutputLine(string text)
		{
			if (!IsHandleCreated) return;
			Invoke(new Action<string>(_AddOutputLine_impl), text);
		}

		private void _AddOutputLine_impl(string text)
		{
			chat.ReceiveMessage("Computer", text);
		}
	}
}
