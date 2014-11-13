using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using Alyx.Speech.Synthesis;

namespace Alyx
{
	static class Program
	{
		public static Alyx.Speech.Synthesis.Engine speaker = new Alyx.Speech.Synthesis.Engines.DefaultEngine();
		public static Alyx.Speech.Recognition.Engine listener = new Alyx.Speech.Recognition.Engines.DefaultEngine();

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

			nid.Icon = Properties.Resources.Alyx_Tray_Default;
			nid.Visible = true;

			// synthesizer.SelectVoice("Microsoft Zira Desktop");

			speaker.StateChanged += speaker_StateChanged;
			speaker.Speak("Hello Michael. This is Alix. I am ready for your command.");

			Application.Run();

			speaker.Speak("See you later Michael");

			nid.Visible = false;
		}

		static void speaker_StateChanged(object sender, EngineStateChangedEventArgs e)
		{
			switch (e.State)
			{
				case Speech.Synthesis.EngineState.Ready:
				{
					nid.Icon = Properties.Resources.Alyx_Tray_Default;
					break;
				}
				case Speech.Synthesis.EngineState.Speaking:
				{
					nid.Icon = Properties.Resources.Alyx_Tray_Speaking;
					break;
				}
			}
		}


		private static ContextMenu BuildContextMenu()
		{
			ContextMenu menu = new ContextMenu();

			MenuItem menuVoice = new MenuItem("&Voice");
			foreach (Voice voice in speaker.GetVoices())
			{
				MenuItem mi = new MenuItem(voice.Name, menuVoice_Click);
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
			Voice voice = (mi.Tag as Voice);
			speaker.Voice = voice;

			speaker.Speak("Hello, Michael. I am " + voice.Name + ".");
		}

		private static void mnuTrayExit_Click(object sender, EventArgs e)
		{
			listener.Stop();

			Application.Exit();
		}
	}
}
