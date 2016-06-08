using System;

namespace Alyx.Thought
{
	public abstract class MindInput
	{
		public class MindInputCollection
			: System.Collections.ObjectModel.Collection<MindInput>
		{
		}

		protected abstract void ProcessInternal ();

		public void Process() 
		{
			ProcessInternal ();
		}
	}
}

