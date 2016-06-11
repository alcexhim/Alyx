using System;
using System.Collections.Generic;
using System.Text;

using UniversalEditor.ObjectModels.Markup;

namespace Alyx.Linguistics
{
	public class SentencePattern
	{
		public class SentencePatternCollection
			: System.Collections.ObjectModel.Collection<SentencePattern>
		{
			private Dictionary<Guid, SentencePattern> _itemsByID = new Dictionary<Guid, SentencePattern> ();

			public SentencePattern this[Guid id]
			{
				get
				{
					if (_itemsByID.ContainsKey (id)) {
						return _itemsByID [id];
					}
					return null;
				}
			}

			protected override void ClearItems ()
			{
				base.ClearItems ();
				_itemsByID.Clear ();
			}
			protected override void InsertItem (int index, SentencePattern item)
			{
				base.InsertItem (index, item);
				_itemsByID[item.ID] = item;
			}
			protected override void RemoveItem (int index)
			{
				if (index >= 0 && index < this.Count) {
					if (_itemsByID.ContainsKey (this [index].ID)) {
						_itemsByID.Remove (this [index].ID);
					}
				}
				base.RemoveItem (index);
			}
			protected override void SetItem (int index, SentencePattern item)
			{
				if (index >= 0 && index < this.Count) {
					if (_itemsByID.ContainsKey (this [index].ID)) {
						_itemsByID.Remove (this [index].ID);
					}
				}
				base.SetItem (index, item);
				_itemsByID[item.ID] = item;
			}
		}

		private Guid mvarID = Guid.Empty;
		public Guid ID { get { return mvarID; } set { mvarID = value; } }

		private WordPattern.WordPatternCollection mvarWordPatterns = new WordPattern.WordPatternCollection();
		public WordPattern.WordPatternCollection WordPatterns { get { return mvarWordPatterns; } }

		public static SentencePattern FromMarkup(MarkupTagElement tag, Language lang)
		{
			if (tag == null)
				return null;

			MarkupAttribute attID = tag.Attributes ["ID"];
			if (attID == null)
				return null;

			SentencePattern pattern = new SentencePattern ();
			pattern.ID = new Guid (attID.Value);

			foreach (MarkupElement elPart in tag.Elements)
			{
				MarkupTagElement tagPart = (elPart as MarkupTagElement);
				if (tagPart == null)
					continue;

				if (tagPart.FullName != "WordPattern")
					continue;

				MarkupAttribute attWordPatternID = tagPart.Attributes ["ID"];
				if (attWordPatternID == null)
					continue;

				WordPattern pat = lang.WordPatterns [new Guid (attWordPatternID.Value)];
				pattern.WordPatterns.Add (pat);
			}

			return pattern;
		}

		public override string ToString ()
		{
			StringBuilder sb = new StringBuilder ();
			foreach (WordPattern pattern in mvarWordPatterns)
			{
				sb.Append (pattern.ToString ());
			}
			return sb.ToString ();
		}

	}
}

