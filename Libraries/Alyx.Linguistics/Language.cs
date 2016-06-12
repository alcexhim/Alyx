using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alyx.Core.ConditionalExpressions;
using Alyx.Linguistics.LanguageParts;

using UniversalEditor;
using UniversalEditor.Accessors;
using UniversalEditor.DataFormats.Markup.XML;
using UniversalEditor.ObjectModels.Markup;

namespace Alyx.Linguistics
{
	public class Language
	{
		public class LanguageCollection
			: System.Collections.ObjectModel.Collection<Language>
		{
			private Dictionary<Guid, Language> _itemsByID = new Dictionary<Guid, Language> ();

			public Language this[Guid id]
			{
				get
				{
					if (_itemsByID.ContainsKey (id)) {
						return _itemsByID [id];
					}
					return null;
				}
			}

			protected override void ClearItems ()
			{
				base.ClearItems ();
				_itemsByID.Clear ();
			}
			protected override void InsertItem (int index, Language item)
			{
				base.InsertItem (index, item);
				_itemsByID[item.ID] = item;
			}
			protected override void RemoveItem (int index)
			{
				if (index >= 0 && index < this.Count) {
					if (_itemsByID.ContainsKey (this [index].ID)) {
						_itemsByID.Remove (this [index].ID);
					}
				}
				base.RemoveItem (index);
			}
			protected override void SetItem (int index, Language item)
			{
				if (index >= 0 && index < this.Count) {
					if (_itemsByID.ContainsKey (this [index].ID)) {
						_itemsByID.Remove (this [index].ID);
					}
				}
				base.SetItem (index, item);
				_itemsByID[item.ID] = item;
			}
		}

		private Guid mvarID = Guid.Empty;
		public Guid ID { get { return mvarID; } }

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private SentenceParser mvarSentenceParser = new Alyx.Linguistics.SentenceParsers.V1.DefaultSentenceParserV1 ();
		public SentenceParser SentenceParser { get { return mvarSentenceParser; } set { mvarSentenceParser = value; } }

		private SentenceTypeMapping.SentenceTypeMappingCollection mvarSentenceTypeMappings = new SentenceTypeMapping.SentenceTypeMappingCollection();
		/// <summary>
		/// Sentence type mappings from ID to sentence type value.
		/// </summary>
		public SentenceTypeMapping.SentenceTypeMappingCollection SentenceTypeMappings { get { return mvarSentenceTypeMappings; } }

		private WordUsage.WordUsageCollection mvarWordUsages = new WordUsage.WordUsageCollection();
		public WordUsage.WordUsageCollection WordUsages { get { return mvarWordUsages; } }

		private WordSource.WordSourceCollection mvarWordSources = new WordSource.WordSourceCollection();
		public WordSource.WordSourceCollection WordSources { get { return mvarWordSources; } }

		private Gender.GenderCollection mvarGenders = new Gender.GenderCollection();
		public Gender.GenderCollection Genders { get { return mvarGenders; } }

		private WordMapper.WordMapperCollection mvarWordMappers = new WordMapper.WordMapperCollection();
		public WordMapper.WordMapperCollection WordMappers { get { return mvarWordMappers; } }

		private WordClass.WordClassCollection mvarWordClasses = new WordClass.WordClassCollection();
		public WordClass.WordClassCollection WordClasses { get { return mvarWordClasses; } }

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

		public override string ToString()
		{
			return mvarTitle;
		}

		private Word.WordCollection mvarWords = new Word.WordCollection();
		/// <summary>
		/// All <see cref="Word" />s available in this <see cref="Language" />.
		/// </summary>
		public Word.WordCollection Words { get { return mvarWords; } }

		private WordPattern.WordPatternCollection mvarWordPatterns = new WordPattern.WordPatternCollection();
		/// <summary>
		/// Gets the word patterns.
		/// </summary>
		/// <value>The word patterns.</value>
		public WordPattern.WordPatternCollection WordPatterns { get { return mvarWordPatterns; } }

		private SentencePattern.SentencePatternCollection mvarSentencePatterns = new SentencePattern.SentencePatternCollection();
		public SentencePattern.SentencePatternCollection SentencePatterns { get { return mvarSentencePatterns; } }

