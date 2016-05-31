using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Alyx.Linguistics.LanguageParts;

namespace Alyx.Linguistics.SentenceParsers.V1
{
	public class DefaultSentenceParserV1 : SentenceParser
	{
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
						noun.Article = context.Article;
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

		protected override void ParseInternal (string value, ref Sentence sentence)
		{
			string next = null;

			SentenceParserContext context = new SentenceParserContext();
			context.Clause = new Clause();

			string[] wordValues = value.Split (new char[] { ' ' });

			for (int i = 0; i < wordValues.Length; i++)
			{
				ProcessWord(wordValues[i], (i == wordValues.Length - 1), ref context);
			}

			if (context.Verb != null) {
				// subject-verb agreement
				if (context.Clause.Subjects.Count > 0) {
					if (context.Clause.Subjects [0] is PronounInstance) {
						PronounInstance pi = (context.Clause.Subjects [0] as PronounInstance);
						context.Verb.Person = pi.Person;
						context.Verb.Quantity  = pi.Quantity;
					}
				}
			}

			if (context.Adjectives.Count > 0 && context.Clause.Predicate == null)
			{
				context.Clause.Predicate = new Predicates.AdjectivePredicate (context.Verb, context.Adjectives.ToArray ());
				context.Verb = null;
				context.Adjectives.Clear ();
			}

			sentence.Clauses.Add(context.Clause);

			if (context.UnknownWords.Count > 0)
			{
				Console.WriteLine("prediction: sentence '" + value + "' contains too many unknowns and is unparseable");
			}
		}
	}
}

