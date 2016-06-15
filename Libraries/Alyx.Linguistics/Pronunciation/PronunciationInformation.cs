using System;

using UniversalEditor.ObjectModels.Markup;

namespace Alyx.Linguistics.Pronunciation
{
	public class PronunciationInformation
	{
		private PronunciationKey.PronunciationKeyCollection mvarPronunciationKeys = new PronunciationKey.PronunciationKeyCollection ();
		public PronunciationKey.PronunciationKeyCollection PronunciationKeys { get { return mvarPronunciationKeys; } }

		private PronunciationSyllable.PronunciationSyllableCollection mvarSyllables = new PronunciationSyllable.PronunciationSyllableCollection ();
		public PronunciationSyllable.PronunciationSyllableCollection Syllables { get { return mvarSyllables; } }

		public void LoadMarkup(MarkupTagElement tag, bool append = false)
		{
			if (tag == null)
				return;
			if (tag.FullName != "Pronunciation")
				return;

			if (!append) {
				mvarPronunciationKeys.Clear ();
				mvarSyllables.Clear ();
			}

			MarkupTagElement tagSyllables = (tag.Elements ["Syllables"] as MarkupTagElement);
			if (tagSyllables != null)
			{
				foreach (MarkupElement elSyllable in tagSyllables.Elements)
				{
					PronunciationSyllable syllable = PronunciationSyllable.FromMarkup (elSyllable as MarkupTagElement);

					if (syllable != null)
						mvarSyllables.Add (syllable);
				}
			}

			MarkupTagElement tagPronunciationKeys = (tag.Elements ["PronunciationKeys"] as MarkupTagElement);
			if (tagPronunciationKeys != null)
			{
				foreach (MarkupElement elPronunciationKey in tagPronunciationKeys.Elements)
				{
					PronunciationKey key = PronunciationKey.FromMarkup (elPronunciationKey as MarkupTagElement, this);
					if (key != null)
						mvarPronunciationKeys.Add (key);
				}
			}
		}

		public static PronunciationInformation FromMarkup(MarkupTagElement tag)
		{
			if (tag == null)
				return null;
			if (tag.FullName != "Pronunciation")
				return null;

			PronunciationInformation pi = new PronunciationInformation ();
			pi.LoadMarkup (tag);
			return pi;
		}
	}
}

