using System;
using System.Text;

using UniversalEditor.ObjectModels.Markup;

namespace Alyx.Linguistics.Pronunciation
{
	public class PronunciationKeySyllable
	{
		public class PronunciationKeySyllableCollection
			: System.Collections.ObjectModel.Collection<PronunciationKeySyllable>
		{

			private System.Collections.Generic.Dictionary<Guid, PronunciationKeySyllable> _itemsBySyllableID = new System.Collections.Generic.Dictionary<Guid, PronunciationKeySyllable>();

			public PronunciationKeySyllable this[Guid syllableID]
			{
				get {
					return _itemsBySyllableID [syllableID];
				}
			}

			protected override void ClearItems ()
			{
				base.ClearItems ();
				_itemsBySyllableID.Clear ();
			}
			protected override void InsertItem (int index, PronunciationKeySyllable item)
			{
				base.InsertItem (index, item);
				_itemsBySyllableID [item.SyllableID] = item;
			}
			protected override void RemoveItem (int index)
			{
				if (index >= 0 && index < this.Count - 1) {
					_itemsBySyllableID.Remove (this [index].SyllableID);
				}
				base.RemoveItem (index);
			}
			protected override void SetItem (int index, PronunciationKeySyllable item)
			{
				if (index >= 0 && index < this.Count - 1) {
					_itemsBySyllableID.Remove (this [index].SyllableID);
				}
				base.SetItem (index, item);
				_itemsBySyllableID [item.SyllableID] = item;
			}

		}

		private PronunciationSyllable mvarSyllable = null;
		public PronunciationSyllable Syllable { get { return mvarSyllable; } set { mvarSyllable = value; } }

		private Guid mvarSyllableID = Guid.Empty;
		public Guid SyllableID {
			get {
				if (mvarSyllable != null) {
					return mvarSyllable.ID;
				}
				else {
					return mvarSyllableID;
				}
			}
			set {
				mvarSyllableID = value;
			}
		}

		private string mvarValue = String.Empty;
		public string Value { get { return mvarValue; } set { mvarValue = value; } }

		public override string ToString ()
		{
			StringBuilder sb = new StringBuilder ();
			sb.Append (mvarSyllable.ToString ());
			sb.Append (" : ");
			sb.Append (mvarValue);
			return sb.ToString ();
		}
	}
}

