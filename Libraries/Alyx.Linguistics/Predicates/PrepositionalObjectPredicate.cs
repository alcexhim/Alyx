using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alyx.Linguistics.LanguageParts;

namespace Alyx.Linguistics.Predicates
{
	public class PrepositionalObjectPredicate : Predicate
	{
		private Preposition mvarPreposition = null;
		public Preposition Preposition { get { return mvarPreposition; } set { mvarPreposition = value; } }

		private Noun mvarSubject = null;
		public Noun Subject { get { return mvarSubject; } set { mvarSubject = value; } }

		public PrepositionalObjectPredicate(Verb verb, Preposition preposition, Noun subject) : base(verb)
		{
			mvarPreposition = preposition;
			mvarSubject = subject;
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(Verb.ToString());
			sb.Append(' ');
			sb.Append(Preposition.ToString());
			sb.Append(' ');
			sb.Append(Subject.ToString());
			return sb.ToString();
		}
	}
}
