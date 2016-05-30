using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alyx.Linguistics.LanguageParts;

namespace Alyx.Linguistics.Predicates
{
	public class IndirectObjectPredicate : DirectObjectPredicate
	{
		private ISubjectCollection mvarIndirectObjects = new ISubjectCollection();
		public ISubjectCollection IndirectObjects { get { return mvarIndirectObjects; } }

		public IndirectObjectPredicate(VerbInstance verb, ISubject[] subjects, ISubject[] indirectObjects)
			: base(verb, subjects)
		{
			foreach (ISubject indirectObject in indirectObjects)
			{
				mvarIndirectObjects.Add(indirectObject);
			}
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			if (Verb != null) {
				sb.Append (Verb.ToString ());
				sb.Append (' ');
			}
			
			Series series1 = new Series(mvarIndirectObjects);
			sb.Append(series1.ToString());

			sb.Append(' ');

			Series series2 = new Series(Subjects);
			sb.Append(series2.ToString());
			return sb.ToString();
		}
	}
}
