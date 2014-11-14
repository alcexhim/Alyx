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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
			this.cbContainer1 = new AwesomeControls.CommandBars.CBContainer();
			this.cbPanel1 = new AwesomeControls.CommandBars.CBPanel();
			this.tbs = new System.Windows.Forms.TabControl();
			this.tabChat = new System.Windows.Forms.TabPage();
			this.scChat = new System.Windows.Forms.SplitContainer();
			this.txtOutput = new System.Windows.Forms.RichTextBox();
			this.txtInput = new System.Windows.Forms.RichTextBox();
			this.tabMaintenance = new System.Windows.Forms.TabPage();
			this.cmdSpeak = new System.Windows.Forms.Button();
			this.txtSpeak = new System.Windows.Forms.TextBox();
			this.lblMaintenanceSpeak = new System.Windows.Forms.Label();
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
			this.cbContainer1.ContentPanel.SuspendLayout();
			this.cbContainer1.TopToolStripPanel.SuspendLayout();
			this.cbContainer1.SuspendLayout();
			this.cbPanel1.SuspendLayout();
			this.tbs.SuspendLayout();
			this.tabChat.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.scChat)).BeginInit();
			this.scChat.Panel1.SuspendLayout();
			this.scChat.Panel2.SuspendLayout();
			this.scChat.SuspendLayout();
			this.tabMaintenance.SuspendLayout();
			this.cbMenuBar1.SuspendLayout();
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
			this.cbPanel1.Controls.Add(this.tbs);
			this.cbPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cbPanel1.Location = new System.Drawing.Point(0, 0);
			this.cbPanel1.Name = "cbPanel1";
			this.cbPanel1.Size = new System.Drawing.Size(556, 325);
			this.cbPanel1.TabIndex = 0;
			// 
			// tbs
			// 
			this.tbs.Controls.Add(this.tabChat);
			this.tbs.Controls.Add(this.tabMaintenance);
			this.tbs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbs.Location = new System.Drawing.Point(0, 0);
			this.tbs.Name = "tbs";
			this.tbs.SelectedIndex = 0;
			this.tbs.Size = new System.Drawing.Size(556, 325);
			this.tbs.TabIndex = 0;
			// 
			// tabChat
			// 
			this.tabChat.Controls.Add(this.scChat);
			this.tabChat.Location = new System.Drawing.Point(4, 22);
			this.tabChat.Name = "tabChat";
			this.tabChat.Padding = new System.Windows.Forms.Padding(3);
			this.tabChat.Size = new System.Drawing.Size(548, 299);
			this.tabChat.TabIndex = 0;
			this.tabChat.Text = "Chat";
			this.tabChat.UseVisualStyleBackColor = true;
			// 
			// scChat
			// 
			this.scChat.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scChat.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.scChat.Location = new System.Drawing.Point(3, 3);
			this.scChat.Name = "scChat";
			this.scChat.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// scChat.Panel1
			// 
			this.scChat.Panel1.Controls.Add(this.txtOutput);
			// 
			// scChat.Panel2
			// 
			this.scChat.Panel2.Controls.Add(this.txtInput);
			this.scChat.Size = new System.Drawing.Size(542, 293);
			this.scChat.SplitterDistance = 185;
			this.scChat.TabIndex = 0;
			// 
			// txtOutput
			// 
			this.txtOutput.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtOutput.HideSelection = false;
			this.txtOutput.Location = new System.Drawing.Point(0, 0);
			this.txtOutput.Name = "txtOutput";
			this.txtOutput.ReadOnly = true;
			this.txtOutput.Size = new System.Drawing.Size(542, 185);
			this.txtOutput.TabIndex = 0;
			this.txtOutput.Text = "";
			// 
			// txtInput
			// 
			this.txtInput.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtInput.HideSelection = false;
			this.txtInput.Location = new System.Drawing.Point(0, 0);
			this.txtInput.Name = "txtInput";
			this.txtInput.Size = new System.Drawing.Size(542, 104);
			this.txtInput.TabIndex = 0;
			this.txtInput.Text = "";
			// 
			// tabMaintenance
			// 
			this.tabMaintenance.Controls.Add(this.cmdSpeak);
			this.tabMaintenance.Controls.Add(this.txtSpeak);
			this.tabMaintenance.Controls.Add(this.lblMaintenanceSpeak);
			this.tabMaintenance.Location = new System.Drawing.Point(4, 22);
			this.tabMaintenance.Name = "tabMaintenance";
			this.tabMaintenance.Padding = new System.Windows.Forms.Padding(3);
			this.tabMaintenance.Size = new System.Drawing.Size(548, 299);
			this.tabMaintenance.TabIndex = 1;
			this.tabMaintenance.Text = "Maintenance";
			this.tabMaintenance.UseVisualStyleBackColor = true;
			// 
			// cmdSpeak
			// 
			this.cmdSpeak.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdSpeak.Location = new System.Drawing.Point(465, 6);
			this.cmdSpeak.Name = "cmdSpeak";
			this.cmdSpeak.Size = new System.Drawing.Size(75, 23);
			this.cmdSpeak.TabIndex = 2;
			this.cmdSpeak.Text = "&Speak";
			this.cmdSpeak.UseVisualStyleBackColor = true;
			this.cmdSpeak.Click += new System.EventHandler(this.cmdSpeak_Click);
			// 
			// txtSpeak
			// 
			this.txtSpeak.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtSpeak.Location = new System.Drawing.Point(95, 8);
			this.txtSpeak.Name = "txtSpeak";
			this.txtSpeak.Size = new System.Drawing.Size(364, 20);
			this.txtSpeak.TabIndex = 1;
			// 
			// lblMaintenanceSpeak
			// 
			this.lblMaintenanceSpeak.AutoSize = true;
			this.lblMaintenanceSpeak.Location = new System.Drawing.Point(9, 11);
			this.lblMaintenanceSpeak.Name = "lblMaintenanceSpeak";
			this.lblMaintenanceSpeak.Size = new System.Drawing.Size(80, 13);
			this.lblMaintenanceSpeak.TabIndex = 0;
			this.lblMaintenanceSpeak.Text = "Speak this text:";
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
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 21);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// mnuFileClose
			// 
			this.mnuFileClose.Name = "mnuFileClose";
			this.mnuFileClose.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
			this.mnuFileClose.Size = new System.Drawing.Size(140, 22);
			this.mnuFileClose.Text = "&Close";
			this.mnuFileClose.Click += new System.EventHandler(this.mnuFileClose_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(137, 6);
			// 
			// mnuFileExit
			// 
			this.mnuFileExit.Name = "mnuFileExit";
			this.mnuFileExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
			this.mnuFileExit.Size = new System.Drawing.Size(140, 22);
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
			this.editToolStripMenuItem.Size = new System.Drawing.Size(37, 21);
			this.editToolStripMenuItem.Text = "&Edit";
			// 
			// undoToolStripMenuItem
			// 
			this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
			this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
			this.undoToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
			this.undoToolStripMenuItem.Text = "&Undo";
			// 
			// redoToolStripMenuItem
			// 
			this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
			this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
			this.redoToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
			this.redoToolStripMenuItem.Text = "&Redo";
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(136, 6);
			// 
			// cutToolStripMenuItem
			// 
			this.cutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cutToolStripMenuItem.Image")));
			this.cutToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
			this.cutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
			this.cutToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
			this.cutToolStripMenuItem.Text = "Cu&t";
			// 
			// copyToolStripMenuItem
			// 
			this.copyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyToolStripMenuItem.Image")));
			this.copyToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.copyToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
			this.copyToolStripMenuItem.Text = "&Copy";
			// 
			// pasteToolStripMenuItem
			// 
			this.pasteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pasteToolStripMenuItem.Image")));
			this.pasteToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
			this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
			this.pasteToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
			this.pasteToolStripMenuItem.Text = "&Paste";
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(136, 6);
			// 
			// selectAllToolStripMenuItem
			// 
			this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
			this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
			this.selectAllToolStripMenuItem.Text = "Select &All";
			// 
			// toolsToolStripMenuItem
			// 
			this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.customizeToolStripMenuItem,
            this.optionsToolStripMenuItem});
			this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
			this.toolsToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
			this.toolsToolStripMenuItem.Text = "&Tools";
			// 
			// customizeToolStripMenuItem
			// 
			this.customizeToolStripMenuItem.Name = "customizeToolStripMenuItem";
			this.customizeToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
			this.customizeToolStripMenuItem.Text = "&Customize";
			// 
			// optionsToolStripMenuItem
			// 
			this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
			this.optionsToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
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
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 21);
			this.helpToolStripMenuItem.Text = "&Help";
			// 
			// contentsToolStripMenuItem
			// 
			this.contentsToolStripMenuItem.Name = "contentsToolStripMenuItem";
			this.contentsToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
			this.contentsToolStripMenuItem.Text = "&Contents";
			// 
			// indexToolStripMenuItem
			// 
			this.indexToolStripMenuItem.Name = "indexToolStripMenuItem";
			this.indexToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
			this.indexToolStripMenuItem.Text = "&Index";
			// 
			// searchToolStripMenuItem
			// 
			this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
			this.searchToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
			this.searchToolStripMenuItem.Text = "&Search";
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(115, 6);
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
			this.aboutToolStripMenuItem.Text = "&About...";
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
			this.tbs.ResumeLayout(false);
			this.tabChat.ResumeLayout(false);
			this.scChat.Panel1.ResumeLayout(false);
			this.scChat.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.scChat)).EndInit();
			this.scChat.ResumeLayout(false);
			this.tabMaintenance.ResumeLayout(false);
			this.tabMaintenance.PerformLayout();
			this.cbMenuBar1.ResumeLayout(false);
			this.cbMenuBar1.PerformLayout();
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
		private System.Windows.Forms.TabControl tbs;
		private System.Windows.Forms.TabPage tabChat;
		private System.Windows.Forms.SplitContainer scChat;
		private System.Windows.Forms.RichTextBox txtOutput;
		private System.Windows.Forms.RichTextBox txtInput;
		private System.Windows.Forms.TabPage tabMaintenance;
		private System.Windows.Forms.Button cmdSpeak;
		private System.Windows.Forms.TextBox txtSpeak;
		private System.Windows.Forms.Label lblMaintenanceSpeak;
	}
}