using System;

using UniversalEditor.ObjectModels.Markup;

namespace Alyx.Linguistics.Pronunciation
{
	public class PronunciationSyllable
	{
		public class PronunciationSyllableCollection
			: System.Collections.ObjectModel.Collection<PronunciationSyllable>
		{
			public PronunciationSyllable this[Guid id]
			{
				get {
					foreach (PronunciationSyllable syllable in this) {
						if (syllable.ID == id)
							return syllable;
					}
					return null;
				}
			}
		}

		private Guid mvarID = Guid.Empty;
		public Guid ID { get { return mvarID; } set { mvarID = value; } }

		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

		public override string ToString ()
		{
			return mvarName;
		}

		public static PronunciationSyllable FromMarkup(MarkupTagElement tag)
		{
			if (tag == null)
				return null;
			if (tag.FullName != "Syllable")
				return null;

			MarkupAttribute attID = tag.Attributes ["ID"];
			if (attID == null)
				return null;

			PronunciationSyllable syllable = new PronunciationSyllable ();

			syllable.ID = new Guid (attID.Value);

			MarkupAttribute attName = tag.Attributes ["Name"];
			if (attName != null)
				syllable.Name = attName.Value;

			return syllable;
		}

	}
}

