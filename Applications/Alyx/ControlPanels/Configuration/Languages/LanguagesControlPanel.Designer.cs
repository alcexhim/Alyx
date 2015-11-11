namespace Alyx.ControlPanels.Configuration.Languages
{
	partial class LanguagesControlPanel
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
			this.listView1 = new System.Windows.Forms.ListView();
			this.cmdAdd = new System.Windows.Forms.Button();
			this.cmdRemove = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// listView1
			// 
			this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.HideSelection = false;
			this.listView1.Location = new System.Drawing.Point(3, 32);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(444, 300);
			this.listView1.TabIndex = 0;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.Details;
			// 
			// cmdAdd
			// 
			this.cmdAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdAdd.Location = new System.Drawing.Point(3, 3);
			this.cmdAdd.Name = "cmdAdd";
			this.cmdAdd.Size = new System.Drawing.Size(75, 23);
			this.cmdAdd.TabIndex = 1;
			this.cmdAdd.Text = "&Add";
			this.cmdAdd.UseVisualStyleBackColor = true;
			// 
			// cmdRemove
			// 
			this.cmdRemove.Enabled = false;
			this.cmdRemove.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdRemove.Location = new System.Drawing.Point(84, 3);
			this.cmdRemove.Name = "cmdRemove";
			this.cmdRemove.Size = new System.Drawing.Size(75, 23);
			this.cmdRemove.TabIndex = 1;
			this.cmdRemove.Text = "&Remove";
			this.cmdRemove.UseVisualStyleBackColor = true;
			// 
			// LanguagesControlPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.cmdRemove);
			this.Controls.Add(this.cmdAdd);
			this.Controls.Add(this.listView1);
			this.Name = "LanguagesControlPanel";
			this.Size = new System.Drawing.Size(450, 335);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.Button cmdAdd;
		private System.Windows.Forms.Button cmdRemove;
	}
}
