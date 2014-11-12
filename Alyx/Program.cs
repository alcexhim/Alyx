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
		public static SpeechRecognizer recognizer = null;

		private static NotifyIcon nid = new NotifyIcon();

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			nid.Text = "Alyx";
			nid.ContextMenu = new ContextMenu(new MenuItem[]
			{
				new MenuItem("E&xit", mnuTrayExit_Click)
			});

			nid.Icon = Properties.Resources.Alyx_Tray_Default;
			nid.Visible = true;

			/*
			recognizer = new SpeechRecognizer();
			recognizer.SpeechRecognized += recognizer_SpeechRecognized;
			*/

			synthesizer.StateChanged += synthesizer_StateChanged;
			synthesizer.Rate = 2;
			synthesizer.Speak("Hello, Michael. This is Alix. I am ready for your command.");

			Application.Run();

			synthesizer.Speak("See you later, Michael.");

			nid.Visible = false;
		}

		private static void mnuTrayExit_Click(object sender, EventArgs e)
		{
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
			
		}
	}
}
