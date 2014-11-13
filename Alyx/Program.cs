using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alyx
{
	static class Program
	{
		public static SpeechSynthesizer synthesizer = new SpeechSynthesizer();
		public static SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine();

		private static NotifyIcon nid = new NotifyIcon();
		private static bool exiting = false;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			nid.Text = "Alyx";
			nid.ContextMenu = BuildContextMenu();

			nid.Icon = Properties.Resources.Alyx_Tray_Default;
			nid.Visible = true;

			recognizer.LoadGrammar(new DictationGrammar());

			recognizer.SetInputToDefaultAudioDevice();
			recognizer.SpeechHypothesized += recognizer_SpeechHypothesized;
			recognizer.SpeechRecognitionRejected += recognizer_SpeechRecognitionRejected;
			recognizer.SpeechRecognized += recognizer_SpeechRecognized;

			// synthesizer.SelectVoice("Microsoft Zira Desktop");

			synthesizer.StateChanged += synthesizer_StateChanged;
			synthesizer.Rate = 2;
			synthesizer.Speak("Hello Michael. This is Alix. I am ready for your command.");

			recognitionThread.Name = "RecognitionThread";
			recognitionThread.Start();

			Application.Run();

			synthesizer.Speak("See you later Michael");

			nid.Visible = false;
		}

		private static System.Threading.Thread recognitionThread = new System.Threading.Thread(recognitionThread_ThreadStart);

		private static void recognitionThread_ThreadStart()
		{
			while (true)
			{
				recognizer.Recognize();
			}
		}

		private static ContextMenu BuildContextMenu()
		{
			ContextMenu menu = new ContextMenu();

			MenuItem menuVoice = new MenuItem("&Voice");
			foreach (InstalledVoice voice in synthesizer.GetInstalledVoices())
			{
				MenuItem mi = new MenuItem(voice.VoiceInfo.Name, menuVoice_Click);
				mi.Tag = voice;
				menuVoice.MenuItems.Add(mi);
			}
			menu.MenuItems.Add(menuVoice);

			menu.MenuItems.Add(new MenuItem("-"));
			menu.MenuItems.Add(new MenuItem("E&xit", mnuTrayExit_Click));
			return menu;
		}

		private static void menuVoice_Click(object sender, EventArgs e)
		{
			MenuItem mi = (sender as MenuItem);
			InstalledVoice voice = (mi.Tag as InstalledVoice);
			synthesizer.SelectVoice(voice.VoiceInfo.Name);

			synthesizer.Speak("Hello, Michael. I am " + voice.VoiceInfo.Name + ".");
		}

		private static void mnuTrayExit_Click(object sender, EventArgs e)
		{
			recognitionThread.Abort();
			Application.Exit();
		}

		private static void synthesizer_StateChanged(object sender, System.Speech.Synthesis.StateChangedEventArgs e)
		{
			switch (e.State)
			{
				case SynthesizerState.Speaking:
				{
					nid.Icon = Properties.Resources.Alyx_Tray_Speaking;
					break;
				}
				case SynthesizerState.Ready:
				{
					nid.Icon = Properties.Resources.Alyx_Tray_Default;
					break;
				}
			}
		}

		private static void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
		{
			Console.WriteLine(e.Result.Text);
		}
		private static void recognizer_SpeechRecognitionRejected(object sender, SpeechRecognitionRejectedEventArgs e)
		{
		}
		private static void recognizer_SpeechHypothesized(object sender, SpeechHypothesizedEventArgs e)
		{
		}
	}
}
