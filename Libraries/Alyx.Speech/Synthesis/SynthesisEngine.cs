using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alyx.Speech.Synthesis
{
	/// <summary>
	/// Provides an abstraction layer for interfacing with compatible speech synthesis engines.
	/// </summary>
	public abstract class SynthesisEngine
	{
		public abstract SynthesisEngineReference MakeReference();
		
		public static SynthesisEngineReference[] GetEngines()
		{
			List<SynthesisEngineReference> list = new List<SynthesisEngineReference>();
			
			string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
			string[] assemblyFileNames = System.IO.Directory.GetFiles(path, "*.dll");
			foreach (string assemblyFileName in assemblyFileNames)
			{
				System.Reflection.Assembly asm = null;
				try
				{
					asm = System.Reflection.Assembly.LoadFile(assemblyFileName);
				}
				catch
				{
					continue;
				}
				
				Type[] types = null;
				try
				{
					types = asm.GetTypes();
				}
				catch (System.Reflection.ReflectionTypeLoadException ex)
				{
					types = ex.Types;
				}
				
				foreach (Type type in types)
				{
					if (type.IsSubclassOf(typeof(SynthesisEngine)))
					{
						SynthesisEngine engine = (type.Assembly.CreateInstance(type.FullName) as SynthesisEngine);
						SynthesisEngineReference er = engine.MakeReference();
						list.Add(er);
					}
				}
			}
			
			return list.ToArray();
		}
		
		public event SynthesisEngineStateChangedEventHandler StateChanged;
		protected virtual void OnStateChanged(SynthesisEngineStateChangedEventArgs e)
		{
			if (StateChanged != null) StateChanged(this, e);
		}

		public void Speak(string text)
		{
			SpeakInternal(text);
		}
		protected abstract void SpeakInternal(string text);

		public Voice[] GetVoices()
		{
			return GetVoicesInternal();
		}
		protected abstract Voice[] GetVoicesInternal();

		private Voice mvarVoice = null;
		public Voice Voice { get { return mvarVoice; } set { mvarVoice = value; SetVoiceInternal(value); } }

		public abstract void SetVoiceInternal(Voice voice);

		private SynthesisEngineState mvarState = SynthesisEngineState.Ready;
		public SynthesisEngineState State { get { return mvarState; } protected set { mvarState = value; } }

		public void WaitUntilDone()
		{
			while (true)
			{
				System.Threading.Thread.Sleep(500);
				if (State == SynthesisEngineState.Ready)
				{
					System.Threading.Thread.Sleep(500);
					break;
				}
			}
		}

		public Voice GetVoice(string name)
		{
			Voice[] voices = GetVoices();
			foreach (Voice voice in voices)
			{
				if (voice.Name == name) return voice;
			}
			return null;
		}
	}
}
