using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alyx.Linguistics
{
	public static class WordSourceGuids
	{
		private static Guid mvarLearned = new Guid("{1BFAB365-2BAD-43A8-8CCF-6C3B42CDD255}");
		public static Guid Learned { get { return mvarLearned; } }
	}
}
