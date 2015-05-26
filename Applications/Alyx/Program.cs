using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using Alyx.Speech.Synthesis;
using Alyx.Speech.Recognition;
using Alyx.Linguistics;
using Alyx.Linguistics.LanguageParts;
using Alyx.Linguistics.Predicates;

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

			nid.MouseDoubleClick += nid_MouseDoubleClick;

			nid.Icon = Properties.Resources.Alyx_Tray_Default;
			nid.Visible = true;
			
			SynthesisEngine[] engines = SynthesisEngine.GetEngines();
			if (engines.Length > 0) speaker = engines[0];
			RefreshAvailableVoices();

			// speaker.Voice = speaker.GetVoice("Cepstral Callie");

			if (speaker != null)
			{
				speaker.StateChanged += speaker_StateChanged;
			}

			Language lang = Language.Create(new Guid("{F369FB77-533C-409D-BBEE-7E9EF347B445}"));
			lang.WordMappings.Add(new WordMapping(new Guid("{5BCA1601-C769-4DD0-BF4E-EDCEC46EF3EB}"), new WordMappingValue[]
			{
				new WordMappingValue("dog", new WordMappingValueCriteria[]
				{
					new WordMappingValueCriteria() { Quantity = Quantity.Unspecified },
					new WordMappingValueCriteria() { Quantity = Quantity.Singular }
				}),
				new WordMappingValue("dogs", new WordMappingValueCriteria[]
				{
					new WordMappingValueCriteria() { Quantity = Quantity.Unspecified },
					new WordMappingValueCriteria() { Quantity = Quantity.Plural }
				})
			}));
			lang.WordMappings.Add(new WordMapping(new Guid("{9E3B38FF-631D-475F-B2BA-D0DAD35C15A9}"), new WordMappingValue[]
			{
				new WordMappingValue("fox", new WordMappingValueCriteria[]
				{
					new WordMappingValueCriteria() { Quantity = Quantity.Unspecified },
					new WordMappingValueCriteria() { Quantity = Quantity.Singular }
				}),
				new WordMappingValue("foxes", new WordMappingValueCriteria[]
				{
					new WordMappingValueCriteria() { Quantity = Quantity.Plural }
				})
			}));
			lang.WordMappings.Add(new WordMapping(new Guid("{7AD70B20-468C-47A8-89E9-A4568A0B7C1E}"), new WordMappingValue[]
			{
				new WordMappingValue("quick")
			}));
			lang.WordMappings.Add(new WordMapping(new Guid("{330DF41E-C811-4E61-8E76-7D9D8B85F9D4}"), new WordMappingValue[]
			{
				new WordMappingValue("brown")
			}));
			lang.WordMappings.Add(new WordMapping(new Guid("{05F6A350-6F7F-4B0A-B95D-1C259D03B111}"), new WordMappingValue[]
			{
				new WordMappingValue("lazy")
			}));
			lang.WordMappings.Add(new WordMapping(new Guid("{FD5B840D-9491-4D00-A338-364AC059521B}"), new WordMappingValue[]
			{
				new WordMappingValue("over")
			}));
			lang.WordMappings.Add(new WordMapping(new Guid("{0F27D1D0-53E2-45E3-9940-A318FE8E7EF7}"), new WordMappingValue[]
			{
				new WordMappingValue("jump")
			}));

			Language.CurrentLanguage = lang;

			Adjective lazy = new Adjective(new Guid("{05F6A350-6F7F-4B0A-B95D-1C259D03B111}"));
			Noun dog = new Noun(new Guid("{5BCA1601-C769-4DD0-BF4E-EDCEC46EF3EB}"), new Adjective[]
			{
				lazy
			});
			dog.Quantity = Quantity.Singular;

			Adjective quick = new Adjective(new Guid("{7AD70B20-468C-47A8-89E9-A4568A0B7C1E}"));
			Adjective brown = new Adjective(new Guid("{330DF41E-C811-4E61-8E76-7D9D8B85F9D4}"));
			Noun fox = new Noun(new Guid("{9E3B38FF-631D-475F-B2BA-D0DAD35C15A9}"), new Adjective[]
			{
				quick,
				brown
			});
			fox.Definiteness = Definiteness.Definite;

			Verb jump = new Verb(new Guid("{0F27D1D0-53E2-45E3-9940-A318FE8E7EF7}"));

			Preposition over = new Preposition(new Guid("{FD5B840D-9491-4D00-A338-364AC059521B}"));

			Sentence quickbrownfox = new Sentence(new Clause[]
			{
				new Clause(fox, new PrepositionalObjectPredicate(jump, over, dog))
			});

			dog.Definiteness = Definiteness.Definite;
			dog.Quantity = Quantity.Singular;

			string text = quickbrownfox.ToString();
			// Speak("I couldn't find your configuration file, so I created a new one. I hope you don't mind.");

			// Speak("I couldn't find the Microsoft Zira Desktop voice, so I chose Microsoft Anna.");

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
			Speak(String.Format(WelcomeLiterals[r], "Michael"));

			Application.Run();

			r = random.Next(0, GoodbyeLiterals.Length);
			Speak(String.Format(GoodbyeLiterals[r], "Michael"), true);

			nid.Visible = false;
		}

		private static void Speak(string text, bool waitUntilDone = false)
		{
			if (speaker != null)
			{
				speaker.Speak(text);
				if (waitUntilDone) speaker.WaitUntilDone();
			}
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
			if (speaker != null)
			{
				foreach (Voice voice in speaker.GetVoices())
				{
					MenuItem mi = new MenuItem(voice.Name, menuTrayVoice_Click);
					mi.Enabled = voice.Enabled;
					mi.Tag = voice;
					menuVoice.MenuItems.Add(mi);
				}
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
			if (listener != null) listener.Stop();

			Application.Exit();
		}
	}
}
