using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBS.Framework;

namespace Alyx.Thought
{
	public class Mind
	{
		public Idea CurrentIdea { get; set; } = null;
		public Idea.IdeaCollection Ideas { get; } = new Idea.IdeaCollection();
		public MindConnection.MindConnectionCollection Connections { get; } = new MindConnection.MindConnectionCollection();
		public MindScript.MindScriptCollection Scripts { get; } = null;

		public Mind()
		{
			Scripts = new MindScript.MindScriptCollection(this);
		}
		public bool EnableDebugging { get; set; } = false;

		private Dictionary<MindScript, System.Threading.Thread> _threads = new Dictionary<MindScript, System.Threading.Thread>();
		private void _thread_ParameterizedThreadStart(object parm)
		{
			MindScript script = parm as MindScript;
			if (script == null) return;

			while (true)
			{
				// check our status, process inputs, etc.
				script.Execute();

				// finally print our current status
				if (EnableDebugging)
					debug_PrintStatus();

				// and sleep for a while so we don't suck up CPU time
				System.Threading.Thread.Sleep(script.Interval);
			}
		}

		private void debug_PrintStatus()
		{
			debug_PrintCurrentIdea();
		}

		public void SendInput(string name, string value)
		{
			foreach (MindConnection connection in Connections)
			{
				if (connection.Input.Name == name)
				{
					connection.Output.Execute(value);
				}
			}
		}

		private void debug_PrintCurrentIdea()
		{
			Console.Write("DEBUG: current idea = ");
			if (CurrentIdea == null)
			{
				Console.Write("(none)");
			}
			else
			{
				Console.Write('\'');
				Console.Write(CurrentIdea.ToString());
				Console.Write('\'');
			}
			Console.WriteLine();
		}

		public void Start()
		{
			foreach (MindScript script in Scripts)
			{
				System.Threading.Thread thread = new System.Threading.Thread(_thread_ParameterizedThreadStart);
				_threads.Add(script, thread);

				thread.Start(script);
			}
		}
		public void Stop()
		{
			foreach (KeyValuePair<MindScript, System.Threading.Thread> kvp in _threads)
			{
				kvp.Key.Destroy();
				kvp.Value.Abort();
			}
			_threads.Clear();
		}

		public PropertyBag Properties { get; } = new PropertyBag();


		private Dictionary<string, List<Delegate>> eventHandlers = new Dictionary<string, List<Delegate>>();
		public void AttachEventHandler<TEventHandler>(string name, TEventHandler eventHandler) where TEventHandler : Delegate
		{
			if (!eventHandlers.ContainsKey(name))
			{
				eventHandlers[name] = new List<Delegate>();
			}
			eventHandlers[name].Add(eventHandler);
		}

		public void InvokeEvent(string name, EventArgs e)
		{
			if (eventHandlers.ContainsKey(name))
			{
				foreach (Delegate delg in eventHandlers[name])
				{
					delg.DynamicInvoke(new object[] { this, e });
				}
			}
		}
	}
}
