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

using Alyx.Core;
using Alyx.Speech;
using Alyx.Thought;
using Alyx.Linguistics.Thought;

namespace Alyx
{
	static class Program
	{
		public static SynthesisEngine speaker = null;
		public static RecognitionEngine listener = null;

		private static ChildWindows.SpeechMonitorWindow mvarSpeechMonitorWindow = null;
		private static bool mvarShowSpeechMonitorWindow = true;

		private static NotifyIcon nid = new NotifyIcon();

		private static Alyx.Networking.Server server = new Alyx.Networking.Server();
		public static Alyx.Networking.Server Server { get { return server; } }

		private static Alyx.Networking.Client client = new Alyx.Networking.Client();
		public static Alyx.Networking.Client Client { get { return client; } }
		
		private static System.Drawing.Image GetImage(string path)
		{
			path = path.Replace("\\", System.IO.Path.DirectorySeparatorChar.ToString());
			path = path.Replace("/", System.IO.Path.DirectorySeparatorChar.ToString());
			string basepath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
			
			basepath += System.IO.Path.DirectorySeparatorChar.ToString() + "Images";
			string imagepath = basepath + System.IO.Path.DirectorySeparatorChar.ToString() + path;
			
			if (!System.IO.File.Exists(imagepath))
			{
				Console.WriteLine("File not exist: '" + imagepath + "'");
				return null;
			}
			return System.Drawing.Image.FromFile(imagepath);
		}
		private static System.Drawing.Icon ToIcon(this System.Drawing.Image image)
		{
			if (image == null)
				return null;

			System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(image.Width, image.Height);
			System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);
			g.DrawImage(image, 0, 0, image.Width, image.Height);
			g.Flush();

			IntPtr hIcon = bitmap.GetHicon();
			return System.Drawing.Icon.FromHandle(hIcon);
		}
		
		private static System.Drawing.Icon iconDefault = null;
		private static System.Drawing.Icon iconActive = null;

		private static Mind mind = new Mind();

		private static void TestMind()
		{
			Language lang = Language.CurrentLanguage;

			Idea idea = new Idea(new Guid("{5846643F-70E1-4E55-A77A-CDF5F17C2A83}"));
			idea.Representations.Add(new WordInstanceIdeaRepresentation(lang.GetNoun(new Guid("{E01FDBD2-758D-42D9-B09C-B43F2B17ACEE}"))));
			mind.Ideas.Add(idea);

			Sentence sent1 = Sentence.Parse("You're ugly.");
			Console.WriteLine (sent1.ToString ());

			mind.Start();
		}

		private static void TestSentenceRenderer()
		{
			Language lang = Language.CurrentLanguage;

			NounInstance apple = lang.GetNoun(new Guid("{E01FDBD2-758D-42D9-B09C-B43F2B17ACEE}"));
			apple.Quantity = Quantity.Plural;

			NounInstance orange = lang.GetNoun(new Guid("{DD172F11-DFF6-43A0-938E-52B08B1E36FC}"));
			orange.Quantity = Quantity.Plural;

			NounInstance pear = lang.GetNoun(new Guid("{4CA5238C-7403-480F-99BD-B3B382434105}"));
			pear.Definiteness = Definiteness.Indefinite;
			pear.Quantity = Quantity.Singular;

			NounInstance cherry = lang.GetNoun(new Guid("{B7177096-0816-4C29-8394-B420F05615EF}"));
			cherry.Quantity = Quantity.Plural;
			cherry.Definiteness = Definiteness.Indefinite;

			Sentence sent = new Sentence(SentenceTypes.Declarative, new Clause[]
			{
				new Clause
				(
					new ISubject[] { lang.GetPronoun(Person.ThirdPerson, Quantity.Singular) },	// I bought [series].
					new DirectObjectPredicate(lang.GetVerb(new Guid("{48CA88BA-CEAF-423C-8A70-3C68C42E004A}")), new ISubject[] { apple, orange, pear, cherry })
				)
			});

			(sent.Clauses[0].Subjects[0] as PronounInstance).Person = Person.ThirdPerson;
			(sent.Clauses[0].Subjects[0] as PronounInstance).Gender = Genders.Feminine;
			(sent.Clauses[0].Subjects[0] as PronounInstance).Quantity = Quantity.Plural;

			string str = sent.ToString();
		}

