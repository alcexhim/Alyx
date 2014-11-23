using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Recognition;
using System.Text;

namespace Alyx.Speech.Recognition.Engines.Windows
{
	public class WindowsEngine : Engine
	{
		private SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine();
		public DefaultEngine()
		{
			recognizer.LoadGrammar(new DictationGrammar());
			recognizer.SetInputToDefaultAudioDevice();

			recognizer.SpeechHypothesized += recognizer_SpeechHypothesized;
			recognizer.SpeechRecognitionRejected += recognizer_SpeechRecognitionRejected;
			recognizer.SpeechRecognized += recognizer_SpeechRecognized;
		}

		private void recognitionThread_ThreadStart()
		{
			while (true)
			{
				recognizer.Recognize();
			}
		}

		private System.Threading.Thread recognitionThread = null;
		public bool Listening
		{
			get { return (recognitionThread != null); }
		}

		protected override void StartInternal()
		{
			if (recognitionThread == null)
			{
				recognitionThread = new System.Threading.Thread(recognitionThread_ThreadStart);
				recognitionThread.Name = "Alyx_Speech_Recognition_RecognitionThread";
			}
			recognitionThread.Start();
		}
		protected override void StopInternal()
		{
			if (recognitionThread != null)
			{
				recognitionThread.Abort();
			}
			recognitionThread = null;
		}

		private static void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
		{
			Console.WriteLine("Alyx recognized word: " + e.Result.Text);
		}
		private static void recognizer_SpeechRecognitionRejected(object sender, SpeechRecognitionRejectedEventArgs e)
		{
		}
		private static void recognizer_SpeechHypothesized(object sender, SpeechHypothesizedEventArgs e)
		{
		}
	}
}
