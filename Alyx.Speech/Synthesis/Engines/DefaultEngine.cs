using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;

namespace Alyx.Speech.Synthesis.Engines
{
	/// <summary>
	/// Provides an implementation of a speech synthesis engine using the default .NET System.Speech
	/// implementation.
	/// </summary>
	public class DefaultEngine : Engine
	{
		private SpeechSynthesizer synthesizer = new SpeechSynthesizer();

		public DefaultEngine()
		{
			synthesizer.StateChanged += synthesizer_StateChanged;
			synthesizer.Rate = 2;
		}

		private void synthesizer_StateChanged(object sender, System.Speech.Synthesis.StateChangedEventArgs e)
		{
			switch (e.State)
			{
				case SynthesizerState.Paused:
				{
					State = EngineState.Paused;
					OnStateChanged(new EngineStateChangedEventArgs(EngineState.Paused));
					break;
				}
				case SynthesizerState.Ready:
				{
					State = EngineState.Ready;
					OnStateChanged(new EngineStateChangedEventArgs(EngineState.Ready));
					break;
				}
				case SynthesizerState.Speaking:
				{
					State = EngineState.Speaking;
					OnStateChanged(new EngineStateChangedEventArgs(EngineState.Speaking));
					break;
				}
			}
		}

		protected override void SpeakInternal(string text)
		{
			synthesizer.SpeakAsync(text);
		}

		protected override Voice[] GetVoicesInternal()
		{
			List<Voice> voices = new List<Voice>();

			System.Collections.ObjectModel.ReadOnlyCollection<InstalledVoice> realvoices = synthesizer.GetInstalledVoices();
			foreach (InstalledVoice voice in realvoices)
			{
				if (voice.VoiceInfo.SupportedAudioFormats.Count == 0)
				{
					voices.Add(new Voice(voice.VoiceInfo.Name, false));
				}
				else
				{
					voices.Add(new Voice(voice.VoiceInfo.Name));
				}
			}
			return voices.ToArray();
		}
		public override void SetVoiceInternal(Voice voice)
		{
			synthesizer.SelectVoice(voice.Name);
		}
	}
}
