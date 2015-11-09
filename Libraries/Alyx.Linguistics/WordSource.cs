using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alyx.Linguistics
{
	public class WordSource
	{
		public class WordSourceCollection
			: System.Collections.ObjectModel.Collection<WordSource>
		{
			private System.Collections.Generic.Dictionary<Guid, WordSource> itemsByGuid = new Dictionary<Guid, WordSource>();

			protected override void ClearItems()
			{
				base.ClearItems();
				itemsByGuid.Clear();
			}
			protected override void InsertItem(int index, WordSource item)
			{
				base.InsertItem(index, item);
				if (item == null) return;

				itemsByGuid[item.ID] = item;
			}
			protected override void RemoveItem(int index)
			{
				if (itemsByGuid.ContainsKey(this[index].ID))
				{
					itemsByGuid.Remove(this[index].ID);
				}
				base.RemoveItem(index);
			}
			protected override void SetItem(int index, WordSource item)
			{
				if (itemsByGuid.ContainsKey(this[index].ID))
				{
					itemsByGuid.Remove(this[index].ID);
				}
				base.SetItem(index, item);
				itemsByGuid[item.ID] = item;
			}

			public WordSource this[Guid id]
			{
				get
				{
					if (itemsByGuid.ContainsKey(id)) return itemsByGuid[id];
					return null;
				}
			}

		}

		private Guid mvariD = Guid.Empty;
		public Guid ID { get { return mvariD; } set { mvariD = value; } }

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		public override string ToString()
		{
			return mvarTitle;
		}
	}
}
