using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alyx.Linguistics
{
	public class SentenceTypeMapping
	{
		public class SentenceTypeMappingCollection
			: System.Collections.ObjectModel.Collection<SentenceTypeMapping>
		{
			public SentenceTypeMapping this[Guid id]
			{
				get
				{
					foreach (SentenceTypeMapping item in this)
					{
						if (item.ID == id) return item;
					}
					return null;
				}
			}
		}

		private Guid mvarID = Guid.Empty;
		public Guid ID { get { return mvarID; } }

		private string mvarPrefix = String.Empty;
		public string Prefix { get { return mvarPrefix; } set { mvarPrefix = value; } }

		private string mvarSuffix = String.Empty;
		public string Suffix { get { return mvarSuffix; } set { mvarSuffix = value; } }

		public SentenceTypeMapping(Guid id, string prefix, string suffix)
		{
			mvarID = id;
			mvarPrefix = prefix;
			mvarSuffix = suffix;
		}
	}
}
