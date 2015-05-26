using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alyx.Linguistics
{
	public abstract class Word
	{
		private Guid mvarID = Guid.Empty;
		public Guid ID { get { return mvarID; } }

		public Word(Guid id)
		{
			mvarID = id;
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			Language lang = Language.CurrentLanguage;
			if (lang != null)
			{
				WordMapping mapping = lang.WordMappings[mvarID];
				if (mapping != null)
				{
					if (mapping.Values.Count > 0)
					{
						return mapping.GetValue(this);
					}
				}
			}
			return sb.ToString();
		}
	}
}
