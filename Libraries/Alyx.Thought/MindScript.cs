using System;

namespace Alyx.Thought
{
	public abstract class MindScript
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

		public virtual int Interval => 500;

		public event EventHandler Initialized;
		protected virtual void OnInitialized(EventArgs e)
		{
			if (Initialized != null)
				Initialized (this, e);
		}

		public Mind Mind { get; private set; }
		public virtual bool DebugPrintChanges => true;

		protected virtual void ExecuteInternal ()
		{
		}

		private bool _initialized = false;

		public void Execute() 
		{
			if (!_initialized) {
				OnInitialized (EventArgs.Empty);
				_initialized = true;
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

