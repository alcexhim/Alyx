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
				if (clause.Subject is PronounInstance)
				{
					PronounInstance niSubject = (clause.Subject as PronounInstance);
					clause.Predicate.Verb.Person = niSubject.Person;
					clause.Predicate.Verb.Quantity = niSubject.Quantity;
				}
				else if (clause.Subject is NounInstance)
				{
					clause.Predicate.Verb.Person = Person.ThirdPerson;
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

			List<AdjectiveInstance> listAdjectives = new List<AdjectiveInstance>();
			Clause nextClause = new Clause();
			VerbInstance nextVerb = null;
			PrepositionInstance nextPrep = null;

			for (int i = 0; i < value.Length; i++)
			{
				if (value[i] == ' ')
				{
					next = sbNext.ToString();
					sbNext = new StringBuilder();
					ProcessWord(next, ref nextClause, ref listAdjectives, ref nextVerb, ref nextPrep);
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
				ProcessWord(next, ref nextClause, ref listAdjectives, ref nextVerb, ref nextPrep);

				sent.Clauses.Add(nextClause);
			}

			return sent;
		}

		private static bool ProcessWord(string next, ref Clause nextClause, ref List<AdjectiveInstance> listAdjectives, ref VerbInstance nextVerb, ref PrepositionInstance nextPrep)
		{
			Language lang = Language.CurrentLanguage;
			Word word = lang.Words[next];
			if (word == null)
			{
				Console.WriteLine("Unknown word '" + next.ToLower() + "'");
				return false;
			}

			// if word is an article...
			// if (article != null) ...

			AdjectiveInstance adj = lang.GetAdjective(word.ID);
			if (adj != null)
			{
				listAdjectives.Add(adj);
			}

			NounInstance noun = lang.GetNoun(word.ID);
			if (noun != null)
			{
				foreach (AdjectiveInstance adj1 in listAdjectives)
				{
					noun.Adjectives.Add(adj1);
				}
				listAdjectives.Clear();

				if (nextPrep != null)
				{
					nextClause.Predicate = new Predicates.PrepositionalObjectPredicate(nextVerb, nextPrep, noun);
				}
				else
				{
					nextClause.Subject = noun;
				}
			}

			VerbInstance verb = lang.GetVerb(word.ID);
			if (verb != null) nextVerb = verb;

			PrepositionInstance prep = lang.GetPreposition(word.ID);
			if (prep != null) nextPrep = prep;
			return true;
		}
	}
}
