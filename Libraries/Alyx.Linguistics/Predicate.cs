using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Alyx.Linguistics.LanguageParts;

namespace Alyx.Linguistics
{
	/// <summary>
	/// Indicates that an object can be passed as the predicate of a clause.
	/// </summary>
	public abstract class Predicate
	{
		private Verb mvarVerb = null;
		public Verb Verb { get { return mvarVerb; } set { mvarVerb = value; } }

		public Predicate(Verb verb)
		{
			mvarVerb = verb;
		}
	}
}
