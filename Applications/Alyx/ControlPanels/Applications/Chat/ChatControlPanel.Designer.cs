namespace Alyx.ControlPanels.Applications.Chat
{
	partial class ChatControlPanel
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.chat = new AwesomeControls.ChatBox.ChatBoxControl();
			this.SuspendLayout();
			// 
			// chat
			// 
			this.chat.Dock = System.Windows.Forms.DockStyle.Fill;
			this.chat.Font = new System.Drawing.Font("Arial", 10F);
			this.chat.Location = new System.Drawing.Point(0, 0);
			this.chat.Margin = new System.Windows.Forms.Padding(4);
			this.chat.Name = "chat";
			this.chat.Size = new System.Drawing.Size(280, 222);
			this.chat.TabIndex = 1;
			this.chat.MessageSent += new AwesomeControls.ChatBox.ChatBoxControl.ChatBoxMessageEventHandler(this.chat_MessageSent);
			// 
			// ChatControlPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.chat);
			this.Name = "ChatControlPanel";
			this.Size = new System.Drawing.Size(280, 222);
			this.ResumeLayout(false);

		}

		#endregion

		private AwesomeControls.ChatBox.ChatBoxControl chat;
	}
}
