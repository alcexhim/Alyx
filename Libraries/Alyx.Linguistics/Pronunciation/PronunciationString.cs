using System;
using System.Collections.Generic;
using System.Text;

namespace Alyx.Linguistics.Pronunciation
{
	/// <summary>
	/// Represents a pronunciation hint for 
	/// </summary>
	public class PronunciationString
	{
		public class PronunciationStringCollection
			: System.Collections.ObjectModel.Collection<PronunciationString>
		{
		}

		private PronunciationSyllable.PronunciationSyllableCollection mvarSyllables = new PronunciationSyllable.PronunciationSyllableCollection();
		public PronunciationSyllable.PronunciationSyllableCollection Syllables { get { return mvarSyllables; } }

		/// <summary>
		/// Parses an IPA string.
		/// </summary>
		/// <param name="value">Value.</param>
		public static PronunciationString Parse(string value, PronunciationKey key = null)
		{
			if (key == null)
				key = PronunciationKeys.IPA;
			
			List<PronunciationKeySyllable> listSyllables = new List<PronunciationKeySyllable> (key.Syllables);
			listSyllables.Sort (PronunciationKeySyllableLengthComparer.Instance);

			PronunciationString str = new PronunciationString ();
			for (int i = 0; i < value.Length; i++) {
				foreach (PronunciationKeySyllable syllable in listSyllables) {
					if ((i + syllable.Value.Length - 1) < value.Length) {
						if (value.Substring (i, syllable.Value.Length) == syllable.Value) {
							str.Syllables.Add (syllable.Syllable);
							i += syllable.Value.Length - 1;
							continue;
						}
					}
				}
			}
			return str;
		}

		public override string ToString ()
		{
			return ToString (PronunciationKeys.IPA);
		}
		public string ToString(PronunciationKey key)
		{
			StringBuilder sb = new StringBuilder ();
			sb.Append (key.Prefix);
			foreach (PronunciationSyllable syllable in mvarSyllables) {
				sb.Append(key.Syllables [syllable.ID].Value);
			}
			sb.Append (key.Suffix);
			return sb.ToString ();
		}
	}
}

