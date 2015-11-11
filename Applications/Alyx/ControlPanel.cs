using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Alyx
{
	public partial class ControlPanel : UserControl
	{
		public ControlPanel()
		{
			InitializeComponent();
		}

		public virtual string[] Path
		{
			get
			{
				return null;
			}
		}
	}
}
