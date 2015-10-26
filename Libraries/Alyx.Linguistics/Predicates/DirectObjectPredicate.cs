using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alyx.Linguistics.LanguageParts;

namespace Alyx.Linguistics.Predicates
{
	public class DirectObjectPredicate : Predicate
	{
		private ISubject mvarSubject = null;
		public ISubject Subject { get { return mvarSubject; } set { mvarSubject = value; } }

		public DirectObjectPredicate(VerbInstance verb, ISubject subject) : base(verb)
		{
			mvarSubject = subject;
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(Verb.ToString());
			sb.Append(' ');
			sb.Append(Subject.ToString());
			return sb.ToString();
		}
	}
}
