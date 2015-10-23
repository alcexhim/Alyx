using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alyx.Linguistics
{
	public class WordClass
	{
		public class WordClassCollection
			: System.Collections.ObjectModel.Collection<WordClass>
		{
			public WordClass this[Guid id]
			{
				get
				{
					foreach (WordClass item in this)
					{
						if (item.ID == id) return item;
					}
					return null;
				}
			}
		}

		private Guid mvarID = Guid.Empty;
		public Guid ID { get { return mvarID; } }

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		public override string ToString()
		{
			if (String.IsNullOrEmpty(mvarTitle)) return mvarID.ToString("B").ToUpper();
			return mvarTitle;
		}

		public WordClass(Guid id)
		{
			mvarID = id;
		}

		public override bool Equals(object obj)
		{
			if (obj is WordClass)
			{
				return mvarID.Equals((obj as WordClass).ID);
			}
			return base.Equals(obj);
		}

		public static bool operator ==(WordClass left, WordClass right)
		{
			return left.Equals(right);
		}
		public static bool operator !=(WordClass left, WordClass right)
		{
			return !left.Equals(right);
		}
	}
}
