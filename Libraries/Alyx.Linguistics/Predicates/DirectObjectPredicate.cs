using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alyx.Linguistics.LanguageParts;

namespace Alyx.Linguistics.Predicates
{
	public class DirectObjectPredicate : Predicate
	{
		private ISubjectCollection mvarSubjects = new ISubjectCollection();
		public ISubjectCollection Subjects { get { return mvarSubjects; } }

		public DirectObjectPredicate(VerbInstance verb, ISubject[] subjects) : base(verb)
		{
			foreach (ISubject subject in subjects)
			{
				mvarSubjects.Add(subject);
			}
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			if (Verb != null) {
				sb.Append (Verb.ToString ());
			} else {
				sb.Append ("[verb:null]");
			}
			sb.Append(' ');

			Series series = new Series();
			foreach (ISubject subject in mvarSubjects)
			{
				series.Words.Add(subject as WordInstance);
			}
			sb.Append(series.ToString());
			return sb.ToString();
		}
	}
}
