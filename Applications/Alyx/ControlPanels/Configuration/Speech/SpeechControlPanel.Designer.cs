namespace Alyx.ControlPanels.Configuration.Speech
{
	partial class SpeechControlPanel
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
			this.cmdSpeak = new System.Windows.Forms.Button();
			this.lblMaintenanceSpeak = new System.Windows.Forms.Label();
			this.txtSpeak = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// cmdSpeak
			// 
			this.cmdSpeak.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdSpeak.Location = new System.Drawing.Point(277, 6);
			this.cmdSpeak.Name = "cmdSpeak";
			this.cmdSpeak.Size = new System.Drawing.Size(75, 23);
			this.cmdSpeak.TabIndex = 5;
			this.cmdSpeak.Text = "&Speak";
			this.cmdSpeak.UseVisualStyleBackColor = true;
			this.cmdSpeak.Click += new System.EventHandler(this.cmdSpeak_Click);
			// 
			// lblMaintenanceSpeak
			// 
			this.lblMaintenanceSpeak.AutoSize = true;
			this.lblMaintenanceSpeak.Location = new System.Drawing.Point(15, 11);
			this.lblMaintenanceSpeak.Name = "lblMaintenanceSpeak";
			this.lblMaintenanceSpeak.Size = new System.Drawing.Size(80, 13);
			this.lblMaintenanceSpeak.TabIndex = 3;
			this.lblMaintenanceSpeak.Text = "Speak this text:";
			// 
			// txtSpeak
			// 
			this.txtSpeak.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtSpeak.Location = new System.Drawing.Point(101, 8);
			this.txtSpeak.Name = "txtSpeak";
			this.txtSpeak.Size = new System.Drawing.Size(170, 20);
			this.txtSpeak.TabIndex = 4;
			// 
			// SpeechControlPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.cmdSpeak);
			this.Controls.Add(this.lblMaintenanceSpeak);
			this.Controls.Add(this.txtSpeak);
			this.Name = "SpeechControlPanel";
			this.Size = new System.Drawing.Size(355, 159);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button cmdSpeak;
		private System.Windows.Forms.Label lblMaintenanceSpeak;
		private System.Windows.Forms.TextBox txtSpeak;

	}
}
