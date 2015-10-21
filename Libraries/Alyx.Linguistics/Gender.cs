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
	}
}
