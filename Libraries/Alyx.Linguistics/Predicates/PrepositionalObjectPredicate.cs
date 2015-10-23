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

		private NounInstance mvarSubject = null;
		public NounInstance Subject { get { return mvarSubject; } set { mvarSubject = value; } }

		public PrepositionalObjectPredicate(VerbInstance verb, PrepositionInstance preposition, NounInstance subject) : base(verb)
		{
			mvarPreposition = preposition;
			mvarSubject = subject;
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
			sb.Append(Subject.ToString());
			return sb.ToString();
		}
	}
}
