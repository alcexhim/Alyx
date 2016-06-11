using System;
using System.Collections.Generic;
using System.Text;

using UniversalEditor.ObjectModels.Markup;

namespace Alyx.Linguistics
{
	public class WordPattern
	{
		public class WordPatternCollection
			: System.Collections.ObjectModel.Collection<WordPattern>
		{
			private Dictionary<Guid, WordPattern> _itemsByID = new Dictionary<Guid, WordPattern> ();

			public WordPattern this[Guid id]
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
			protected override void InsertItem (int index, WordPattern item)
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
			protected override void SetItem (int index, WordPattern item)
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

		private WordPatternPart.WordPatternPartCollection mvarParts = new WordPatternPart.WordPatternPartCollection();
		public WordPatternPart.WordPatternPartCollection Parts { get { return mvarParts; } }

		public override string ToString ()
		{
			StringBuilder sb = new StringBuilder ();
			foreach (WordPatternPart part in mvarParts) {
				if (part == null) {
					sb.Append ("(null)");
				} else {
					sb.Append (part.ToString ());
				}

				if (mvarParts.IndexOf (part) < mvarParts.Count - 1) {
					sb.Append (' ');
				}
			}
			return sb.ToString ();
		}

		public static WordPattern FromMarkup(MarkupTagElement tag, Language language = null)
		{
			if (language == null)
				language = Language.CurrentLanguage;

			if (language == null)
				return null;

			if (tag == null)
				return null;

			MarkupAttribute attID = tag.Attributes ["ID"];
			if (attID == null)
				return null;

			WordPattern pattern = new WordPattern ();
			pattern.ID = new Guid (attID.Value);

			foreach (MarkupElement el1 in tag.Elements)
			{
				MarkupTagElement tag1 = (el1 as MarkupTagElement);
				if (tag1 == null)
					continue;
				if (!tag1.FullName.Equals("PatternPart"))
					continue;

				MarkupAttribute attWordClassID = tag1.Attributes ["WordClassID"];

				bool optional = false, multiple = false;

				MarkupAttribute attOptional = tag1.Attributes ["Optional"];
				optional = (attOptional != null && attOptional.Value.ToLower ().Equals ("true"));

				MarkupAttribute attMultiple = tag1.Attributes ["Multiple"];
				multiple = (attMultiple != null && attMultiple.Value.ToLower ().Equals ("true"));

				MarkupAttribute attWordPatternID = tag1.Attributes ["WordPatternID"];
				
				WordPatternPart part = new WordPatternPart ();
				part.IsOptional = optional;
				part.AllowMultiple = multiple;

				if (attWordClassID != null) {
					part.WordClass = language.WordClasses [new Guid (attWordClassID.Value)];
				} else if (attWordPatternID != null) {
					part._WordPatternID = new Guid (attWordPatternID.Value);
				} else {
					continue;
				}
				pattern.Parts.Add (part);
			}

			return pattern;
		}
		
	}
}

