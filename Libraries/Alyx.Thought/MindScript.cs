using System;

namespace Alyx.Thought
{
	public abstract class MindScript
	{
		public class MindScriptCollection
			: System.Collections.ObjectModel.Collection<MindScript>
		{
		}

		public event EventHandler Initialized;
		protected virtual void OnInitialized(EventArgs e)
		{
			if (Initialized != null)
				Initialized (this, e);
		}

		protected virtual void ProcessInternal ()
		{
		}

		private bool _initialized = false;

		public void Execute() 
		{
			if (!_initialized) {
				OnInitialized (EventArgs.Empty);
				_initialized = true;
			}

			ProcessInternal ();
		}
	}
}