		private static void TestSentenceParser(string sentence)
		{
			Sentence sent = Sentence.Parse(sentence);
			Console.WriteLine (" Input: " + sentence);
			Console.WriteLine ("Output: " + sent.ToString ());
		}
		private static void TestSentenceParser()
		{
			Language lang = Language.CurrentLanguage;

			// A Word Sequence should be considered a Proper Noun if either of the following conditions apply:
			//		1.  If a particular word is capitalized, it should be considered a proper noun.
			//			An example of this case would be "John".
			//		2.	The entire sequence should be considered a proper noun, if the word sequence is such that:
			//			a.	Every word in the sequence is capitalized.
			//			An example of this case would be "Elm Street"
			//		3.  The entire sequence should be considered a proper noun, if the word sequence is such that:
			//			a.	the beginning and end word are both capitalized, and
			//			b.	lowercase words in between are solely of Article and Preposition classes
			//			An example of this case would be "Lord of the Rings".


			// Fix this one:
			//	----- Subject -----	  -Object-
			// [The name of the boy] [is John]
			TestSentenceParser ("The name of the boy is John.");

			TestSentenceParser("The quick brown fox jumped over the lazy dog.");
			TestSentenceParser("The adorable young kitten chased the bug.");
			TestSentenceParser("Jane, John, and Francheska jumped over the lazy dog.");
			TestSentenceParser("Dazzle me.");
			TestSentenceParser("What's the name of the founder of Wikipedia?");
		}
		
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			Language lang = Language.GetByID(new Guid("{81B5B066-0E62-4868-81D8-0C9DD388A41B}"));
			Language.CurrentLanguage = lang;

			// TestMind();

			// TestSentenceRenderer();
			TestSentenceParser();

			bool enableNetworking = false;
			if (enableNetworking)
			{
				// start the Alyx server
				Server.Transport = new Indigo.Transports.TCP.TCPTransport(51221);
				Server.Start();

				Client.Connect(System.Net.IPAddress.Parse("127.0.0.1"), 51221);
			}

			nid.Text = "Alyx";
			nid.ContextMenu = BuildContextMenu();

			nid.MouseDoubleClick += nid_MouseDoubleClick;

			iconDefault = GetImage("TrayIcon/Default.png").ToIcon();
			iconActive = GetImage("TrayIcon/Active.png").ToIcon();

			nid.Icon = iconDefault;
			nid.Visible = true;

			mvarSpeechMonitorWindow = new ChildWindows.SpeechMonitorWindow();
			mvarSpeechMonitorWindow.Show();
			
			SynthesisEngineReference[] engines = SynthesisEngine.GetEngines();
			
			if (engines.Length > 0) speaker = engines[0].Create();

			if (speaker != null)
				speaker.SuppressSpeechEngineNotFound = true;

			RefreshAvailableVoices();

			// speaker.Voice = speaker.GetVoice("Cepstral Callie");

			if (speaker != null)
			{
				speaker.StateChanged += speaker_StateChanged;
			}

			Language langEnglish = InitializeLanguage_English();

			Language.CurrentLanguage = langEnglish;

			AdjectiveInstance lazy = langEnglish.GetAdjective(new Guid("{05F6A350-6F7F-4B0A-B95D-1C259D03B111}"));
			AdjectiveInstance quick = langEnglish.GetAdjective(new Guid("{7AD70B20-468C-47A8-89E9-A4568A0B7C1E}"));
			AdjectiveInstance brown = langEnglish.GetAdjective(new Guid("{330DF41E-C811-4E61-8E76-7D9D8B85F9D4}"));
			
			NounInstance dog = langEnglish.GetNoun(new Guid("{5BCA1601-C769-4DD0-BF4E-EDCEC46EF3EB}"));
			dog.Adjectives.Add(lazy);

			NounInstance fox = langEnglish.GetNoun(new Guid("{9E3B38FF-631D-475F-B2BA-D0DAD35C15A9}"));
			fox.Adjectives.AddRange<AdjectiveInstance>(quick, brown);

			fox.Definiteness = Definiteness.Definite;
			fox.Quantity = Quantity.Plural;

