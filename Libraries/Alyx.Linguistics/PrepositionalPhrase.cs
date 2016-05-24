using System;
using System.Text;

using Alyx.Linguistics.LanguageParts;

namespace Alyx.Linguistics
{
	public class PrepositionalPhrase
	{
		private PrepositionInstance mvarPreposition = null;
		public PrepositionInstance Preposition { get { return mvarPreposition; } set { mvarPreposition = value; } }

		private ISubjectCollection mvarSubjects = new ISubjectCollection();
		public ISubjectCollection Subjects { get { return mvarSubjects; } }

		public PrepositionalPhrase(PrepositionInstance preposition, ISubject[] subjects)
		{
			mvarPreposition = preposition;
			for (int i = 0; i < subjects.Length; i++) {
				mvarSubjects.Add (subjects [i]);
			}
		}

		public override string ToString ()
		{
			StringBuilder sb = new StringBuilder ();
			if (mvarPreposition != null) {
				sb.Append (mvarPreposition.ToString ());
			}
			if (mvarSubjects.Count > 0) {
				sb.Append (' ');
				Series series = new Series (mvarSubjects);
				sb.Append (series.ToString ());
			}
			return sb.ToString ();
		}

	}
}