		private static void LoadCriteria(Language lang, MarkupTagElement tag, WordMapperMappingCriteria.WordMapperMappingCriteriaCollection criteria)
		{
			foreach (MarkupElement elCriterion in tag.Elements)
			{
				MarkupTagElement tagCriterion = (elCriterion as MarkupTagElement);
				if (tagCriterion == null) continue;
				if (tagCriterion.FullName != "Criterion") continue;

				WordMapperMappingCriteria criterion = new WordMapperMappingCriteria();
													
				MarkupAttribute attAspect = tagCriterion.Attributes["Aspect"];
				if (attAspect != null)
				{
					switch (attAspect.Value.ToLower())
					{
						case "continuous":
						{
							criterion.Aspect = Aspect.Continuous;
							break;
						}
						case "perfect":
						{
							criterion.Aspect = Aspect.Continuous;
							break;
						}
						case "perfectcontinuous":
						{
							criterion.Aspect = Aspect.PerfectContinuous;
							break;
						}
						case "simple":
						{
							criterion.Aspect = Aspect.Simple;
							break;
						}
					}
				}
				MarkupAttribute attPerson = tagCriterion.Attributes["Person"];
				if (attPerson != null)
				{
					switch (attPerson.Value.ToLower())
					{
						case "firstperson":
						{
							criterion.Person = Person.FirstPerson;
							break;
						}
						case "secondperson":
						{
							criterion.Person = Person.SecondPerson;
							break;
						}
						case "thirdperson":
						{
							criterion.Person = Person.ThirdPerson;
							break;
						}
					}
				}
				MarkupAttribute attQuantity = tagCriterion.Attributes["Quantity"];
				if (attQuantity != null)
				{
					switch (attQuantity.Value.ToLower())
					{
						case "singular":
						{
							criterion.Quantity = Quantity.Singular;
							break;
						}
						case "plural":
						{
							criterion.Quantity = Quantity.Plural;
							break;
						}
					}
				}
				MarkupAttribute attTense = tagCriterion.Attributes["Tense"];
				if (attTense != null)
				{
					switch (attTense.Value.ToLower())
					{
						case "future":
						{
							criterion.Tense = Tense.Future;
							break;
						}
						case "past":
						{
							criterion.Tense = Tense.Past;
							break;
						}
						case "pastfuture":
						{
							criterion.Tense = Tense.PastFuture;
							break;
						}
						case "present":
						{
							criterion.Tense = Tense.Present;
							break;
						}
					}
				}
				
				MarkupAttribute attUsage = tagCriterion.Attributes["UsageID"];
				if (attUsage != null)
				{
					criterion.Usage = lang.WordUsages[new Guid(attUsage.Value)];
				}

				MarkupAttribute attGenderID = tagCriterion.Attributes["GenderID"];
				if (attGenderID != null)
				{
					criterion.Gender = lang.Genders[new Guid(attGenderID.Value)];
				}

				MarkupAttribute attDefiniteness = tagCriterion.Attributes["Definiteness"];
				if (attDefiniteness != null)
				{
					switch (attDefiniteness.Value.ToLower())
					{
						case "definite":
						{
							criterion.Definiteness = Definiteness.Definite;
							break;
						}
						case "indefinite":
						{
							criterion.Definiteness = Definiteness.Indefinite;
							break;
						}
					}
				}

				criteria.Add(criterion);
			}
		}

