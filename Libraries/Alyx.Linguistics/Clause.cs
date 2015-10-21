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
		public Clause(INounInstance subject, Predicate predicate)
		{
			mvarSubject = subject;
			mvarPredicate = predicate;
		}

		private INounInstance mvarSubject = null;
		public INounInstance Subject { get { return mvarSubject; } set { mvarSubject = value; } }

		private Predicate mvarPredicate = null;
		public Predicate Predicate { get { return mvarPredicate; } set { mvarPredicate = value; } }

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			if (mvarSubject != null)
			{
				sb.Append(mvarSubject.ToString());
			}
			if (mvarPredicate != null)
			{
				sb.Append(" ");
				sb.Append(mvarPredicate.ToString());
			}
			return sb.ToString();
		}
	}
}
