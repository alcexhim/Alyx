using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alyx.Linguistics.LanguageParts;

namespace Alyx.Linguistics
{
	public class WordMapping
	{
		public class WordMappingCollection
			: System.Collections.ObjectModel.Collection<WordMapping>
		{
			public WordMapping this[Guid id]
			{
				get
				{
					foreach (WordMapping item in this)
					{
						if (item.ID == id) return item;
					}
					return null;
				}
			}
		}

		private Guid mvarID = Guid.Empty;
		public Guid ID { get { return mvarID; } }

		private WordMappingValue.WordMappingValueCollection mvarValues = new WordMappingValue.WordMappingValueCollection();
		public WordMappingValue.WordMappingValueCollection Values { get { return mvarValues; } }

		public WordMapping(Guid id, WordMappingValue[] values)
		{
			mvarID = id;
			foreach (WordMappingValue value in values)
			{
				mvarValues.Add(value);
			}
		}

		public string GetValue(Word word)
		{
			if (mvarValues == null) return null;

			List<string> candidates = new List<string>();
			foreach (WordMappingValue value in mvarValues)
			{
				bool found = false;
				foreach (WordMappingValueCriteria criterion in value.Criteria)
				{
					if (criterion.Match(word))
					{
						found = true;
						break;
					}
				}
				if (found)
				{
					candidates.Add(value.Value);
				}
			}

			if (candidates.Count == 0)
			{
				if (mvarValues.Count > 0)
				{
					return mvarValues[0].Value;
				}
				else
				{
					throw new InvalidOperationException("No candidate found for word '" + word.ID.ToString("B") + "'!");
				}
			}

			return candidates[candidates.Count - 1];
		}
	}
}
