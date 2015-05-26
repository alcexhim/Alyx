using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
		public Clause(ISubject subject, IPredicate predicate)
		{
			mvarSubject = subject;
			mvarPredicate = predicate;
		}

		private ISubject mvarSubject = null;
		public ISubject Subject { get { return mvarSubject; } set { mvarSubject = value; } }

		private IPredicate mvarPredicate = null;
		public IPredicate Predicate { get { return mvarPredicate; } set { mvarPredicate = value; } }
	}
}
