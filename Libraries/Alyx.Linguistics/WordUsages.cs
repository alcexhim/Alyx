using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alyx.Linguistics
{
	public static class WordUsages
	{
		private static WordUsage mvarUnspecified = new WordUsage(new Guid("{00000000-0000-0000-0000-000000000000}"));
		public static WordUsage Unspecified { get { return mvarUnspecified; } }

		private static WordUsage mvarSubject = new WordUsage(new Guid("{64AE32D4-3DCB-43C8-A91C-D2D5DF66DE26}"));
		public static WordUsage Subject { get { return mvarSubject; } }

		private static WordUsage mvarObject = new WordUsage(new Guid("{1B51C0CE-8F1E-44DA-B9A1-D3E78E8D963A}"));
		public static WordUsage Object { get { return mvarObject; } }

		private static WordUsage mvarPossessive = new WordUsage(new Guid("{D4D598DD-4931-497D-83A4-34BD76B85F08}"));
		public static WordUsage Possessive { get { return mvarPossessive; } }
	}
}
