using System;
using System.Text;

namespace Alyx.Linguistics
{
	public class WordPatternPart
	{
		public class WordPatternPartCollection
			: System.Collections.ObjectModel.Collection<WordPatternPart>
		{
		}

		private WordClass mvarWordClass = null;
		public WordClass WordClass { get { return mvarWordClass; } set { mvarWordClass = value; } }

		internal Guid _WordPatternID = Guid.Empty;
		private WordPattern mvarWordPattern = null;
		public WordPattern WordPattern { get { return mvarWordPattern; } set { mvarWordPattern = value; } }

		private bool mvarIsOptional = false;
		public bool IsOptional { get { return mvarIsOptional; } set { mvarIsOptional = value; } }

		private bool mvarAllowMultiple = false;
		public bool AllowMultiple { get { return mvarAllowMultiple; } set { mvarAllowMultiple = value; } }

		public override string ToString ()
		{
			StringBuilder sb = new StringBuilder ();
			if (mvarIsOptional)
				sb.Append ('[');
			if (mvarWordClass != null) {
				sb.Append ('<');
				sb.Append (mvarWordClass.Title);
				sb.Append ('>');
			} else if (mvarWordPattern != null) {
				sb.Append ('(');
				sb.Append (mvarWordPattern.ToString ());
				sb.Append (')');
			}
			if (mvarAllowMultiple)
				sb.Append ('*');
			if (mvarIsOptional)
				sb.Append (']');
			return sb.ToString ();
		}
	}
}

