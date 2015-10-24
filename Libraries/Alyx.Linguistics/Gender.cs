using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alyx.Linguistics
{
	public class Gender
	{
		public class GenderCollection
			: System.Collections.ObjectModel.Collection<Gender>
		{
			public Gender this[Guid id]
			{
				get
				{
					foreach (Gender item in this)
					{
						if (item.ID == id) return item;
					}
					return null;
				}
			}
		}

		private Guid mvarID = Guid.Empty;
		public Guid ID { get { return mvarID; } set { mvarID = value; } }

		public Gender(Guid id)
		{
			mvarID = id;
		}

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		public override bool Equals(object obj)
		{
			if (obj is Gender)
			{
				return mvarID.Equals((obj as Gender).ID);
			}
			return base.Equals(obj);
		}
		
		public static bool operator ==(Gender left, Gender right)
		{
			return left.Equals(right);
		}
		public static bool operator !=(Gender left, Gender right)
		{
			return !left.Equals(right);
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(mvarID.ToString("B").ToUpper());
			sb.Append(' ');
			sb.Append(mvarTitle);
			return sb.ToString();
		}
	}
}