		public static Language FromMarkup(MarkupTagElement tagLanguage)
		{
			if (tagLanguage == null) return null;
			if (tagLanguage.FullName != "Language") return null;

			MarkupAttribute attLanguageID = tagLanguage.Attributes["ID"];
			if (attLanguageID == null) return null;

			Language lang = new Language(new Guid(attLanguageID.Value));

			MarkupTagElement tagInformation = (tagLanguage.Elements["Information"] as MarkupTagElement);
			if (tagInformation != null)
			{
				MarkupTagElement tagTitle = (tagInformation.Elements["Title"] as MarkupTagElement);
				if (tagTitle != null) lang.Title = tagTitle.Value;
			}

			MarkupTagElement tagContractionTypes = (tagLanguage.Elements["ContractionTypes"] as MarkupTagElement);
			if (tagContractionTypes != null)
			{
				foreach (MarkupElement elContractionType in tagContractionTypes.Elements)
				{
					MarkupTagElement tagContractionType = (elContractionType as MarkupTagElement);
					if (tagContractionType == null) continue;
					if (tagContractionType.FullName != "ContractionType") continue;

					MarkupAttribute attContractionTypeID = tagContractionType.Attributes["ID"];
					if (attContractionTypeID == null) continue;

					MarkupAttribute attContraction = tagContractionType.Attributes["Contraction"];
					MarkupAttribute attValue = tagContractionType.Attributes["Value"];

					MarkupTagElement tagAllowedPrefixes = (tagContractionType.Elements["AllowedPrefixes"] as MarkupTagElement);

					if (attContraction == null || attValue == null || tagAllowedPrefixes == null) continue;

					ContractionType ctype = new ContractionType();
					ctype.ID = new Guid(attContractionTypeID.Value);
					ctype.Contraction = attContraction.Value;
					ctype.Value = attValue.Value;
					foreach (MarkupElement elAllowedPrefix in tagAllowedPrefixes.Elements)
					{
						MarkupTagElement tagAllowedPrefix = (elAllowedPrefix as MarkupTagElement);
						if (tagAllowedPrefix == null) continue;
						if (tagAllowedPrefix.FullName != "AllowedPrefix") continue;

						MarkupAttribute attAllowedPrefixValue = tagAllowedPrefix.Attributes["Value"];
						if (attAllowedPrefixValue == null) continue;

						ctype.Prefixes.Add(attAllowedPrefixValue.Value);
					}
					lang.ContractionTypes.Add(ctype);
				}
			}

			MarkupTagElement tagDefaultSentenceParser = (tagLanguage.Elements ["DefaultSentenceParser"] as MarkupTagElement);
			if (tagDefaultSentenceParser != null)
			{
				MarkupAttribute attDefaultSentenceParserTypeName = tagDefaultSentenceParser.Attributes ["TypeName"];
				if (attDefaultSentenceParserTypeName != null)
				{
					Type type = Type.GetType (attDefaultSentenceParserTypeName.Value);
					if (type != null)
					{
						lang.SentenceParser = (SentenceParser)(type.Assembly.CreateInstance (type.FullName));
					}
				}
			}

			MarkupTagElement tagGenders = (tagLanguage.Elements["Genders"] as MarkupTagElement);
			if (tagGenders != null)
			{
				foreach (MarkupElement elGender in tagGenders.Elements)
				{
					MarkupTagElement tagGender = (elGender as MarkupTagElement);
					if (tagGender == null) continue;
					if (tagGender.FullName != "Gender") continue;

					MarkupAttribute attGenderID = tagGender.Attributes["ID"];
					if (attGenderID == null) continue;

					Gender gender = new Gender(new Guid(attGenderID.Value));
					
					MarkupAttribute attGenderTitle = tagGender.Attributes["Title"];
					if (attGenderTitle != null) gender.Title = attGenderTitle.Value;

					lang.Genders.Add(gender);
				}
			}

			MarkupTagElement tagWordUsages = (tagLanguage.Elements["WordUsages"] as MarkupTagElement);
			if (tagWordUsages != null)
			{
				foreach (MarkupElement elWordUsage in tagWordUsages.Elements)
				{
					MarkupTagElement tagWordUsage = (elWordUsage as MarkupTagElement);
					if (tagWordUsage == null) continue;
					if (tagWordUsage.FullName != "WordUsage") continue;

					MarkupAttribute attWordUsageID = tagWordUsage.Attributes["ID"];
					if (attWordUsageID == null) continue;

					WordUsage usage = new WordUsage(new Guid(attWordUsageID.Value));

					MarkupAttribute attWordUsageTitle = tagWordUsage.Attributes["Title"];
					if (attWordUsageTitle != null) usage.Title = attWordUsageTitle.Value;

					lang.WordUsages.Add(usage);
				}
			}

			MarkupTagElement tagSentenceTypes = (tagLanguage.Elements["SentenceTypes"] as MarkupTagElement);
			if (tagSentenceTypes != null)
			{
				foreach (MarkupElement elSentenceType in tagSentenceTypes.Elements)
				{
					MarkupTagElement tagSentenceType = (elSentenceType as MarkupTagElement);
					if (tagSentenceType == null) continue;
					if (tagSentenceType.FullName != "SentenceType") continue;

					MarkupAttribute attID = tagSentenceType.Attributes["ID"];
					if (attID == null) continue;

					SentenceTypeMapping mapping = new SentenceTypeMapping(new Guid(attID.Value));

					MarkupAttribute attPrefix = tagSentenceType.Attributes["Prefix"];
					if (attPrefix != null) mapping.Prefix = attPrefix.Value;

					MarkupAttribute attSuffix = tagSentenceType.Attributes["Suffix"];
					if (attSuffix != null) mapping.Suffix = attSuffix.Value;

					lang.SentenceTypeMappings.Add(mapping);
				}
			}

			MarkupTagElement tagWordSources = (tagLanguage.Elements["WordSources"] as MarkupTagElement);
			if (tagWordSources != null)
			{
				foreach (MarkupElement elWordSource in tagWordSources.Elements)
				{
					MarkupTagElement tagWordSource = (elWordSource as MarkupTagElement);
					if (tagWordSource == null) continue;
					if (tagWordSource.FullName != "WordSource") continue;

					MarkupAttribute attWordSourceID = tagWordSource.Attributes["ID"];
					if (attWordSourceID == null) continue;

					MarkupAttribute attWordSourceTitle = tagWordSource.Attributes["Title"];
					if (attWordSourceTitle == null) continue;

					WordSource source = new WordSource();
					source.ID = new Guid(attWordSourceID.Value);
					source.Title = attWordSourceTitle.Value;
					lang.WordSources.Add(source);
				}
			}

			MarkupTagElement tagWordMappers = (tagLanguage.Elements["WordMappers"] as MarkupTagElement);
			if (tagWordMappers != null)
			{
				foreach (MarkupElement elWordMapper in tagWordMappers.Elements)
				{
					MarkupTagElement tagWordMapper = (elWordMapper as MarkupTagElement);
					if (tagWordMapper == null) continue;
					if (tagWordMapper.FullName != "WordMapper") continue;

					MarkupAttribute attID = tagWordMapper.Attributes["ID"];
					if (attID == null) continue;

					WordMapper mapper = new WordMapper(new Guid(attID.Value));

					MarkupAttribute attPriority = tagWordMapper.Attributes["Priority"];
					if (attPriority != null)
					{
						int priority = 0;
						if (attPriority.Value.ToLower() == "highest")
						{
							priority = Int32.MaxValue;
						}
						else if (attPriority.Value.ToLower() == "lowest")
						{
							priority = Int32.MinValue;
						}
						else
						{
							Int32.TryParse(attPriority.Value, out priority);
						}
						mapper.Priority = priority;
					}

					MarkupTagElement tagConditionalStatement = (tagWordMapper.Elements["ConditionalStatement"] as MarkupTagElement);
					if (tagConditionalStatement != null && tagConditionalStatement.Elements.Count > 0)
					{
						MarkupTagElement tagCondition = null;
						foreach (MarkupElement el in tagConditionalStatement.Elements)
						{
							tagCondition = (el as MarkupTagElement);
							if (tagCondition != null) break;
						}
						mapper.Condition = ConditionalStatementParser.Parse(tagCondition);
					}

					MarkupTagElement tagMappings = (tagWordMapper.Elements["Mappings"] as MarkupTagElement);
					if (tagMappings != null)
					{
						foreach (MarkupElement elMapping in tagMappings.Elements)
						{
							MarkupTagElement tagMapping = (elMapping as MarkupTagElement);
							if (tagMapping == null) continue;
							if (tagMapping.FullName != "Mapping") continue;

							MarkupAttribute attValue = tagMapping.Attributes["Value"];
							if (attValue == null) continue;

							WordMapperMapping mapping = new WordMapperMapping(attValue.Value);

							MarkupTagElement tagCriteria = (tagMapping.Elements["Criteria"] as MarkupTagElement);
							if (tagCriteria != null)
							{
								LoadCriteria(lang, tagCriteria, mapping.Criteria);
							}

							mapper.Mappings.Add(mapping);
						}
					}

					lang.WordMappers.Add(mapper);
				}
			}

			lang.WordMappers.Sort(new Comparison<WordMapper>(delegate(WordMapper left, WordMapper right)
			{
				return right.Priority.CompareTo(left.Priority);
			}));

			#region Load word classes
			{
				MarkupTagElement tagWordClasses = (tagLanguage.Elements["WordClasses"] as MarkupTagElement);
				if (tagWordClasses != null)
				{
					foreach (MarkupElement elWordClass in tagWordClasses.Elements)
					{
						MarkupTagElement tagWordClass = (elWordClass as MarkupTagElement);
						if (tagWordClass == null) continue;

						MarkupAttribute attWordClassID = tagWordClass.Attributes["ID"];
						if (attWordClassID == null) continue;

						WordClass wordClass = new WordClass(new Guid(attWordClassID.Value));

						MarkupAttribute attWordClassTitle = tagWordClass.Attributes["Title"];
						if (attWordClassTitle != null)
							wordClass.Title = attWordClassTitle.Value;

						lang.WordClasses.Add(wordClass);
					}
				}
			}
			#endregion

			MarkupTagElement tagWords = (tagLanguage.Elements["Words"] as MarkupTagElement);
			if (tagWords != null)
			{
				foreach (MarkupElement elWord in tagWords.Elements)
				{
					MarkupTagElement tagWord = (elWord as MarkupTagElement);
					if (tagWord == null) continue;
					if (tagWord.FullName != "Word") continue;

					MarkupAttribute attWordID = tagWord.Attributes["ID"];
					if (attWordID == null) continue;

					Word word = new Word(new Guid(attWordID.Value));

					MarkupAttribute attPriority = tagWord.Attributes["Priority"];
					if (attPriority != null)
					{
						int priority = 0;
						if (attPriority.Value.ToLower() == "highest")
						{
							priority = Int32.MaxValue;
						}
						else if (attPriority.Value.ToLower() == "lowest")
						{
							priority = Int32.MinValue;
						}
						else
						{
							Int32.TryParse(attPriority.Value, out priority);
						}
						word.Priority = priority;
					}

					MarkupAttribute attValue = tagWord.Attributes["Value"];
					if (attValue != null) word.Value = attValue.Value;

					MarkupTagElement tagWordClasses = (tagWord.Elements["WordClasses"] as MarkupTagElement);
					if (tagWordClasses != null)
					{
						foreach (MarkupElement elWordClass in tagWordClasses.Elements)
						{
							MarkupTagElement tagWordClass = (elWordClass as MarkupTagElement);
							if (tagWordClass == null) continue;
							if (tagWordClass.FullName != "WordClass") continue;

							MarkupAttribute attWordClassID = tagWordClass.Attributes["ID"];
							if (attWordClassID == null) continue;

							WordClass wordClass = lang.WordClasses[new Guid(attWordClassID.Value)];
							if (wordClass == null) continue;

							MarkupAttribute attWordClassTitle = tagWordClass.Attributes["Title"];
							if (attWordClassTitle != null)
							{
								wordClass.Title = attWordClassTitle.Value;
							}

							word.Classes.Add(wordClass);
						}
					}

					lang.Words.Add(word);
				}

				lang.Words.Sort(new Comparison<Word>(delegate(Word left, Word right)
				                                                 {
					return right.Priority.CompareTo(left.Priority);
				}));
			}

			MarkupTagElement tagWordPatterns = (tagLanguage.Elements ["WordPatterns"] as MarkupTagElement);
			if (tagWordPatterns != null)
			{
				foreach (MarkupElement elWordPattern in tagWordPatterns.Elements)
				{
					WordPattern pattern = WordPattern.FromMarkup (elWordPattern as MarkupTagElement, lang);
					if (pattern == null)
						continue;
					lang.WordPatterns.Add (pattern);
				}

				foreach (WordPattern pattern in lang.WordPatterns)
				{
					foreach (WordPatternPart part in pattern.Parts)
					{
						if (part._WordPatternID != Guid.Empty)
						{
							part.WordPattern = lang.WordPatterns [part._WordPatternID];
						}
					}
				}
			}

			MarkupTagElement tagSentencePatterns = (tagLanguage.Elements ["SentencePatterns"] as MarkupTagElement);
			if (tagSentencePatterns != null)
			{
				foreach (MarkupElement elSentencePattern in tagSentencePatterns.Elements)
				{
					SentencePattern pattern = SentencePattern.FromMarkup (elSentencePattern as MarkupTagElement, lang);
					if (pattern == null)
						continue;

					lang.SentencePatterns.Add (pattern);
				}
			}

			return lang;
		}

