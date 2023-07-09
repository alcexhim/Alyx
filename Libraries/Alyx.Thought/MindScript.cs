using System;
using MBS.Framework;

namespace Alyx.Thought
{
	public abstract class MindScript : Plugin
	{
		public class MindScriptCollection
			: System.Collections.ObjectModel.Collection<MindScript>
		{
			private Mind _Mind = null;
			public MindScriptCollection(Mind mind)
			{
				_Mind = mind;
			}

			protected override void InsertItem(int index, MindScript item)
			{
				base.InsertItem(index, item);
				item.Mind = _Mind;
			}
			protected override void RemoveItem(int index)
			{
				this[index].Mind = null;
				base.RemoveItem(index);
			}
			protected override void ClearItems()
			{
				for (int i = 0; i < Count; i++)
				{
					this[i].Mind = null;
				}
				base.ClearItems();
			}
		}

		public new abstract Guid ID { get; }
		public virtual int Interval => 500;

		public Mind Mind { get; private set; }
		public virtual bool DebugPrintChanges => true;

		protected virtual void ExecuteInternal ()
		{
		}

		protected virtual void OnInitialized(EventArgs e)
		{
		}

		protected override void InitializeInternal()
		{
			base.ID = this.ID;

			base.InitializeInternal();
			OnInitialized(EventArgs.Empty);
		}

		public void Execute() 
		{
			if (!Initialized) {
				Initialize();
			}

			ExecuteInternal ();
		}

		protected virtual void DestroyInternal()
		{
		}
		public void Destroy()
		{
			DestroyInternal();
		}
	}
}

