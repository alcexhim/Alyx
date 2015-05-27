using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alyx.Linguistics
{
	public class WordMapperMapping
	{
		public class WordMapperMappingCollection
			: System.Collections.ObjectModel.Collection<WordMapperMapping>
		{

		}

		private string mvarValue = String.Empty;
		public string Value { get { return mvarValue; } set { mvarValue = value; } }

		private WordMapperMappingCriteria.WordMapperMappingCriteriaCollection mvarCriteria = new WordMapperMappingCriteria.WordMapperMappingCriteriaCollection();
		public WordMapperMappingCriteria.WordMapperMappingCriteriaCollection Criteria { get { return mvarCriteria; } }

		public WordMapperMapping(string value, WordMapperMappingCriteria[] criteria = null)
		{
			mvarValue = value;
			if (criteria != null)
			{
				foreach (WordMapperMappingCriteria criterion in criteria)
				{
					mvarCriteria.Add(criterion);
				}
			}
		}
	}
}
