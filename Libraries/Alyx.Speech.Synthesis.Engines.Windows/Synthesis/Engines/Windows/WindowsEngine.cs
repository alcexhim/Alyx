using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;

namespace Alyx.Speech.Synthesis.Engines.Windows
{
	/// <summary>
	/// Provides an implementation of a speech synthesis engine using the default .NET System.Speech
	/// implementation.
	/// </summary>
	public class WindowsEngine : SynthesisEngine
	{
		private SpeechSynthesizer synthesizer = new SpeechSynthesizer();

		public WindowsEngine()
		{
			synthesizer.StateChanged += synthesizer_StateChanged;
			synthesizer.Rate = 1;
		}

		private void synthesizer_StateChanged(object sender, System.Speech.Synthesis.StateChangedEventArgs e)
		{
			switch (e.State)
			{
				case SynthesizerState.Paused:
				{
					State = SynthesisEngineState.Paused;
					OnStateChanged(new SynthesisEngineStateChangedEventArgs(SynthesisEngineState.Paused));
					break;
				}
				case SynthesizerState.Ready:
				{
					State = SynthesisEngineState.Ready;
					OnStateChanged(new SynthesisEngineStateChangedEventArgs(SynthesisEngineState.Ready));
					break;
				}
				case SynthesizerState.Speaking:
				{
					State = SynthesisEngineState.Speaking;
					OnStateChanged(new SynthesisEngineStateChangedEventArgs(SynthesisEngineState.Speaking));
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
					voices.Add(new Voice(voice.VoiceInfo.Name));
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