		public ArticleInstance GetArticle(Definiteness definiteness = Definiteness.Unspecified, Quantity quantity = Quantity.Unspecified)
		{
			foreach (Word word in mvarWords)
			{
				if (word.Classes.Contains(Linguistics.WordClasses.Article))
				{
					return GetArticle(word.ID, definiteness, quantity);
				}
			}
			return null;
		}
		public ArticleInstance GetArticle(Guid id, Definiteness definiteness = Definiteness.Unspecified, Quantity quantity = Quantity.Unspecified)
		{
			Word word = mvarWords[id];
			if (word == null) return null;
			if (!word.Classes.Contains(Alyx.Linguistics.WordClasses.Article)) return null;

			return new ArticleInstance(word, definiteness, quantity);
		}
		public NounInstance GetNoun(Guid id)
		{
			Word word = mvarWords[id];
			if (word == null) return null;
			if (!word.Classes.Contains(Alyx.Linguistics.WordClasses.Noun)) return null;

			return new NounInstance(word);
		}
		public AdjectiveInstance GetAdjective(Guid id)
		{
			Word word = mvarWords[id];
			if (word == null) return null;
			if (!word.Classes.Contains(Alyx.Linguistics.WordClasses.Adjective)) return null;

			return new AdjectiveInstance(word);
		}
		public VerbInstance GetVerb(Guid id, Person person = Person.Unspecified, Tense tense = Tense.Unspecified, Aspect aspect = Aspect.Unspecified)
		{
			Word word = mvarWords[id];
			if (word == null) return null;
			if (!word.Classes.Contains(Alyx.Linguistics.WordClasses.Verb)) return null;

			return new VerbInstance(word, person, tense, aspect);
		}
		public PrepositionInstance GetPreposition(Guid id)
		{
			Word word = mvarWords[id];
			if (word == null) return null;
			if (!word.Classes.Contains(Alyx.Linguistics.WordClasses.Preposition)) return null;

			return new PrepositionInstance(word);
		}

