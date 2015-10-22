using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alyx.Linguistics
{
	public class Word
	{
		public class WordCollection
			: System.Collections.ObjectModel.Collection<Word>
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

			private Dictionary<string, Word> wordsByValue = new Dictionary<string, Word>();

			public Word this[string value]
			{
				get
				{
					value = value.ToLower();

					if (!wordsByValue.ContainsKey(value))
					{
						bool exitAll = false;

						foreach (Word word in this)
						{
							if (word.Value == value)
							{
								wordsByValue.Add(value, word);
								break;
							}
							else
							{
								foreach (WordMapper mapper in Language.CurrentLanguage.WordMappers)
								{
									bool exit = false;
									if (mapper.Condition == null || mapper.Condition.Test
									(
										new KeyValuePair<string, object>("Word", word),
										new KeyValuePair<string, object>("WordClasses", word.Classes),
										new KeyValuePair<string, object>("ID", word.ID.ToString("B").ToUpper())
									))
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
														wordsByValue.Add(value, word);
														exit = true;
														break;
													}
												}
												else if ((!String.IsNullOrEmpty(mappingValueParts[0]) && value.StartsWith(mappingValueParts[0]))
													|| (!String.IsNullOrEmpty(mappingValueParts[mappingValueParts.Length - 1]) && value.EndsWith(mappingValueParts[mappingValueParts.Length - 1])))
												{
													string wordValue = value.Substring(mappingValueParts[0].Length, value.Length - mappingValueParts[mappingValueParts.Length - 1].Length);
													if (word.Value == wordValue)
													{
														wordsByValue.Add(value, word);
														exit = true;
														break;
													}
												}
											}
											else if (mappingValueParts.Length == 1)
											{
												if (mapping.Value == value) return word;
											}
										}
									}
									if (exit)
									{
										exitAll = true;
										break;
									}
								}
							}
							if (exitAll) break;
						}
					}

					if (wordsByValue.ContainsKey(value)) return wordsByValue[value];
					return null;
				}
			}
		}

		private Guid mvarID = Guid.Empty;
		public Guid ID { get { return mvarID; } }

		private string mvarValue = String.Empty;
		public string Value { get { return mvarValue; } set { mvarValue = value; } }

		private WordClass.WordClassCollection mvarClasses = new WordClass.WordClassCollection();
		/// <summary>
		/// The <see cref="WordClass" />es that are applicable to this <see cref="Word" />.
		/// </summary>
		public WordClass.WordClassCollection Classes { get { return mvarClasses; } }

		private WordMapper mvarMapper = null;
		public WordMapper Mapper { get { return mvarMapper; } set { mvarMapper = value; } }

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
