using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alyx.Linguistics.LanguageParts;

namespace Alyx.Linguistics
{
	public class Clause
	{
		public class ClauseCollection
			: System.Collections.ObjectModel.Collection<Clause>
		{

		}

		public Clause()
		{

		}
		public Clause(ISubject[] subjects, Predicate predicate)
		{
			foreach (ISubject subject in subjects)
			{
				mvarSubjects.Add(subject);
			}
			mvarPredicate = predicate;
		}

		private ISubjectCollection mvarSubjects = new ISubjectCollection();
		public ISubjectCollection Subjects { get { return mvarSubjects; } }

		private Predicate mvarPredicate = null;
		public Predicate Predicate { get { return mvarPredicate; } set { mvarPredicate = value; } }

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();

			Series series = new Series(mvarSubjects);
			sb.Append(series.ToString());

			if (mvarPredicate != null)
			{
				sb.Append(" ");
				sb.Append(mvarPredicate.ToString());
			}
			return sb.ToString();
		}
	}
}
