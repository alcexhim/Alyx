using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using Alyx.Speech.Synthesis;
using Alyx.Speech.Recognition;

namespace Alyx
{
	static class Program
	{
		public static SynthesisEngine speaker = null;
		public static RecognitionEngine listener = null;

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
			nid.ContextMenu = BuildContextMenu();
			RefreshAvailableVoices();

			nid.MouseDoubleClick += nid_MouseDoubleClick;

			nid.Icon = Properties.Resources.Alyx_Tray_Default;
			nid.Visible = true;
			
			SynthesisEngine[] engines = SynthesisEngine.GetEngines();
			if (engines.Length == 0) return;
			
			speaker = engines[0];

			// speaker.Voice = speaker.GetVoice("Cepstral Callie");

			speaker.StateChanged += speaker_StateChanged;

			// speaker.Speak("I couldn't find your configuration file, so I created a new one. I hope you don't mind.");

			// speaker.Speak("I couldn't find the Microsoft Zira Desktop voice, so I chose Microsoft Anna.");

			string[] WelcomeLiterals = new string[]
			{
				"Good to see you again {0}",
				"Hello {0}",
				"Welcome back {0}"
			};
			string[] GoodbyeLiterals = new string[]
			{
				"See you later {0}",
				"Goodbye {0}",
				"Have a nice day {0}"
			};

			Random random = new Random();
			int r = 0;

			r = random.Next(0, WelcomeLiterals.Length);
			speaker.Speak(String.Format(WelcomeLiterals[r], "Michael"));

			Application.Run();

			r = random.Next(0, GoodbyeLiterals.Length);
			speaker.Speak(String.Format(GoodbyeLiterals[r], "Michael"));

			speaker.WaitUntilDone();

			nid.Visible = false;
		}

		private static void nid_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			ShowMainWindow();
		}

		private static void speaker_StateChanged(object sender, SynthesisEngineStateChangedEventArgs e)
		{
			switch (e.State)
			{
				case SynthesisEngineState.Ready:
				{
					nid.Icon = Properties.Resources.Alyx_Tray_Default;
					break;
				}
				case SynthesisEngineState.Speaking:
				{
					nid.Icon = Properties.Resources.Alyx_Tray_Speaking;
					break;
				}
			}
		}

		private static MainWindow mvarMainWindow = null;
		public static void ShowMainWindow()
		{
			if (mvarMainWindow == null) mvarMainWindow = new MainWindow();
			if (mvarMainWindow.IsDisposed) mvarMainWindow = new MainWindow();
			mvarMainWindow.Show();
		}


		private static ContextMenu BuildContextMenu()
		{
			ContextMenu menu = new ContextMenu();

			menu.MenuItems.Add(new MenuItem("&Options", menuTrayOptions_Click));
			menu.MenuItems[0].DefaultItem = true;

			menu.MenuItems.Add("-");

			MenuItem menuVoice = new MenuItem("&Voice");
			menu.MenuItems.Add(menuVoice);

			menu.MenuItems.Add(new MenuItem("-"));
			menu.MenuItems.Add(new MenuItem("E&xit", mnuTrayExit_Click));
			return menu;
		}

		public static void RefreshAvailableVoices()
		{
			MenuItem menuVoice = nid.ContextMenu.MenuItems[2];
			menuVoice.MenuItems.Clear();
			foreach (Voice voice in speaker.GetVoices())
			{
				MenuItem mi = new MenuItem(voice.Name, menuTrayVoice_Click);
				mi.Enabled = voice.Enabled;
				mi.Tag = voice;
				menuVoice.MenuItems.Add(mi);
			}
			menuVoice.MenuItems.Add("-");
			menuVoice.MenuItems.Add(new MenuItem("&Refresh Available Voices", menuTrayVoiceRefresh_Click));
		}

		private static void menuTrayVoiceRefresh_Click(object sender, EventArgs e)
		{
			RefreshAvailableVoices();
		}

		private static void menuTrayOptions_Click(object sender, EventArgs e)
		{
			ShowMainWindow();
		}

		private static void menuTrayVoice_Click(object sender, EventArgs e)
		{
			MenuItem mi = (sender as MenuItem);
			Voice voice = (mi.Tag as Voice);

			try
			{
				speaker.Voice = voice;
				speaker.Speak("Hello, Michael. I am " + voice.Name + ".");
			}
			catch (Exception ex)
			{
				speaker.Speak("I can't use that voice. " + ex.Message);
			}
		}

		private static void mnuTrayExit_Click(object sender, EventArgs e)
		{
			listener.Stop();

			Application.Exit();
		}
	}
}