			Sentence quickbrownfox = new Sentence(SentenceTypes.Declarative, new Clause[]
			{
				new Clause(new ISubject[] { fox }, new PrepositionalObjectPredicate
				(
					langEnglish.GetVerb(new Guid("{0F27D1D0-53E2-45E3-9940-A318FE8E7EF7}"), Person.ThirdPerson, Tense.Past),
					langEnglish.GetPreposition(new Guid("{FD5B840D-9491-4D00-A338-364AC059521B}")),
					new ISubject[] { dog }
				))
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

			mind.Stop();
			server.Stop();
		}

		private static Language InitializeLanguage_English()
		{
			Language lang = Language.GetByID(new Guid("{81B5B066-0E62-4868-81D8-0C9DD388A41B}"));
			/*
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
				new WordMappingValue("jump", new WordMappingValueCriteria[]
				{
					new WordMappingValueCriteria() { Tense = Linguistics.Tense.Unspecified, Person = Linguistics.Person.Unspecified, Quantity = Quantity.Unspecified },	// [infinitive] jump

					new WordMappingValueCriteria() { Tense = Linguistics.Tense.Present, Person = Linguistics.Person.FirstPerson, Quantity = Quantity.Singular },	// I jump
					new WordMappingValueCriteria() { Tense = Linguistics.Tense.Present, Person = Linguistics.Person.FirstPerson, Quantity = Quantity.Plural },		// we jump
					new WordMappingValueCriteria() { Tense = Linguistics.Tense.Present, Person = Linguistics.Person.SecondPerson, Quantity = Quantity.Singular },	// you jump
					new WordMappingValueCriteria() { Tense = Linguistics.Tense.Present, Person = Linguistics.Person.SecondPerson, Quantity = Quantity.Plural },	// you all jump
					new WordMappingValueCriteria() { Tense = Linguistics.Tense.Present, Person = Linguistics.Person.ThirdPerson, Quantity = Quantity.Plural }		// they jump
				}),
				new WordMappingValue("jumps", new WordMappingValueCriteria[]
				{
					new WordMappingValueCriteria() { Tense = Linguistics.Tense.Present, Person = Linguistics.Person.ThirdPerson, Quantity = Quantity.Singular }	// he/she/it jumps
				}),
				new WordMappingValue("jumped", new WordMappingValueCriteria[]
				{
					new WordMappingValueCriteria() { Tense = Linguistics.Tense.Past, Person = Linguistics.Person.FirstPerson, Quantity = Quantity.Singular },	// I jumped
					new WordMappingValueCriteria() { Tense = Linguistics.Tense.Past, Person = Linguistics.Person.FirstPerson, Quantity = Quantity.Plural },		// we jumped
					new WordMappingValueCriteria() { Tense = Linguistics.Tense.Past, Person = Linguistics.Person.SecondPerson, Quantity = Quantity.Singular },	// you jumped
					new WordMappingValueCriteria() { Tense = Linguistics.Tense.Past, Person = Linguistics.Person.SecondPerson, Quantity = Quantity.Plural },	// you all jumped
					new WordMappingValueCriteria() { Tense = Linguistics.Tense.Past, Person = Linguistics.Person.ThirdPerson, Quantity = Quantity.Singular },		// he/she/it jumped
					new WordMappingValueCriteria() { Tense = Linguistics.Tense.Past, Person = Linguistics.Person.ThirdPerson, Quantity = Quantity.Plural }		// they jumped
				})
			}));
			*/
			return lang;
		}

		private static void Speak(string text, bool waitUntilDone = false)
		{
			if (speaker != null)
			{
				try
				{
					speaker.Speak(text);
					if (waitUntilDone) speaker.WaitUntilDone();
				}
				catch (SpeechEngineNotFoundException ex)
				{
					Console.WriteLine("alyx.speech: engine '" + ex.EngineName + "' not found");
				}
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
					nid.Icon = iconDefault;
					break;
				}
				case SynthesisEngineState.Speaking:
				{
					if (mvarSpeechMonitorWindow != null)
					{
						mvarSpeechMonitorWindow.AddOutputLine(e.Text);
					}
					nid.Icon = iconActive;
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
