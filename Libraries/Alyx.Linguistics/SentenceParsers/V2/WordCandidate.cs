using System;
using System.Text;

namespace Alyx.Linguistics.SentenceParsers.V2
{
	public class WordCandidate
	{
		private bool mvarSeries = false;
		public bool Series { get { return mvarSeries; } set { mvarSeries = value; } }

		private string mvarValue = String.Empty;
		public string Value { get { return mvarValue; } set { mvarValue = value; } }

		private WordInstance.WordInstanceCollection mvarInstances = new WordInstance.WordInstanceCollection();
		public WordInstance.WordInstanceCollection Instances { get { return mvarInstances; } }

		private WordInstance mvarPreferredInstance = null;
		public WordInstance PreferredInstance { get { return mvarPreferredInstance; } set { mvarPreferredInstance = value; } }

		public WordCandidate(string value) {
			mvarValue = value;
		}

		public override string ToString ()
		{
			StringBuilder sb = new StringBuilder ();
			sb.Append ('[');
			sb.Append (mvarValue);
			sb.Append ("] ");
			if (mvarSeries)
				sb.Append ("<Series>");
			sb.Append(": ");
			for (int i = 0; i < mvarInstances.Count; i++) {
				sb.Append (mvarInstances [i].ToString ());
				if (i < mvarInstances.Count - 1)
					sb.Append (", ");
			}
			if (mvarPreferredInstance != null) {
				sb.Append (" (");
				sb.Append (mvarPreferredInstance.ToString ());
				sb.Append (')');
			}
			return sb.ToString ();
		}
	}
}

