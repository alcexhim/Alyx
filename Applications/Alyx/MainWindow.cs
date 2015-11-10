using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Alyx
{
	public partial class MainWindow : Form
	{
		public MainWindow()
		{
			InitializeComponent();
			SwitchPanel("ApplicationsChat");
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

		private void chat_MessageSent(object sender, AwesomeControls.ChatBox.ChatBoxControl.ChatBoxMessageEventArgs e)
		{
			Program.Client.SendChatMessage(e.Message);
		}

		private void tv_AfterSelect(object sender, TreeViewEventArgs e)
		{
			SwitchPanel(e.Node.Name.Substring("node".Length));
		}

		private bool inhibitSwitchPanel = false;
		private void SwitchPanel(string name)
		{
			if (inhibitSwitchPanel) return;

			foreach (Control ctl in splitContainer1.Panel2.Controls)
			{
				if (ctl is Panel && ctl.Name.Substring(3).Equals(name))
				{
					ctl.Enabled = true;
					ctl.Visible = true;
				}
				else
				{
					ctl.Visible = false;
					ctl.Enabled = false;
				}
			}

			SwitchTreeNode(name);
		}

		private void SwitchTreeNode(string name, TreeNode parent = null)
		{
			if (parent == null)
			{
				inhibitSwitchPanel = true;
				foreach (TreeNode tn in tv.Nodes)
				{
					if (tn.Name == "node" + name)
					{
						tv.SelectedNode = tn;
						break;
					}
					else
					{
						SwitchTreeNode(name, tn);
					}
				}
				inhibitSwitchPanel = false;
			}
			else
			{
				foreach (TreeNode tn in parent.Nodes)
				{
					if (tn.Name == "node" + name)
					{
						tv.SelectedNode = tn;
						return;
					}
					else
					{
						SwitchTreeNode(name, tn);
					}
				}
			}
		}
	}
}
