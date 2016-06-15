using System;

using UniversalEditor.ObjectModels.Markup;

namespace Alyx.Linguistics.Pronunciation
{
	public class PronunciationKey
	{
		public class PronunciationKeyCollection
			: System.Collections.ObjectModel.Collection<PronunciationKey>
		{
		}

		private Guid mvarID = Guid.Empty;
		public Guid ID { get { return mvarID; } set { mvarID = value; } }

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private string mvarPrefix = String.Empty;
		public string Prefix { get { return mvarPrefix; } set { mvarPrefix = value; } }

		private string mvarSuffix = String.Empty;
		public string Suffix { get { return mvarSuffix; } set { mvarSuffix = value; } }

		private PronunciationKeySyllable.PronunciationKeySyllableCollection mvarSyllables = new PronunciationKeySyllable.PronunciationKeySyllableCollection();
		public PronunciationKeySyllable.PronunciationKeySyllableCollection Syllables { get { return mvarSyllables; } }

		public static PronunciationKey FromMarkup(MarkupTagElement tag, PronunciationInformation pi)
		{
			if (tag == null)
				return null;
			if (tag.FullName != "PronunciationKey")
				return null;

			MarkupAttribute attID = tag.Attributes ["ID"];
			if (attID == null)
				return null;

			PronunciationKey key = new PronunciationKey ();
			key.ID = new Guid (attID.Value);

			MarkupAttribute attPrefix = tag.Attributes ["Prefix"];
			if (attPrefix != null)
				key.Prefix = attPrefix.Value;

			MarkupAttribute attSuffix = tag.Attributes ["Suffix"];
			if (attSuffix != null)
				key.Suffix = attSuffix.Value;

			MarkupTagElement tagInformation = (tag.Elements ["Information"] as MarkupTagElement);
			if (tagInformation != null) {
				MarkupTagElement tagTitle = (tagInformation.Elements["Title"] as MarkupTagElement);
				if (tagTitle != null) {
					key.Title = tagTitle.Value;
				}
			}

			MarkupTagElement tagSyllables = (tag.Elements ["Syllables"] as MarkupTagElement);
			if (tagSyllables != null) {
				foreach (MarkupElement elSyllable in tagSyllables.Elements) {
					MarkupTagElement tagSyllable = (elSyllable as MarkupTagElement);
					if (tagSyllable == null)
						continue;
					if (tagSyllable.FullName != "Syllable")
						continue;

					MarkupAttribute attSyllableID = tagSyllable.Attributes ["ID"];
					if (attSyllableID == null)
						continue;

					Guid syllableID = new Guid (attSyllableID.Value);
					MarkupAttribute attValue = tagSyllable.Attributes ["Value"];

					PronunciationKeySyllable syllable = new PronunciationKeySyllable ();
					syllable.SyllableID = syllableID;
					syllable.Syllable = pi.Syllables [syllableID];
					syllable.Value = attValue.Value;
					key.Syllables.Add (syllable);
				}
			}

			return key;
		}
	}
}

