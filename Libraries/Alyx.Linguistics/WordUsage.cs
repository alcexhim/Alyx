using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alyx.Linguistics
{
	public class WordUsage
	{
		public class WordUsageCollection
			: System.Collections.ObjectModel.Collection<WordUsage>
		{
			public WordUsage this[Guid id]
			{
				get
				{
					foreach (WordUsage item in this)
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


		public override bool Equals(object obj)
		{
			if (obj is WordUsage)
			{
				return mvarID.Equals((obj as WordUsage).ID);
			}
			return base.Equals(obj);
		}

		public static bool operator ==(WordUsage left, WordUsage right)
		{
			return left.Equals(right);
		}
		public static bool operator !=(WordUsage left, WordUsage right)
		{
			return !left.Equals(right);
		}

		public WordUsage(Guid id)
		{
			mvarID = id;
		}
	}
}
