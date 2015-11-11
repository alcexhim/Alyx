using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Alyx.ControlPanels.Applications.Chat
{
	public partial class ChatControlPanel : ControlPanel
	{
		public ChatControlPanel()
		{
			InitializeComponent();
		}

		public override string[] Path
		{
			get
			{
				return new string[] { "Applications", "Chat" };
			}
		}

		private void chat_MessageSent(object sender, AwesomeControls.ChatBox.ChatBoxControl.ChatBoxMessageEventArgs e)
		{
			Program.Client.SendChatMessage(e.Message);
		}
	}
}
