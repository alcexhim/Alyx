using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alyx.Linguistics
{
	public class Language
	{
		private Guid mvarID = Guid.Empty;
		public Guid ID { get { return mvarID; } }

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private Language(Guid id)
		{
			mvarID = id;
		}

		public static Language Create(Guid id)
		{
			Language item = new Language(id);
			return item;
		}
	}
}
