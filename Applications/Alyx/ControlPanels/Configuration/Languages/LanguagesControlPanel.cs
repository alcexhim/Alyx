using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Alyx.ControlPanels.Configuration.Languages
{
	public partial class LanguagesControlPanel : ControlPanel
	{
		public LanguagesControlPanel()
		{
			InitializeComponent();
		}

		public override string[] Path
		{
			get
			{
				return new string[] { "Configuration", "Languages" };
			}
		}
	}
}
