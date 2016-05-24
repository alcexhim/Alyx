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

			value = lang.ReplaceContractions(value);

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

			SentenceParserContext context = new SentenceParserContext();
			context.Clause = new Clause();

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
					ProcessWord(next, false, ref context);
				}
				else if (value[i] != '.' && value[i] != '?' && value[i] != '!')
				{
					sbNext.Append(value[i]);
				}
			}

			if (sbNext.Length > 0)
			{
				// final word
				next = sbNext.ToString();
				ProcessWord(next, true, ref context);

				sent.Clauses.Add(context.Clause);
			}

			if (context.UnknownWords.Count > 0)
			{
				Console.WriteLine("prediction: sentence '" + value + "' contains too many unknowns and is unparseable");
			}
			return sent;
		}

		private static bool ProcessWord(string next, bool final, ref SentenceParserContext context)
		{
			Language lang = Language.CurrentLanguage;

			if (next.EndsWith(","))
			{
				next = next.Substring(0, next.Length - 1);
				context.Conjunctions.Push (new ConjunctionInstance (lang.Words [new Guid ("{AD54FAAF-3C4A-4027-9EAB-E4F41B6329D4}")]));
			}

			WordInstance[] wordInstances = lang.Words.GetWordInstances(next);
			if (wordInstances.Length == 0)
			{
				Console.WriteLine("Unknown word '" + next.ToLower() + "'");
				{
					Word unk = new Word(Guid.NewGuid());
					unk.Value = next;
					if (lang.WordSources[WordSourceGuids.Learned] != null)
					{
						unk.Sources.Add(lang.WordSources[WordSourceGuids.Learned]);
					}
					lang.Words.Add(unk);
					context.UnknownWords.Push(unk);

					if (context.Word is ArticleInstance)
					{
						// PredictAdjectives (ref context);

						// NounInstance[] nouns = PredictNoun (ref context);
						// context.Clause.Subjects.AddRange (nouns);
					}
				}
				if (final)
				{
					if (context.UnknownWords.Count > 0)
					{
						if (context.Clause.Subjects.Count > 0 && context.UnknownWords.Count == 2)
						{
							// [subject] [verb] [...]
							// you are [a cheap dirty bastard]			N.(NP.) V. N.
							// you are [ugly]							N.(NP.) V. A.

							// N. = ('a' | 'an' | 'the') + ' ' + ...
							// A. = ...

							AdjectiveInstance ai = null;
							NounInstance ni = null;
							Word unk1 = context.UnknownWords.Pop();
							if (context.Article == null)
							{
								if (unk1.Value.Length > 0 && Char.IsUpper(unk1.Value[0]))
								{
									unk1.Classes.Add (WordClasses.Noun);
									unk1.SetClassProperty<bool> (WordClasses.Noun, "IsProper", true);
									ni = new NounInstance (unk1);
								}
								else
								{
									unk1.Classes.Add(WordClasses.Adjective);
									ai = new AdjectiveInstance(unk1);
								}
							}
							else
							{
								unk1.Classes.Add(WordClasses.Noun);
								ni = new NounInstance (unk1);
							}

							if (context.Verb == null)
							{
								VerbInstance vi = PredictVerb(ref context);
								if (ai == null) {
									if (context.Clause.Predicate is Predicates.PrepositionalObjectPredicate) {

										Predicates.PrepositionalObjectPredicate ppo = (context.Clause.Predicate as Predicates.PrepositionalObjectPredicate);
										if (ppo != null) {
											(context.Clause.Subjects [0] as NounInstance).PrepositionalPhrase = new PrepositionalPhrase (ppo.Preposition, ppo.Subjects.ToArray ());
										}
										context.Clause.Predicate = new Predicates.DirectObjectPredicate (vi, new ISubject[] { new NounInstance(unk1) });
									}
								} else {
									context.Clause.Predicate = new Predicates.DirectObjectPredicate (vi, new ISubject[] { ai });
								}
							}
							else
							{
								if (context.Clause.Predicate != null) {
									context.Clause.Predicate.Verb = context.Verb;
									context.Verb = null;
								} else {
									if (context.Article == null) {
										// no article, so it must be a multiple-word proper noun; e.g. "John Smith"
										NounInstance[] noun1 = PredictNoun (ref context);
										ni.Word.Value = noun1 [0].Word.Value + ' ' + ni.Word.Value;

										context.Clause.Predicate = new Predicates.DirectObjectPredicate (context.Verb, new ISubject[] { ni });
									} else {
										// there is an article, so treat it as a single-word noun with adjectives
										ni.Definiteness = context.Article.Definiteness;
										ni.Quantity = context.Article.Quantity;

										PredictAdjectives (ref context);
										ni.Adjectives.AddRange (context.Adjectives.ToArray ());
										context.Clause.Predicate = new Predicates.DirectObjectPredicate (context.Verb, new ISubject[] { ni });
									}
								}
							}
							return true;
						}

						NounInstance[] nouns = PredictNoun(ref context);
						if (context.Preposition != null)
						{
							if (context.Verb != null)
							{
								context.Clause.Predicate = new Predicates.PrepositionalObjectPredicate (context.Verb, context.Preposition, nouns);
								context.Preposition = null;
							}
							else
							{
								NounInstance ni = (context.Clause.Subjects [context.Clause.Subjects.Count - 1] as NounInstance);
								ni.PrepositionalPhrase = new PrepositionalPhrase (context.Preposition, nouns);
							}
						}
						else if (context.Clause.Subjects.Count > 0)
						{
							if (context.PredicateObject != null)
							{
								context.Clause.Predicate = new Predicates.IndirectObjectPredicate(context.Verb, nouns, new ISubject[] { context.PredicateObject });
							}
							else
							{
								context.Clause.Predicate = new Predicates.DirectObjectPredicate(context.Verb, nouns);
							}
						}
						else
						{
							context.Clause.Subjects.AddRange(nouns);
						}
					}
				}
				return false;
			}

			foreach (WordInstance inst in wordInstances)
			{
				context.Word = inst;
				if (inst is ArticleInstance)
				{
					ProcessArticle(ref context);
					break;
				}
				else if (inst is AdjectiveInstance)
				{
					if (context.UnknownWords.Count > 0)
					{
						PredictAdjectives(ref context);
					}

					context.Adjectives.Add(inst as AdjectiveInstance);
					break;
				}
				else if (inst is NounInstance)
				{
					NounInstance noun = (inst as NounInstance);

					PredictAdjectives(ref context);

					foreach (AdjectiveInstance adj1 in context.Adjectives)
					{
						noun.Adjectives.Add(adj1);
					}
					context.Adjectives.Clear();

					if (context.Article != null)
					{
						noun.Definiteness = context.Article.Definiteness;
						noun.Quantity = context.Article.Quantity;
						context.Article = null;
					}

					if (context.Preposition != null)
					{
						context.Clause.Predicate = new Predicates.PrepositionalObjectPredicate(context.Verb, context.Preposition, new ISubject[] { noun });
					}
					else
					{
						context.Clause.Subjects.Add(noun);
					}
					break;
				}
				else if (inst is VerbInstance)
				{
					if (context.UnknownWords.Count > 0)
					{
						NounInstance[] nouns = PredictNoun(ref context);

						if (context.Preposition != null)
						{
							if (context.Verb != null)
							{
								context.Clause.Predicate = new Predicates.PrepositionalObjectPredicate(context.Verb, context.Preposition, nouns);
							}
							else
							{
								NounInstance ni = (context.Clause.Subjects [context.Clause.Subjects.Count - 1] as NounInstance);
								ni.PrepositionalPhrase = new PrepositionalPhrase (context.Preposition, nouns);
							}
						}
						else
						{
							foreach (NounInstance noun in nouns)
							{
								context.Clause.Subjects.Add(noun);
							}
						}
					}

					context.Verb = (inst as VerbInstance);
					break;
				}
				else if (inst is PrepositionInstance)
				{
					NounInstance[] nis = PredictNoun (ref context);
					if (nis.Length > 0)
					{
						context.Clause.Subjects.Add (nis [0]);
					}
					context.Preposition = (inst as PrepositionInstance);
					break;
				}
				else if (inst is PronounInstance)
				{
					PronounInstance pi = (inst as PronounInstance);
					if (pi.Usages.Contains(WordUsages.Subject) && context.Clause.Subjects.Count == 0)
					{
						context.Clause.Subjects.Add(inst as PronounInstance);
					}
					else if (pi.Usages.Contains(WordUsages.Object) && context.Clause.Predicate == null)
					{
						VerbInstance verb = PredictVerb(ref context);
						context.Clause.Predicate = new Predicates.DirectObjectPredicate(verb, new ISubject[] { (inst as PronounInstance) });
					}
					else
					{
						VerbInstance verb = PredictVerb(ref context);
						context.Verb = verb;
						context.PredicateObject = (inst as PronounInstance);
					}
					break;
				}
				else if (inst is ConjunctionInstance)
				{
					context.Conjunctions.Push (inst as ConjunctionInstance);
					break;
				}
			}
			return true;
		}

		private static void ProcessArticle(ref SentenceParserContext context)
		{
			if (context.UnknownWords.Count > 2)
			{
				VerbInstance verb = PredictVerb(ref context);
				if (context.UnknownWords.Count > 1)
				{
					NounInstance[] ni = PredictNoun(ref context);
					context.Clause.Subjects.AddRange(ni);
					context.Verb = verb;
				}
			}
			context.Article = (context.Word as ArticleInstance);
		}

		private static VerbInstance PredictVerb(ref SentenceParserContext context)
		{
			if (context.UnknownWords.Count == 0) return null;

			Word unkVerb = context.UnknownWords.Pop();

			// since it came before a known AdjectiveInstance, it must be an adjective
			unkVerb.Classes.Add(WordClasses.Verb);

			VerbInstance verb = new VerbInstance(unkVerb);

			Console.WriteLine("prediction: next unknown word '" + verb.Word.Value + "' created as Verb");
			return verb;
		}

		private static NounInstance[] PredictNoun(ref SentenceParserContext context)
		{
			if (context.UnknownWords.Count == 0)
				return new NounInstance[0];

			List<NounInstance> list = new List<NounInstance> ();
			Word unkNoun = context.UnknownWords.Pop();

			// since it came before a known AdjectiveInstance, it must be an adjective
			unkNoun.Classes.Add(WordClasses.Noun);

			NounInstance noun = new NounInstance(unkNoun);
			if (context.Article == null)
			{
				// a noun without an article and not in the dictionary is considered a proper noun
				unkNoun.SetClassProperty<bool>(WordClasses.Noun, "IsProper", true);
			}

			if (context.Conjunctions.Count > 0)
			{
				ConjunctionInstance conj = context.Conjunctions.Pop ();

				NounInstance[] nis = PredictNoun(ref context);
				foreach (NounInstance ni in nis)
				{
					list.Add(ni);
				}
			}

			PredictAdjectives(ref context);

			foreach (AdjectiveInstance adj in context.Adjectives)
			{
				noun.Adjectives.Add(adj);
			}
			context.Adjectives.Clear();

			if (context.Article != null)
			{
				noun.Definiteness = context.Article.Definiteness;
				noun.Quantity = context.Article.Quantity;
				context.Article = null;
			}
			list.Add(noun);

			Console.WriteLine("prediction: next unknown word '" + unkNoun.Value + "' created as Noun '" + noun.ToString() + "'");

			return list.ToArray();
		}

		private static void PredictAdjectives(ref SentenceParserContext context)
		{
			while (context.UnknownWords.Count > 0)
			{
				Word unk1 = context.UnknownWords.Pop();
				unk1.Classes.Add(WordClasses.Adjective);

				AdjectiveInstance adj = new AdjectiveInstance(unk1);
				context.Adjectives.Insert(0, adj);
				Console.WriteLine("prediction: next unknown word '" + adj.ToString() + "' created as Adjective");
			}
		}
	}
}
