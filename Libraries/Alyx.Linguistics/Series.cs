using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alyx.Linguistics
{
	/// <summary>
	/// Formats a series of <see cref="Word" />s using the rules defined in the current <see cref="Language" />.
	/// </summary>
	public class Series : ISubject
	{
		private WordInstance.WordInstanceCollection mvarWords = new WordInstance.WordInstanceCollection();
		/// <summary>
		/// An array of <see cref="Word" />s to format.
		/// </summary>
		public WordInstance.WordInstanceCollection Words { get { return mvarWords; } set { mvarWords = value; } }

		public Series(WordInstance[] words = null)
		{
			if (words != null)
			{
				foreach (WordInstance inst in words)
				{
					mvarWords.Add(inst);
				}
			}
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < mvarWords.Count; i++)
			{
				if (i == mvarWords.Count - 1)
				{
					if (Language.CurrentLanguage.EnableOxfordComma) sb.Append(',');
					sb.Append(' ');
					sb.Append("and");
					sb.Append(' ');
				}
				sb.Append(mvarWords[i]);

				if (i < mvarWords.Count - 2)
				{
					sb.Append(',');
					sb.Append(' ');
				}
			}
			return sb.ToString();
		}
	}
}
