using System;

using UniversalWidgetToolkit;
using UniversalWidgetToolkit.Controls;

namespace Alyx
{
	public class MainWindow : Window
	{
		private SplitContainer scLeftRight = new SplitContainer (Orientation.Vertical);

		public MainWindow ()
		{
			this.Controls.Add (scLeftRight);
		}
	}
}

