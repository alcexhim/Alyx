using Alyx.Linguistics.LanguageParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alyx.Linguistics
{
	public class Sentence
	{
		private Clause.ClauseCollection mvarClauses = new Clause.ClauseCollection();
		public Clause.ClauseCollection Clauses { get { return mvarClauses; } }

		private SentenceType mvarSentenceType = SentenceTypes.Declarative;
		public SentenceType SentenceType { get { return mvarSentenceType; } set { mvarSentenceType = value; } }

		public Sentence(SentenceType type, Clause[] clauses = null)
		{
			mvarSentenceType = type;
			if (clauses != null)
			{
				foreach (Clause item in clauses)
				{
					mvarClauses.Add(item);
				}
			}
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			SentenceTypeMapping mapping = null;

			// TODO: implement subject-verb agreement!
			foreach (Clause clause in mvarClauses)
			{
				foreach (ISubject subject in clause.Subjects)
				{
					if (subject is PronounInstance)
					{
						PronounInstance niSubject = (subject as PronounInstance);
						clause.Predicate.Verb.Person = niSubject.Person;
						clause.Predicate.Verb.Quantity = niSubject.Quantity;
					}
					else if (subject is NounInstance)
					{
						if (clause.Predicate.Verb != null)
						{
							clause.Predicate.Verb.Person = Person.ThirdPerson;
						}
					}
				}
			}
			
			if (mvarSentenceType != null)
			{
				Language lang = Language.CurrentLanguage;
				if (lang != null)
				{
					mapping = lang.SentenceTypeMappings[mvarSentenceType.ID];
				}
			}

			if (mapping != null)
			{
				sb.Append(mapping.Prefix);
			}

			foreach (Clause clause in mvarClauses)
			{
				sb.Append(clause.ToString());
			}

			if (mapping != null)
			{
				sb.Append(mapping.Suffix);
			}

			string text = sb.ToString();
			return (text.Substring(0, 1).ToUpper() + text.Substring(1));
		}

		public static Sentence Parse(string value)
		{
			Language lang = Language.CurrentLanguage;
			SentenceType type = null;

			foreach (SentenceTypeMapping mapping in lang.SentenceTypeMappings)
			{
				if ((mapping.Prefix == null || value.StartsWith(mapping.Prefix)) && (mapping.Suffix == null || value.EndsWith(mapping.Suffix)))
				{
					if (mapping.ID == SentenceTypes.Declarative.ID)
					{
						type = SentenceTypes.Declarative;
						break;
					}
					else if (mapping.ID == SentenceTypes.Exclamatory.ID)
					{
						type = SentenceTypes.Exclamatory;
						break;
					}
					else if (mapping.ID == SentenceTypes.Imperative.ID)
					{
						type = SentenceTypes.Imperative;
						break;
					}
					else if (mapping.ID == SentenceTypes.Interrogative.ID)
					{
						type = SentenceTypes.Interrogative;
						break;
					}
				}
			}

			string next = null;
			StringBuilder sbNext = new StringBuilder();


			Sentence sent = new Sentence(type);

			Stack<Word> nextUnknown = new Stack<Word>();
			ArticleInstance nextArticle = null;
			List<AdjectiveInstance> listAdjectives = new List<AdjectiveInstance>();
			Clause nextClause = new Clause();
			VerbInstance nextVerb = null;
			PrepositionInstance nextPrep = null;
			ISubject nextObject = null;
			ConjunctionInstance nextConj = null;

			for (int i = 0; i < value.Length; i++)
			{
				if (value[i] == ' ')
				{
					while (value[i] == ' ')
					{
						i++;
					}
					i--;

					next = sbNext.ToString();
					sbNext = new StringBuilder();
					ProcessWord(next, false, ref nextClause, ref nextUnknown, ref nextArticle, ref listAdjectives, ref nextVerb, ref nextPrep, ref nextObject, ref nextConj);
				}
				else if (value[i] != '.' && value[i] != '?' && value[i] != '!' && value[i] != ',')
				{
					sbNext.Append(value[i]);
				}
			}

			if (sbNext.Length > 0)
			{
				// final word
				next = sbNext.ToString();
				ProcessWord(next, true, ref nextClause, ref nextUnknown, ref nextArticle, ref listAdjectives, ref nextVerb, ref nextPrep, ref nextObject, ref nextConj);

				sent.Clauses.Add(nextClause);
			}

			if (nextUnknown.Count > 0)
			{
				Console.WriteLine("prediction: sentence '" + value + "' contains too many unknowns and is unparseable");
			}
			return sent;
		}

		private static bool ProcessWord(string next, bool final, ref Clause nextClause, ref Stack<Word> nextUnknown, ref ArticleInstance nextArticle, ref List<AdjectiveInstance> listAdjectives, ref VerbInstance nextVerb, ref PrepositionInstance nextPrep, ref ISubject nextObject, ref ConjunctionInstance nextConj)
		{
			Language lang = Language.CurrentLanguage;

			WordInstance[] wordInstances = lang.Words.GetWordInstances(next);
			if (wordInstances.Length == 0)
			{
				Console.WriteLine("Unknown word '" + next.ToLower() + "'");
				{
					Word unk = new Word(Guid.NewGuid());
					unk.Value = next.ToLower();
					lang.Words.Add(unk);
					nextUnknown.Push(unk);
				}
				if (final)
				{
					if (nextUnknown.Count > 0)
					{
						NounInstance[] nouns = PredictNoun(ref nextUnknown, ref listAdjectives, ref nextArticle, ref nextConj);
						if (nextPrep != null)
						{
							nextClause.Predicate = new Predicates.PrepositionalObjectPredicate(nextVerb, nextPrep, nouns);
							nextPrep = null;
						}
						else if (nextClause.Subjects.Count > 0)
						{
							if (nextObject != null)
							{
								nextClause.Predicate = new Predicates.IndirectObjectPredicate(nextVerb, nouns, new ISubject[] { nextObject });
							}
							else
							{
								nextClause.Predicate = new Predicates.DirectObjectPredicate(nextVerb, nouns);
							}
						}
						else
						{
							nextClause.Subjects.AddRange(nouns);
						}
					}
				}
				return false;
			}

			foreach (WordInstance inst in wordInstances)
			{
				if (inst is ArticleInstance)
				{
					if (nextUnknown.Count > 2)
					{
						VerbInstance verb = PredictVerb(ref nextUnknown);
						if (nextUnknown.Count > 1)
						{
							NounInstance[] ni = PredictNoun(ref nextUnknown, ref listAdjectives, ref nextArticle, ref nextConj);
							nextClause.Subjects.AddRange(ni);
							nextVerb = verb;
						}
					}

					nextArticle = (inst as ArticleInstance);
					break;
				}
				else if (inst is AdjectiveInstance)
				{
					if (nextUnknown.Count > 0)
					{
						PredictAdjectives(ref nextUnknown, ref listAdjectives);
					}

					listAdjectives.Add(inst as AdjectiveInstance);
					break;
				}
				else if (inst is NounInstance)
				{
					NounInstance noun = (inst as NounInstance);

					PredictAdjectives(ref nextUnknown, ref listAdjectives);

					foreach (AdjectiveInstance adj1 in listAdjectives)
					{
						noun.Adjectives.Add(adj1);
					}
					listAdjectives.Clear();

					if (nextArticle != null)
					{
						noun.Definiteness = nextArticle.Definiteness;
						noun.Quantity = nextArticle.Quantity;
						nextArticle = null;
					}

					if (nextPrep != null)
					{
						nextClause.Predicate = new Predicates.PrepositionalObjectPredicate(nextVerb, nextPrep, new ISubject[] { noun });
					}
					else
					{
						nextClause.Subjects.Add(noun);
					}
					break;
				}
				else if (inst is VerbInstance)
				{
					if (nextUnknown.Count > 0)
					{
						NounInstance[] nouns = PredictNoun(ref nextUnknown, ref listAdjectives, ref nextArticle, ref nextConj);

						if (nextPrep != null)
						{
							nextClause.Predicate = new Predicates.PrepositionalObjectPredicate(nextVerb, nextPrep, nouns);
						}
						else
						{
							foreach (NounInstance noun in nouns)
							{
								nextClause.Subjects.Add(noun);
							}
						}
					}

					nextVerb = (inst as VerbInstance);
					break;
				}
				else if (inst is PrepositionInstance)
				{
					nextPrep = (inst as PrepositionInstance);
					break;
				}
				else if (inst is PronounInstance)
				{
					if (nextClause.Subjects.Count == 0)
					{
						nextClause.Subjects.Add(inst as PronounInstance);
					}
					else
					{
						VerbInstance verb = PredictVerb(ref nextUnknown);
						nextVerb = verb;
						nextObject = (inst as PronounInstance);
					}
					break;
				}
				else if (inst is ConjunctionInstance)
				{
					nextConj = (inst as ConjunctionInstance);
					break;
				}
			}
			return true;
		}

		private static VerbInstance PredictVerb(ref Stack<Word> nextUnknown)
		{
			Word unkVerb = nextUnknown.Pop();

			// since it came before a known AdjectiveInstance, it must be an adjective
			unkVerb.Classes.Add(WordClasses.Verb);

			VerbInstance verb = new VerbInstance(unkVerb);

			Console.WriteLine("prediction: next unknown word '" + verb.Word.Value + "' created as Verb");
			return verb;
		}

		private static NounInstance[] PredictNoun(ref Stack<Word> nextUnknown, ref List<AdjectiveInstance> listAdjectives, ref ArticleInstance nextArticle, ref ConjunctionInstance nextConj)
		{
			List<NounInstance> list = new List<NounInstance>();
			Word unkNoun = nextUnknown.Pop();

			// since it came before a known AdjectiveInstance, it must be an adjective
			unkNoun.Classes.Add(WordClasses.Noun);

			NounInstance noun = new NounInstance(unkNoun);

			Console.WriteLine("prediction: next unknown word '" + noun.Word.Value + "' created as Noun");

			if (nextConj != null)
			{
				Word unkNoun2 = nextUnknown.Pop();
				unkNoun2.Classes.Add(WordClasses.Noun);

				NounInstance noun2 = new NounInstance(unkNoun2);
				Console.WriteLine("prediction: next unknown word '" + noun2.Word.Value + "' created as Noun");

				list.Add(noun2);

				nextConj = null;
			}

			PredictAdjectives(ref nextUnknown, ref listAdjectives);

			foreach (AdjectiveInstance adj in listAdjectives)
			{
				noun.Adjectives.Add(adj);
			}
			listAdjectives.Clear();

			if (nextArticle != null)
			{
				noun.Definiteness = nextArticle.Definiteness;
				noun.Quantity = nextArticle.Quantity;
				nextArticle = null;
			}
			list.Insert(0, noun);

			return list.ToArray();
		}

		private static void PredictAdjectives(ref Stack<Word> nextUnknown, ref List<AdjectiveInstance> listAdjectives)
		{
			while (nextUnknown.Count > 0)
			{
				Word unk1 = nextUnknown.Pop();
				unk1.Classes.Add(WordClasses.Adjective);

				AdjectiveInstance adj = new AdjectiveInstance(unk1);
				listAdjectives.Add(adj);
				Console.WriteLine("prediction: next unknown word '" + adj.Word.Value + "' created as Adjective");
			}
		}
	}
}
