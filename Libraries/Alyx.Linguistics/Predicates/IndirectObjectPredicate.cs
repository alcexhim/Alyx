using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alyx.Linguistics.LanguageParts;

namespace Alyx.Linguistics.Predicates
{
	public class IndirectObjectPredicate : DirectObjectPredicate
	{
		private ISubject mvarIndirectObject = null;
		public ISubject IndirectObject { get { return mvarIndirectObject; } set { mvarIndirectObject = value; } }

		public IndirectObjectPredicate(VerbInstance verb, ISubject subject, ISubject indirectObject)
			: base(verb, subject)
		{
			mvarIndirectObject = indirectObject;
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(Verb.ToString());
			sb.Append(' ');
			sb.Append(IndirectObject.ToString());
			sb.Append(' ');
			sb.Append(Subject.ToString());
			return sb.ToString();
		}
	}
}
