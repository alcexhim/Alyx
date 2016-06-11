using System;
using System.Collections.Generic;

using UniversalEditor.ObjectModels.Markup;

using Alyx.Linguistics;

namespace Alyx.Configuration
{
	public class Instance : ICloneable
	{
		public class InstanceCollection
			: System.Collections.ObjectModel.Collection<Instance>
		{
			private Dictionary<Guid, Instance> _itemsByID = new Dictionary<Guid, Instance> ();

			public Instance this[Guid id]
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
			protected override void InsertItem (int index, Instance item)
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
			protected override void SetItem (int index, Instance item)
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

		private Instance(Guid id) {
			mvarID = id;
		}

		private static Instance mvarGlobalInstance = new Instance(Guid.Empty);
		public static Instance GlobalInstance { get { return mvarGlobalInstance; } }

		private Guid mvarID = Guid.Empty;
		public Guid ID { get { return mvarID; } set { mvarID = value; } }

		private Language.LanguageCollection mvarLanguages = new Language.LanguageCollection();
		public Language.LanguageCollection Languages { get { return mvarLanguages; } }

		public static Instance Create()
		{
			Instance instance = new Instance (Guid.NewGuid ());
			return instance;
		}
		public static Instance FromMarkup(MarkupTagElement tag)
		{
			if (tag == null)
				return null;

			MarkupAttribute attID = tag.Attributes ["ID"];
			if (attID == null)
				return null;

			Instance instance = new Instance (new Guid (attID.Value));

			MarkupTagElement tagLanguages = (tag.Elements["Languages"] as MarkupTagElement);
			if (tagLanguages != null)
			{
				foreach (MarkupElement elLanguage in tagLanguages.Elements)
				{
					Language lang = Language.FromMarkup (elLanguage as MarkupTagElement);
					if (lang == null)
						continue;

					instance.Languages.Add (lang);
				}
			}

			return instance;
		}

		public object Clone()
		{
			Instance clone = Instance.Create();
			foreach (Language lang in mvarLanguages) {
				clone.Languages.Add (lang);
			}
			return clone;
		}
	}
}

