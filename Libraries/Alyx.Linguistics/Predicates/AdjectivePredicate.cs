using System;
using System.Text;

using Alyx.Linguistics.LanguageParts;

namespace Alyx.Linguistics.Predicates
{
	public class AdjectivePredicate : Predicate
	{
		private AdjectiveInstance.AdjectiveCollection mvarAdjectives = new AdjectiveInstance.AdjectiveCollection ();

		public AdjectiveInstance.AdjectiveCollection Adjectives { get { return mvarAdjectives; } }

		public AdjectivePredicate (VerbInstance verb, AdjectiveInstance[] adjectives) : base(verb)
		{
			for (int i = 0; i < adjectives.Length; i++)
			{
				mvarAdjectives.Add (adjectives [i]);
			}
		}

		public override string ToString ()
		{
			StringBuilder sb = new StringBuilder ();
			sb.Append (Verb.ToString ());
			if (mvarAdjectives.Count > 0) {
				sb.Append (' ');
				for (int i = 0; i < mvarAdjectives.Count; i++) {
					sb.Append (mvarAdjectives [i].ToString ());
				}
			}
			return sb.ToString ();
		}
	}
}

