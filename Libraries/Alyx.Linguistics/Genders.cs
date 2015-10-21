using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alyx.Linguistics
{
	public static class Genders
	{
		private static Gender mvarMasculine = new Gender(new Guid("{DD296D03-F8B5-42A5-9A9E-B62E342667B6}"));
		public static Gender Masculine { get { return mvarMasculine; } }

		private static Gender mvarFeminine = new Gender(new Guid("{272413AD-BF77-473E-B118-8C766C1025E4}"));
		public static Gender Feminine { get { return mvarFeminine; } }

		private static Gender mvarUnspecified = new Gender(Guid.Empty);
		public static Gender Unspecified { get { return mvarUnspecified; } }
	}
}
