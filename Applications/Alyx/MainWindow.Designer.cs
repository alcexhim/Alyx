namespace Alyx
{
	partial class MainWindow
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Chat");
			System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Applications", new System.Windows.Forms.TreeNode[] {
            treeNode1});
			System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Languages");
			System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Speech");
			System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Configuration", new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode4});
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
			this.cbContainer1 = new AwesomeControls.CommandBars.CBContainer();
			this.cbPanel1 = new AwesomeControls.CommandBars.CBPanel();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.tv = new System.Windows.Forms.TreeView();
			this.pnlApplicationsChat = new System.Windows.Forms.Panel();
			this.chat = new AwesomeControls.ChatBox.ChatBoxControl();
			this.pnlConfigurationSpeech = new System.Windows.Forms.Panel();
			this.cmdSpeak = new System.Windows.Forms.Button();
			this.lblMaintenanceSpeak = new System.Windows.Forms.Label();
			this.txtSpeak = new System.Windows.Forms.TextBox();
			this.cbMenuBar1 = new AwesomeControls.CommandBars.CBMenuBar();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFileClose = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.customizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.contentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.indexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pnlConfigurationLanguages = new System.Windows.Forms.Panel();
			this.listView1 = new System.Windows.Forms.ListView();
			this.chLanguage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.cbContainer1.ContentPanel.SuspendLayout();
			this.cbContainer1.TopToolStripPanel.SuspendLayout();
			this.cbContainer1.SuspendLayout();
			this.cbPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.pnlApplicationsChat.SuspendLayout();
			this.pnlConfigurationSpeech.SuspendLayout();
			this.cbMenuBar1.SuspendLayout();
			this.pnlConfigurationLanguages.SuspendLayout();
			this.SuspendLayout();
			// 
			// cbContainer1
			// 
			// 
			// cbContainer1.ContentPanel
			// 
			this.cbContainer1.ContentPanel.Controls.Add(this.cbPanel1);
			this.cbContainer1.ContentPanel.Size = new System.Drawing.Size(556, 325);
			this.cbContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cbContainer1.Location = new System.Drawing.Point(0, 0);
			this.cbContainer1.Name = "cbContainer1";
			this.cbContainer1.Size = new System.Drawing.Size(556, 350);
			this.cbContainer1.TabIndex = 0;
			this.cbContainer1.Text = "cbContainer1";
			// 
			// cbContainer1.TopToolStripPanel
			// 
			this.cbContainer1.TopToolStripPanel.Controls.Add(this.cbMenuBar1);
			this.cbContainer1.UseCommandManager = false;
			// 
			// cbPanel1
			// 
			this.cbPanel1.Controls.Add(this.splitContainer1);
			this.cbPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cbPanel1.Location = new System.Drawing.Point(0, 0);
			this.cbPanel1.Name = "cbPanel1";
			this.cbPanel1.Size = new System.Drawing.Size(556, 325);
			this.cbPanel1.TabIndex = 0;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.tv);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.pnlConfigurationLanguages);
			this.splitContainer1.Panel2.Controls.Add(this.pnlApplicationsChat);
			this.splitContainer1.Panel2.Controls.Add(this.pnlConfigurationSpeech);
			this.splitContainer1.Size = new System.Drawing.Size(556, 325);
			this.splitContainer1.SplitterDistance = 185;
			this.splitContainer1.TabIndex = 1;
			// 
			// tv
			// 
			this.tv.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tv.HideSelection = false;
			this.tv.Location = new System.Drawing.Point(0, 0);
			this.tv.Name = "tv";
			treeNode1.Name = "nodeApplicationsChat";
			treeNode1.Text = "Chat";
			treeNode2.Name = "nodeApplications";
			treeNode2.Text = "Applications";
			treeNode3.Name = "nodeConfigurationLanguages";
			treeNode3.Text = "Languages";
			treeNode4.Name = "nodeConfigurationSpeech";
			treeNode4.Text = "Speech";
			treeNode5.Name = "nodeConfiguration";
			treeNode5.Text = "Configuration";
			this.tv.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode5});
			this.tv.Size = new System.Drawing.Size(185, 325);
			this.tv.TabIndex = 0;
			this.tv.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tv_AfterSelect);
			// 
			// pnlApplicationsChat
			// 
			this.pnlApplicationsChat.Controls.Add(this.chat);
			this.pnlApplicationsChat.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlApplicationsChat.Location = new System.Drawing.Point(0, 0);
			this.pnlApplicationsChat.Name = "pnlApplicationsChat";
			this.pnlApplicationsChat.Size = new System.Drawing.Size(367, 325);
			this.pnlApplicationsChat.TabIndex = 0;
			// 
			// chat
			// 
			this.chat.Dock = System.Windows.Forms.DockStyle.Fill;
			this.chat.Font = new System.Drawing.Font("Arial", 10F);
			this.chat.Location = new System.Drawing.Point(0, 0);
			this.chat.Margin = new System.Windows.Forms.Padding(4);
			this.chat.Name = "chat";
			this.chat.Size = new System.Drawing.Size(367, 325);
			this.chat.TabIndex = 0;
			this.chat.MessageSent += new AwesomeControls.ChatBox.ChatBoxControl.ChatBoxMessageEventHandler(this.chat_MessageSent);
			// 
			// pnlConfigurationSpeech
			// 
			this.pnlConfigurationSpeech.Controls.Add(this.cmdSpeak);
			this.pnlConfigurationSpeech.Controls.Add(this.lblMaintenanceSpeak);
			this.pnlConfigurationSpeech.Controls.Add(this.txtSpeak);
			this.pnlConfigurationSpeech.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlConfigurationSpeech.Location = new System.Drawing.Point(0, 0);
			this.pnlConfigurationSpeech.Name = "pnlConfigurationSpeech";
			this.pnlConfigurationSpeech.Size = new System.Drawing.Size(367, 325);
			this.pnlConfigurationSpeech.TabIndex = 2;
			// 
			// cmdSpeak
			// 
			this.cmdSpeak.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdSpeak.Location = new System.Drawing.Point(280, 18);
			this.cmdSpeak.Name = "cmdSpeak";
			this.cmdSpeak.Size = new System.Drawing.Size(75, 23);
			this.cmdSpeak.TabIndex = 2;
			this.cmdSpeak.Text = "&Speak";
			this.cmdSpeak.UseVisualStyleBackColor = true;
			this.cmdSpeak.Click += new System.EventHandler(this.cmdSpeak_Click);
			// 
			// lblMaintenanceSpeak
			// 
			this.lblMaintenanceSpeak.AutoSize = true;
			this.lblMaintenanceSpeak.Location = new System.Drawing.Point(13, 23);
			this.lblMaintenanceSpeak.Name = "lblMaintenanceSpeak";
			this.lblMaintenanceSpeak.Size = new System.Drawing.Size(80, 13);
			this.lblMaintenanceSpeak.TabIndex = 0;
			this.lblMaintenanceSpeak.Text = "Speak this text:";
			// 
			// txtSpeak
			// 
			this.txtSpeak.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtSpeak.Location = new System.Drawing.Point(99, 20);
			this.txtSpeak.Name = "txtSpeak";
			this.txtSpeak.Size = new System.Drawing.Size(175, 20);
			this.txtSpeak.TabIndex = 1;
			// 
			// cbMenuBar1
			// 
			this.cbMenuBar1.Dock = System.Windows.Forms.DockStyle.None;
			this.cbMenuBar1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
			this.cbMenuBar1.Location = new System.Drawing.Point(0, 0);
			this.cbMenuBar1.Name = "cbMenuBar1";
			this.cbMenuBar1.Size = new System.Drawing.Size(556, 25);
			this.cbMenuBar1.TabIndex = 0;
			this.cbMenuBar1.Text = "cbMenuBar1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileClose,
            this.toolStripSeparator2,
            this.mnuFileExit});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 21);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// mnuFileClose
			// 
			this.mnuFileClose.Name = "mnuFileClose";
			this.mnuFileClose.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
			this.mnuFileClose.Size = new System.Drawing.Size(145, 22);
			this.mnuFileClose.Text = "&Close";
			this.mnuFileClose.Click += new System.EventHandler(this.mnuFileClose_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(142, 6);
			// 
			// mnuFileExit
			// 
			this.mnuFileExit.Name = "mnuFileExit";
			this.mnuFileExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
			this.mnuFileExit.Size = new System.Drawing.Size(145, 22);
			this.mnuFileExit.Text = "E&xit";
			this.mnuFileExit.Click += new System.EventHandler(this.mnuFileExit_Click);
			// 
			// editToolStripMenuItem
			// 
			this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.toolStripSeparator3,
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripSeparator4,
            this.selectAllToolStripMenuItem});
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 21);
			this.editToolStripMenuItem.Text = "&Edit";
			// 
			// undoToolStripMenuItem
			// 
			this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
			this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
			this.undoToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.undoToolStripMenuItem.Text = "&Undo";
			// 
			// redoToolStripMenuItem
			// 
			this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
			this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
			this.redoToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.redoToolStripMenuItem.Text = "&Redo";
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(141, 6);
			// 
			// cutToolStripMenuItem
			// 
			this.cutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cutToolStripMenuItem.Image")));
			this.cutToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
			this.cutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
			this.cutToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.cutToolStripMenuItem.Text = "Cu&t";
			// 
			// copyToolStripMenuItem
			// 
			this.copyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyToolStripMenuItem.Image")));
			this.copyToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.copyToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.copyToolStripMenuItem.Text = "&Copy";
			// 
			// pasteToolStripMenuItem
			// 
			this.pasteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pasteToolStripMenuItem.Image")));
			this.pasteToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
			this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
			this.pasteToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.pasteToolStripMenuItem.Text = "&Paste";
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(141, 6);
			// 
			// selectAllToolStripMenuItem
			// 
			this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
			this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.selectAllToolStripMenuItem.Text = "Select &All";
			// 
			// toolsToolStripMenuItem
			// 
			this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.customizeToolStripMenuItem,
            this.optionsToolStripMenuItem});
			this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
			this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 21);
			this.toolsToolStripMenuItem.Text = "&Tools";
			// 
			// customizeToolStripMenuItem
			// 
			this.customizeToolStripMenuItem.Name = "customizeToolStripMenuItem";
			this.customizeToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
			this.customizeToolStripMenuItem.Text = "&Customize";
			// 
			// optionsToolStripMenuItem
			// 
			this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
			this.optionsToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
			this.optionsToolStripMenuItem.Text = "&Options";
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contentsToolStripMenuItem,
            this.indexToolStripMenuItem,
            this.searchToolStripMenuItem,
            this.toolStripSeparator5,
            this.aboutToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
			this.helpToolStripMenuItem.Text = "&Help";
			// 
			// contentsToolStripMenuItem
			// 
			this.contentsToolStripMenuItem.Name = "contentsToolStripMenuItem";
			this.contentsToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
			this.contentsToolStripMenuItem.Text = "&Contents";
			// 
			// indexToolStripMenuItem
			// 
			this.indexToolStripMenuItem.Name = "indexToolStripMenuItem";
			this.indexToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
			this.indexToolStripMenuItem.Text = "&Index";
			// 
			// searchToolStripMenuItem
			// 
			this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
			this.searchToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
			this.searchToolStripMenuItem.Text = "&Search";
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(119, 6);
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
			this.aboutToolStripMenuItem.Text = "&About...";
			// 
			// pnlConfigurationLanguages
			// 
			this.pnlConfigurationLanguages.Controls.Add(this.listView1);
			this.pnlConfigurationLanguages.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlConfigurationLanguages.Location = new System.Drawing.Point(0, 0);
			this.pnlConfigurationLanguages.Name = "pnlConfigurationLanguages";
			this.pnlConfigurationLanguages.Size = new System.Drawing.Size(367, 325);
			this.pnlConfigurationLanguages.TabIndex = 3;
			// 
			// listView1
			// 
			this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chLanguage});
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.HideSelection = false;
			this.listView1.Location = new System.Drawing.Point(3, 3);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(361, 319);
			this.listView1.TabIndex = 0;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.Details;
			// 
			// chLanguage
			// 
			this.chLanguage.Text = "Language";
			this.chLanguage.Width = 268;
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(556, 350);
			this.Controls.Add(this.cbContainer1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.cbMenuBar1;
			this.Name = "MainWindow";
			this.Text = "A.L.Y.X. Control Center";
			this.cbContainer1.ContentPanel.ResumeLayout(false);
			this.cbContainer1.TopToolStripPanel.ResumeLayout(false);
			this.cbContainer1.TopToolStripPanel.PerformLayout();
			this.cbContainer1.ResumeLayout(false);
			this.cbContainer1.PerformLayout();
			this.cbPanel1.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.pnlApplicationsChat.ResumeLayout(false);
			this.pnlConfigurationSpeech.ResumeLayout(false);
			this.pnlConfigurationSpeech.PerformLayout();
			this.cbMenuBar1.ResumeLayout(false);
			this.cbMenuBar1.PerformLayout();
			this.pnlConfigurationLanguages.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private AwesomeControls.CommandBars.CBContainer cbContainer1;
		private AwesomeControls.CommandBars.CBPanel cbPanel1;
		private AwesomeControls.CommandBars.CBMenuBar cbMenuBar1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem mnuFileClose;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
		private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem customizeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem contentsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem indexToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.Button cmdSpeak;
		private System.Windows.Forms.TextBox txtSpeak;
		private System.Windows.Forms.Label lblMaintenanceSpeak;
		private AwesomeControls.ChatBox.ChatBoxControl chat;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.Panel pnlApplicationsChat;
		private System.Windows.Forms.Panel pnlConfigurationSpeech;
		private System.Windows.Forms.TreeView tv;
		private System.Windows.Forms.Panel pnlConfigurationLanguages;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader chLanguage;
	}
}