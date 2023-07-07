using System;

using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Controls.ListView;
using MBS.Framework.UserInterface.Layouts;

namespace Alyx
{
	public partial class MainWindow : Window
	{
		private SplitContainer scLeftRight;

		private ListViewControl tv;
		private DefaultTreeModel tm;

		private void InitializeComponent()
		{
			this.Layout = new BoxLayout (Orientation.Horizontal);

			this.tm = new DefaultTreeModel (new Type[] { typeof(string) });

			this.tv = new ListViewControl();
			this.tv.Model = this.tm;

			this.scLeftRight = new SplitContainer (Orientation.Horizontal);
			this.scLeftRight.Panel1.Layout = new BoxLayout (Orientation.Vertical);
			this.scLeftRight.Panel1.Controls.Add (this.tv);

			this.scLeftRight.Panel2.Layout = new BoxLayout (Orientation.Vertical);
			this.scLeftRight.Panel2.Controls.Add (new Button ());

			this.Controls.Add (scLeftRight, new BoxLayout.Constraints (true, true));
		}
	}
}

