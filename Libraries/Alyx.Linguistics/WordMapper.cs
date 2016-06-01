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
			: System.Collections.Generic.List<WordMapper>
		{
			public WordMapper[] GetByCondition(Dictionary<string, object> dict) {
				List<WordMapper> list = new List<WordMapper> ();
				foreach (WordMapper mapper in this) {
					if (mapper.Condition.Test (dict))
						list.Add (mapper);
				}
				return list.ToArray ();
			}
		}

		private Guid mvarID = Guid.Empty;
		public Guid ID { get { return mvarID; } }

		private IConditionalStatement mvarCondition = null;
		public IConditionalStatement Condition { get { return mvarCondition; } set { mvarCondition = value; } }

		private int mvarPriority = 0;
		public int Priority { get { return mvarPriority; } set { mvarPriority = value; } }

		private WordMapperMapping.WordMapperMappingCollection mvarMappings = new WordMapperMapping.WordMapperMappingCollection();
		public WordMapperMapping.WordMapperMappingCollection Mappings { get { return mvarMappings; } }

		public WordInstance[] GetInstances (string value)
		{
			List<WordInstance> list = new List<WordInstance> ();
			foreach (WordMapperMapping mapping in mvarMappings) {
				if (mapping.Value == "$(Word)") {
					string word = value;
				}
				else {
					string[] parts = mapping.Value.Split (new string[] { "$(Word)" }, StringSplitOptions.None);
					  
					if (parts.Length == 2) {
						if (String.IsNullOrEmpty (parts [0])) {
							if (value.EndsWith (parts [1])) {
								string wordStr = value.Substring (0, value.Length - parts [1].Length);

								Console.WriteLine ("root word found: '" + wordStr + "'");
								// still don't know how to create arbitrary WordInstance for this word

								WordInstance[] inst = Language.CurrentLanguage.Words.GetWordInstances (wordStr);
								if (inst.Length == 0) {
									Word word = new Word (Guid.NewGuid ());
									word.Classes.Add (WordClasses.Verb);
									word.Value = wordStr;

									// Aspect=Continuous, Tense=Present: loving
									// Aspect=Continuous, Tense=Past: being loved
									// Aspect=Perfect, Tense=Present: having loved
									// Aspect=Perfect, Tense=Past: having been loved

									VerbInstance verb = new VerbInstance (word);
									verb.Aspect = Aspect.Continuous;
									verb.Tense = Tense.Present;
									list.Add (verb);
								}
							}
						}
					}
				}
			}
			return list.ToArray ();
		}

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
						if (((criterion.Aspect == Aspect.Unspecified /* && verb.Aspect == Aspect.Unspecified */) || (criterion.Aspect == verb.Aspect))
							&& ((criterion.Person == Person.Unspecified /* && verb.Person == Person.Unspecified */) || (criterion.Person == verb.Person))
							&& ((criterion.Tense == Tense.Unspecified /* && verb.Tense == Tense.Unspecified */) || (criterion.Tense == verb.Tense))
							&& ((criterion.Quantity == Quantity.Unspecified /* && verb.Quantity == Quantity.Unspecified */) || (criterion.Quantity == verb.Quantity))
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
							((criterion.Person == Person.Unspecified /* && pronoun.Person == Person.Unspecified */) || (criterion.Person == pronoun.Person))
							&& ((criterion.Gender == Genders.Unspecified /* && pronoun.Gender == Genders.Unspecified */) || (criterion.Gender == pronoun.Gender))
							&& ((criterion.Quantity == Quantity.Unspecified /* && pronoun.Quantity == Quantity.Unspecified */) || (criterion.Quantity == pronoun.Quantity))
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
							((criterion.Definiteness == Definiteness.Unspecified) || (criterion.Definiteness == article.Definiteness))
							&& ((criterion.Quantity == Quantity.Unspecified) || (criterion.Quantity == article.Quantity))
						)
						{
							found = true;
							break;
						}
					}
				}

				if (found)
				{
					return ExecMapping(mapping.Value, word.Word.Value);
				}
			}
			return null;
		}

		private string ExecMapping(string mapping, string value)
		{
			StringBuilder sb = new StringBuilder();
			if (mapping.Contains("$(Word)"))
			{
				sb.Append(mapping.Replace("$(Word)", value));
			}
			else
			{
				for (int i = 0; i < mapping.Length; i++)
				{
					if (mapping[i] == '$')
					{
						if (mapping[i + 1] == '(')
						{
							string variableName = mapping.Substring(i + 2, mapping.IndexOf(')', i + 1) - 1);
							i += variableName.Length + 2;

							string[] variableParts = variableName.Split(new char[] { ':' });
							if (variableParts.Length > 1)
							{
								variableName = variableParts[0];

								string funcName = variableParts[1];
								string[] funcParams = new string[0];
								if (variableParts[1].Contains('('))
								{
									funcName = funcName.Substring(0, funcName.IndexOf('('));
									funcParams = variableParts[1].Substring(variableParts[1].IndexOf('(') + 1, variableParts[1].IndexOf(')') - (variableParts[1].IndexOf('(') + 1)).Split(new char[] { ',' });
								}

								for (int j = 0; j < funcParams.Length; j++)
								{
									funcParams[j] = funcParams[j].Replace("Length", value.Length.ToString());

									StringBuilder sbNext = new StringBuilder();
									string op1 = null;
									int op = 0;

									for (int k = 0; k < funcParams[j].Length; k++)
									{
										if (funcParams[j][k] == '-')
										{
											op1 = sbNext.ToString();
											op = -1;
											sbNext = new StringBuilder();
										}
										else
										{
											sbNext.Append(funcParams[j][k]);
										}
									}
									if (op != 0)
									{
										int p1 = Int32.Parse(op1);
										int p2 = Int32.Parse(sbNext.ToString());
										sbNext = new StringBuilder();
										sbNext.Append((p1 - p2).ToString());
									}
									funcParams[j] = sbNext.ToString();
								}

								switch (funcName.ToLower())
								{
									case "substring":
									{
										if (funcParams.Length == 2)
										{
											int start = Int32.Parse(funcParams[0]);
											int length = Int32.Parse(funcParams[1]);
											
											switch (variableName)
											{
												case "Word":
												{
													sb.Append(value.Substring(start, length));
													break;
												}
											}
										}
										break;
									}
								}
							}
						}
					}
					else
					{
						sb.Append(mapping[i]);
					}
				}
			}
			return sb.ToString();
		}

		public WordMapper(Guid id, IConditionalStatement condition = null)
		{
			mvarID = id;
			mvarCondition = condition;
		}
	}
}
