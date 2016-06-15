using System;

namespace Alyx.Linguistics.Pronunciation
{
	public class PronunciationKeySyllableLengthComparer : System.Collections.Generic.IComparer<PronunciationKeySyllable>
	{
		#region IComparer implementation
		public int Compare (PronunciationKeySyllable x, PronunciationKeySyllable y)
		{
			return x.Value.Length.CompareTo (y.Value.Length);
		}
		#endregion

		private static PronunciationKeySyllableLengthComparer _Instance = new PronunciationKeySyllableLengthComparer();
		public static PronunciationKeySyllableLengthComparer Instance
		{
			get { return _Instance; }
		}
	}
}

