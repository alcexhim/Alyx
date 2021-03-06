﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Alyx.Linguistics.LanguageParts;

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
						if (clause.Predicate != null) {
							if (clause.Predicate.Verb != null) {
								clause.Predicate.Verb.Person = Person.ThirdPerson;
							}
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

		public static Sentence Parse(string value, SentenceParser parser = null)
		{
			if (parser == null) parser = Language.CurrentLanguage.SentenceParser;
			return parser.Parse (value);
		}
	}
}
