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
					}
					else if (mapping.ID == SentenceTypes.Exclamatory.ID)
					{
						type = SentenceTypes.Exclamatory;
					}
					else if (mapping.ID == SentenceTypes.Imperative.ID)
					{
						type = SentenceTypes.Imperative;
					}
					else if (mapping.ID == SentenceTypes.Interrogative.ID)
					{
						type = SentenceTypes.Interrogative;
					}
				}
			}

			string next = null;
			StringBuilder sbNext = new StringBuilder();


			Sentence sent = new Sentence(type);

			for (int i = 0; i < value.Length; i++)
			{
				if (value[i] == ' ')
				{
					next = sbNext.ToString();
					sbNext = new StringBuilder();
				}
				else
				{
					sbNext.Append(value[i]);
				}
			}

			return sent;
		}
	}
}
