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
		private VerbInstance mvarVerb = null;
		public VerbInstance Verb { get { return mvarVerb; } set { mvarVerb = value; } }

		public Predicate(VerbInstance verb)
		{
			mvarVerb = verb;
		}
	}
}
