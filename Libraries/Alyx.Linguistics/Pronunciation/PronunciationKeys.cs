using System;

namespace Alyx.Linguistics.Pronunciation
{
	public class PronunciationKeys
	{
		private static PronunciationKey mvarIPA = null;
		public static PronunciationKey IPA {
			get {
				return mvarIPA;
			}
			set {
				mvarIPA = value; 
			}
		}
	}
}

