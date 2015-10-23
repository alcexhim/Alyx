using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alyx.Core.ConditionalExpressions;
using Alyx.Linguistics.LanguageParts;

namespace Alyx.Linguistics
{
	/// <summary>
	/// A <see cref="WordMapper" /> is responsible for mapping GUIDs to words in a particular
	/// language.
	/// </summary>
	public class WordMapper
	{
		public class WordMapperCollection
			: System.Collections.ObjectModel.Collection<WordMapper>
		{

		}

		private Guid mvarID = Guid.Empty;
		public Guid ID { get { return mvarID; } }

		private IConditionalStatement mvarCondition = null;
		public IConditionalStatement Condition { get { return mvarCondition; } set { mvarCondition = value; } }

		private WordMapperMapping.WordMapperMappingCollection mvarMappings = new WordMapperMapping.WordMapperMappingCollection();
		public WordMapperMapping.WordMapperMappingCollection Mappings { get { return mvarMappings; } }

		public string GetValue(WordInstance word)
		{
			ArticleInstance article = (word as ArticleInstance);
			NounInstance noun = (word as NounInstance);
			VerbInstance verb = (word as VerbInstance);
			AdjectiveInstance adjective = (word as AdjectiveInstance);
			PronounInstance pronoun = (word as PronounInstance);
			PrepositionInstance prep = (word as PrepositionInstance);

			foreach (WordMapperMapping mapping in mvarMappings)
			{
				bool found = false;
				
				foreach (WordMapperMappingCriteria criterion in mapping.Criteria)
				{
					if (noun != null)
					{
						if ((criterion.Quantity == Quantity.Unspecified && noun.Quantity == Quantity.Unspecified) || (criterion.Quantity == noun.Quantity))
						{
							found = true;
							break;
						}
					}
					else if (verb != null)
					{
						if (((criterion.Aspect == Aspect.Unspecified && verb.Aspect == Aspect.Unspecified) || (criterion.Aspect == verb.Aspect))
							&& ((criterion.Person == Person.Unspecified && verb.Person == Person.Unspecified) || (criterion.Person == verb.Person))
							&& ((criterion.Tense == Tense.Unspecified && verb.Tense == Tense.Unspecified) || (criterion.Tense == verb.Tense))
							&& ((criterion.Quantity == Quantity.Unspecified && verb.Quantity == Quantity.Unspecified) || (criterion.Quantity == verb.Quantity))
						)
						{
							found = true;
							break;
						}
					}
					else if (adjective != null)
					{
						found = true;
						break;
					}
					else if (pronoun != null)
					{
						if
						(
							((criterion.Person == Person.Unspecified && pronoun.Person == Person.Unspecified) || (criterion.Person == pronoun.Person))
							&& ((criterion.Gender == Genders.Unspecified && pronoun.Gender == Genders.Unspecified) || (criterion.Gender == pronoun.Gender))
						)
						{
							found = true;
							break;
						}
					}
					else if (prep != null)
					{
						found = true;
						break;
					}
					else if (article != null)
					{
						if 
						(
							((criterion.Definiteness == Definiteness.Unspecified && article.Definiteness == Definiteness.Unspecified) || (criterion.Definiteness == article.Definiteness))
							&& ((criterion.Quantity == Quantity.Unspecified && article.Quantity == Quantity.Unspecified) || (criterion.Quantity == article.Quantity))
						)
						{
							found = true;
							break;
						}
					}
				}

				if (found)
				{
					return mapping.Value.Replace("$(Word)", word.Word.Value);
				}
			}
			return null;
		}

		public WordMapper(Guid id, IConditionalStatement condition = null)
		{
			mvarID = id;
			mvarCondition = condition;
		}
	}
}
