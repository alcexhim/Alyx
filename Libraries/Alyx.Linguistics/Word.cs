﻿using Alyx.Linguistics.LanguageParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alyx.Linguistics
{
	public class Word
	{
		public class WordCollection
			: System.Collections.Generic.List<Word>
		{
			public Word this[Guid id]
			{
				get
				{
					foreach (Word item in this)
					{
						if (item.ID == id) return item;
					}
					return null;
				}
			}

			public WordInstance[] GetWordInstances(Word word, WordMapperMapping mapping = null)
			{
				List<WordInstance> list = new List<WordInstance>();
				foreach (WordClass clas in word.Classes)
				{
					if (clas == WordClasses.Adjective)
					{
						list.Add(new AdjectiveInstance(word));
					}
					else if (clas == WordClasses.Adverb)
					{
						list.Add(new AdverbInstance(word));
					}
					else if (clas.ID == WordClasses.Article.ID)
					{
						ArticleInstance inst = new ArticleInstance(word);
						if (mapping != null)
						{
							foreach (WordMapperMappingCriteria criteria in mapping.Criteria)
							{
								if (criteria.Definiteness != Definiteness.Unspecified) inst.Definiteness = criteria.Definiteness;
								if (criteria.Quantity != Quantity.Unspecified) inst.Quantity = criteria.Quantity;
							}
						}
						list.Add(inst);
					}
					else if (clas == WordClasses.Conjunction)
					{
						list.Add(new ConjunctionInstance(word));
					}
					/*
					else if (clas == WordClasses.Interjection)
					{
						list.Add(new AdjectiveInstance(word));
					}
					*/
					else if (clas == WordClasses.Noun)
					{
						list.Add(new NounInstance(word));
					}
					else if (clas == WordClasses.Preposition)
					{
						list.Add(new PrepositionInstance(word));
					}
					else if (clas == WordClasses.Pronoun)
					{
						PronounInstance inst = new PronounInstance(word);
						if (mapping != null)
						{
							foreach (WordMapperMappingCriteria criteria in mapping.Criteria)
							{
								if (criteria.Usage != WordUsages.Unspecified) inst.Usages.Add(criteria.Usage);
								if (criteria.Gender != Genders.Unspecified) inst.Gender = criteria.Gender;
								if (criteria.Person != Person.Unspecified) inst.Person = criteria.Person;
								if (criteria.Quantity != Quantity.Unspecified) inst.Quantity = criteria.Quantity;
							}
						}
						list.Add(inst);
					}
					else if (clas == WordClasses.Verb)
					{
						VerbInstance verb = new VerbInstance(word);
						if (mapping != null)
						{
							foreach (WordMapperMappingCriteria criteria in mapping.Criteria)
							{
								if (criteria.Aspect != Aspect.Unspecified) verb.Aspect = criteria.Aspect;
								if (criteria.Person != Person.Unspecified) verb.Person = criteria.Person;
								if (criteria.Quantity != Quantity.Unspecified) verb.Quantity = criteria.Quantity;
								if (criteria.Tense != Tense.Unspecified) verb.Tense = criteria.Tense;
							}
						}

						list.Add(verb);
					}
				}
				return list.ToArray();
			}
			public WordInstance[] GetWordInstances(string value)
			{
				value = value.ToLower();

				List<WordInstance> list = new List<WordInstance>();

				bool exitAll = false;

				foreach (Word word in this)
				{
					if (word.Value == value)
					{
						WordInstance[] wordInstances = GetWordInstances(word);
						return wordInstances;
					}
					else
					{
						Dictionary<string, object> dict = new Dictionary<string, object> ();
						dict.Add ("Word", word);
						dict.Add ("WordClasses", word.Classes);
						dict.Add ("ID", word.ID.ToString ("B").ToUpper ());

						WordMapper[] mappers = Language.CurrentLanguage.WordMappers.GetByCondition (dict);
						foreach (WordMapper mapper in mappers)
						{
							foreach (WordMapperMapping mapping in mapper.Mappings)
							{
								string[] mappingValueParts = mapping.Value.Split(new string[] { "$(Word)" }, StringSplitOptions.None);
								if (mappingValueParts.Length >= 2)
								{
									if (String.IsNullOrEmpty(mappingValueParts[0]) && String.IsNullOrEmpty(mappingValueParts[mappingValueParts.Length - 1]))
									{
										if (mapping.Value.Replace("$(Word)", word.Value) == value)
										{
											return GetWordInstances(word, mapping);
										}
									}
									else if ((!String.IsNullOrEmpty(mappingValueParts[0]) && value.StartsWith(mappingValueParts[0]))
										|| (!String.IsNullOrEmpty(mappingValueParts[mappingValueParts.Length - 1]) && value.EndsWith(mappingValueParts[mappingValueParts.Length - 1])))
									{
										string wordValue = value.Substring(mappingValueParts[0].Length, value.Length - mappingValueParts[mappingValueParts.Length - 1].Length);
										if (word.Value == wordValue)
										{
											return GetWordInstances(word, mapping);
										}
									}
								}
								else if (mappingValueParts.Length == 1)
								{
									if (mapping.Value.ToLower() == value) return GetWordInstances(word, mapping);
								}
							}
						}
					}
					if (exitAll) break;
				}

				return list.ToArray();
			}
		}

		private Guid mvarID = Guid.Empty;
		public Guid ID { get { return mvarID; } }

		private int mvarPriority = 0;
		public int Priority { get { return mvarPriority; } set { mvarPriority = value; } }

		private Pronunciation.PronunciationString.PronunciationStringCollection mvarPronunciations = new Pronunciation.PronunciationString.PronunciationStringCollection ();
		public Pronunciation.PronunciationString.PronunciationStringCollection Pronunciations { get { return mvarPronunciations; } }

		private string mvarValue = String.Empty;
		public string Value { get { return mvarValue; } set { mvarValue = value; } }

		private WordClass.WordClassCollection mvarClasses = new WordClass.WordClassCollection();
		/// <summary>
		/// The <see cref="WordClass" />es that are applicable to this <see cref="Word" />.
		/// </summary>
		public WordClass.WordClassCollection Classes { get { return mvarClasses; } }

		private Dictionary<WordClass, Dictionary<string, object>> dictClassProperties = new Dictionary<WordClass, Dictionary<string, object>>();
		public void SetClassProperty<T>(WordClass wordClass, string propertyName, T propertyValue)
		{
			if (!dictClassProperties.ContainsKey (wordClass)) {
				dictClassProperties [wordClass] = new Dictionary<string, object> ();
			}
			dictClassProperties [wordClass] [propertyName] = propertyValue;
		}
		public T GetClassProperty<T>(WordClass wordClass, string propertyName, T defaultValue = default(T))
		{
			if (!dictClassProperties.ContainsKey (wordClass)) {
				dictClassProperties [wordClass] = new Dictionary<string, object> ();
			}
			if (!dictClassProperties [wordClass].ContainsKey (propertyName)) {
				dictClassProperties [wordClass] [propertyName] = defaultValue;
			}
			return (T)dictClassProperties [wordClass] [propertyName];
		}

		private WordMapper mvarMapper = null;
		public WordMapper Mapper { get { return mvarMapper; } set { mvarMapper = value; } }

		private WordSource.WordSourceCollection mvarSources = new WordSource.WordSourceCollection();
		public WordSource.WordSourceCollection Sources { get { return mvarSources; } }

		public Word(Guid id)
		{
			mvarID = id;
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(mvarID.ToString("B").ToUpper());
			sb.Append(' ');
			sb.Append(mvarValue);
			return sb.ToString();
		}
	}
}
