using System;

using UniversalWidgetToolkit;
using UniversalWidgetToolkit.Controls;
using UniversalWidgetToolkit.Layouts;

namespace Alyx
{
	public partial class ConnectDialog : Dialog
	{
		private Label lblTitle;
		private Label lblServerName;
		internal TextBox txtServerName;

		private Label lblPort;
		internal TextBox txtPort;

		private Label lblUserName;
		internal TextBox txtUserName;

		private Label lblPassword;
		internal TextBox txtPassword;

		private void InitializeComponent ()
		{
			this.Title = "Connect to Server";

			GridLayout layout = new GridLayout ();
			this.Layout = layout;

			this.lblTitle = new Label ();
			this.lblTitle.Text = "Connect to A.L.Y.X.";
			this.Controls.Add (this.lblTitle, new GridLayout.Constraints (0, 0, 1, 1));

			this.lblServerName = new Label ();
			this.lblServerName.Text = "Server _name or IP address:";
			this.lblServerName.HorizontalAlignment = HorizontalAlignment.Right;
			this.Controls.Add (this.lblServerName, new GridLayout.Constraints (1, 0, 1, 1));

			this.txtServerName = new TextBox ();
			this.Controls.Add (this.txtServerName, new GridLayout.Constraints (1, 1, 1, 1));

			this.lblPort = new Label ();
			this.lblPort.Text = "_Port:";
			this.lblPort.HorizontalAlignment = HorizontalAlignment.Right;
			this.Controls.Add (this.lblPort, new GridLayout.Constraints (1, 2, 1, 1));
			
			this.txtPort = new TextBox ();
			this.txtPort.Text = "51221";
			this.txtPort.MaxLength = 5;
			this.txtPort.WidthChars = 5;
			this.Controls.Add (this.txtPort, new GridLayout.Constraints (1, 3, 1, 1));
			
			this.lblUserName = new Label ();
			this.lblUserName.Text = "_User name:";
			this.lblUserName.HorizontalAlignment = HorizontalAlignment.Right;
			this.Controls.Add (this.lblUserName, new GridLayout.Constraints (2, 0, 1, 1));

			this.txtUserName = new TextBox ();
			this.Controls.Add (this.txtUserName, new GridLayout.Constraints (2, 1, 1, 3));
			
			this.lblPassword = new Label ();
			this.lblPassword.Text = "_Password:";
			this.lblPassword.HorizontalAlignment = HorizontalAlignment.Right;
			this.Controls.Add (this.lblPassword, new GridLayout.Constraints (3, 0, 1, 1));

			this.txtPassword = new TextBox ();
			this.txtPassword.UseSystemPasswordChar = true;
			this.Controls.Add (this.txtPassword, new GridLayout.Constraints (3, 1, 1, 3));

			this.Buttons.Add (new Button (ButtonStockType.Connect, DialogResult.OK));
			this.Buttons.Add (new Button (ButtonStockType.Cancel, DialogResult.Cancel));

			this.DefaultButton = this.Buttons [0];
		}
	}
}

