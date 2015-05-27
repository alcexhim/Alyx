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

		private SentenceTypeMapping.SentenceTypeMappingCollection mvarSentenceTypeMappings = new SentenceTypeMapping.SentenceTypeMappingCollection();
		/// <summary>
		/// Sentence type mappings from ID to sentence type value.
		/// </summary>
		public SentenceTypeMapping.SentenceTypeMappingCollection SentenceTypeMappings { get { return mvarSentenceTypeMappings; } }

		private WordMapping.WordMappingCollection mvarWordMappings = new WordMapping.WordMappingCollection();
		/// <summary>
		/// Word mappings from ID to language value.
		/// </summary>
		public WordMapping.WordMappingCollection WordMappings { get { return mvarWordMappings; } }

		private Language(Guid id)
		{
			mvarID = id;
		}

		public static Language Create(Guid id)
		{
			Language item = new Language(id);
			return item;
		}

		private static Language mvarCurrentLanguage = null;
		public static Language CurrentLanguage { get { return mvarCurrentLanguage; } set { mvarCurrentLanguage = value; } }

		public string GetArticle(Definiteness definiteness, Quantity quantity)
		{
			switch (definiteness)
			{
				case Definiteness.Definite: return "the";
				case Definiteness.Indefinite:
				{
					if (quantity == Quantity.Plural) return null;
					return "a";
				}
			}
			return null;
		}
	}
}
