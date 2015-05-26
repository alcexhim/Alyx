using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alyx.Linguistics
{
	public class WordMappingValue
	{
		public class WordMappingValueCollection
			: System.Collections.ObjectModel.Collection<WordMappingValue>
		{

		}

		private string mvarValue = String.Empty;
		public string Value { get { return mvarValue; } set { mvarValue = value; } }

		private WordMappingValueCriteria.WordMappingValueCriteriaCollection mvarCriteria = new WordMappingValueCriteria.WordMappingValueCriteriaCollection();
		public WordMappingValueCriteria.WordMappingValueCriteriaCollection Criteria { get { return mvarCriteria; } }

		public WordMappingValue(string value, WordMappingValueCriteria[] criteria = null)
		{
			mvarValue = value;
			if (criteria != null)
			{
				foreach (WordMappingValueCriteria criterion in criteria)
				{
					mvarCriteria.Add(criterion);
				}
			}
		}
	}
}
