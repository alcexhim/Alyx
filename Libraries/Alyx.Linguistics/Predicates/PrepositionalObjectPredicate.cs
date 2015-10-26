using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alyx.Linguistics.LanguageParts;

namespace Alyx.Linguistics.Predicates
{
	public class PrepositionalObjectPredicate : Predicate
	{
		private PrepositionInstance mvarPreposition = null;
		public PrepositionInstance Preposition { get { return mvarPreposition; } set { mvarPreposition = value; } }

		private ISubjectCollection mvarSubjects = new ISubjectCollection();
		public ISubjectCollection Subjects { get { return mvarSubjects; } }

		public PrepositionalObjectPredicate(VerbInstance verb, PrepositionInstance preposition, ISubject[] subjects) : base(verb)
		{
			mvarPreposition = preposition;
			foreach (ISubject subject in subjects)
			{
				mvarSubjects.Add(subject);
			}
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			if (Verb != null)
			{
				sb.Append(Verb.ToString());
			}
			else
			{
				Console.WriteLine("Invalid clause: missing Verb component");
			}
			sb.Append(' ');
			sb.Append(Preposition.ToString());
			sb.Append(' ');
			
			Series series = new Series();
			foreach (ISubject subj in mvarSubjects)
			{
				series.Words.Add(subj as WordInstance);
			}

			sb.Append(series.ToString());
			return sb.ToString();
		}
	}
}