		public PronounInstance GetPronoun(Guid id, Person person, Quantity quantity)
		{
			Word[] pronouns = GetPronouns();
			for (int i = 0; i < pronouns.Length; i++) {
				if (pronouns [i].ID == id) {
					PronounInstance pi = new PronounInstance (pronouns [i]);
					pi.Person = person;
					pi.Quantity = quantity;
					return pi;
				}
			}
			return null;
		}

		private ContractionType.ContractionTypeCollection mvarContractionTypes = new ContractionType.ContractionTypeCollection();
		public ContractionType.ContractionTypeCollection ContractionTypes { get { return mvarContractionTypes; } }

		private Word[] mvarPronouns = null;
		public Word[] GetPronouns()
		{
			if (mvarPronouns == null)
			{
				List<Word> list = new List<Word>();
				foreach (Word word in mvarWords)
				{
					if (word.Classes.Contains(Alyx.Linguistics.WordClasses.Pronoun))
					{
						list.Add(word);
					}
				}
				mvarPronouns = list.ToArray();
			}
			return mvarPronouns;
		}

		private bool mvarEnableOxfordComma = true;
		/// <summary>
		/// Determines whether the Oxford comma (a comma before the final "and" in a multiple-item <see cref="Series" />) is rendered.
		/// </summary>
		public bool EnableOxfordComma { get { return mvarEnableOxfordComma; } set { mvarEnableOxfordComma = value; } }

		public string ReplaceContractions(string value)
		{
			StringBuilder sb = new StringBuilder();
			string[] words = value.Split(new char[] { ' ' });
			for (int i = 0; i < words.Length; i++)
			{
				if (words[i].Contains('\''))
				{
					string[] w = words[i].Split(new char[] { '\'' });
					string prefix = w[0].ToLower();
					string suffix = w[1].ToLower();
					foreach (ContractionType ct in mvarContractionTypes)
					{
						if (ct.Prefixes.Contains(prefix))
						{
							sb.Append(prefix);
							sb.Append(' ');
							sb.Append(ct.Value);
							break;
						}
					}
				}
				else
				{
					sb.Append(words[i]);
				}
				if (i < words.Length - 1) sb.Append(' ');
			}
			return sb.ToString();
		}
	}
}
