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

		private MindConnection.MindConnectionCollection mvarConnections = new MindConnection.MindConnectionCollection ();
		public MindConnection.MindConnectionCollection Connections { get { return mvarConnections; } }

		private MindScript.MindScriptCollection mvarScripts = null;
		public MindScript.MindScriptCollection Scripts { get { return mvarScripts; } }

		public Mind()
		{
			mvarScripts = new MindScript.MindScriptCollection(this);
		}

		private bool mvarEnableDebugging = false;
		public bool EnableDebugging { get { return mvarEnableDebugging; } set { mvarEnableDebugging = value; } }

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
				if (mvarEnableDebugging)
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

		private Dictionary<Guid, string> _PropertyNames = new Dictionary<Guid, string>();
		public string GetPropertyName(Guid id)
		{
			if (_PropertyNames.ContainsKey(id))
				return _PropertyNames[id];
			return null;
		}
		public void SetPropertyName(Guid id, string name)
		{
			_PropertyNames[id] = name;
		}

		private Dictionary<Guid, object> _PropertyValues = new Dictionary<Guid, object>();
		public bool HasPropertyValue(Guid id)
		{
			return _PropertyValues.ContainsKey(id);
		}
		public bool SetPropertyValue<T>(Guid id, T value)
		{
			bool changed = false;
			lock (_PropertyValues)
			{
				object oldValue = null;
				if (!_PropertyValues.ContainsKey(id) || (!(
						(_PropertyValues[id] == null && (value as object) == null) ||
						(_PropertyValues[id] != null && _PropertyValues[id].Equals(value)))))
				{
					changed = true;
				}
				if (_PropertyValues.ContainsKey(id))
				{
					oldValue = _PropertyValues[id];
				}

				if (changed)
				{
					PropertyValueChangingEventArgs e = new PropertyValueChangingEventArgs(id, oldValue, value);
					OnPropertyValueChanging(e);
					if (e.Cancel)
					{
						return false;
					}
				}
				_PropertyValues[id] = value;
				if (changed)
				{
					OnPropertyValueChanged(new PropertyValueChangedEventArgs(id, oldValue, value));
				}
			}
			return changed;
		}

		public event EventHandler<PropertyValueChangingEventArgs> PropertyValueChanging;
		protected virtual void OnPropertyValueChanging(PropertyValueChangingEventArgs e)
		{
			PropertyValueChanging?.Invoke(this, e);
		}
		public event EventHandler<PropertyValueChangedEventArgs> PropertyValueChanged;
		protected virtual void OnPropertyValueChanged(PropertyValueChangedEventArgs e)
		{
			PropertyValueChanged?.Invoke(this, e);
		}

		public T GetPropertyValue<T>(Guid id, T defaultValue = default(T))
		{
			lock (_PropertyValues)
			{
				if (_PropertyValues.ContainsKey(id))
				{
					if (_PropertyValues[id] is T val)
					{
						return val;
					}
				}
			}
			return defaultValue;
		}

		private Dictionary<string, List<Delegate>> eventHandlers = new Dictionary<string, List<Delegate>>();
		public void AttachEventHandler<TEventHandler>(string name, TEventHandler eventHandler) where TEventHandler : Delegate
		{
			if (!eventHandlers.ContainsKey(name))
			{
				eventHandlers[name] = new List<Delegate>();
			}
			eventHandlers[name].Add(eventHandler);
		}

		public IEnumerable<KeyValuePair<Guid, object>> GetPropertyValues()
		{
			return _PropertyValues;
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
