using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alyx.Thought
{
	public class Mind
	{
		private Idea mvarCurrentIdea = null;
		public Idea CurrentIdea { get { return mvarCurrentIdea; } set { mvarCurrentIdea = value; } }

		private Idea.IdeaCollection mvarIdeas = new Idea.IdeaCollection();
		public Idea.IdeaCollection Ideas { get { return mvarIdeas; } }

		private MindInput.MindInputCollection mvarInputs = new MindInput.MindInputCollection();
		public MindInput.MindInputCollection Inputs { get { return mvarInputs; } }

		private System.Threading.Thread _thread = null;
		private void _thread_ThreadStart()
		{
			while (true)
			{
				// check our status, process inputs, etc.
				foreach (MindInput input in mvarInputs)
				{
					input.Process ();
				}

				// finally print our current status
				debug_PrintStatus();

				// and sleep for a while so we don't suck up CPU time
				System.Threading.Thread.Sleep(500);
			}
		}

		private void debug_PrintStatus()
		{
			debug_PrintCurrentIdea();
		}

		private void debug_PrintCurrentIdea()
		{
			Console.Write("DEBUG: current idea = ");
			if (mvarCurrentIdea == null)
			{
				Console.Write("(none)");
			}
			else
			{
				Console.Write('\'');
				Console.Write(mvarCurrentIdea.ToString());
				Console.Write('\'');
			}
			Console.WriteLine();
		}

		public void Start()
		{
			if (_thread != null) _thread.Abort();
			
			_thread = new System.Threading.Thread(_thread_ThreadStart);
			_thread.Name = "Alyx Thought Mind Thread";
			_thread.Start();
		}
		public void Stop()
		{
			if (_thread == null) return;
			if (_thread.IsAlive)
			{
				_thread.Abort();
			}
			_thread = null;
		}
	}
}
