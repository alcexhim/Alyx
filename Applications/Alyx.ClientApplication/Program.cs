using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using Alyx.Core;
using Alyx.Imaging;
using Alyx.Imaging.Thought;

using Alyx.Speech;
using Alyx.Speech.Synthesis;
using Alyx.Speech.Recognition;

using Alyx.Thought;

using Alyx.Linguistics;
using Alyx.Linguistics.LanguageParts;
using Alyx.Linguistics.Predicates;
using Alyx.Linguistics.Thought;

using Alyx.Configuration;

using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Dialogs;

namespace Alyx
{
	class Program : MBS.Framework.UserInterface.UIApplication
	{
		public static SynthesisEngine speaker = null;
		public static RecognitionEngine listener = null;

		private static Alyx.Networking.Client client = new Alyx.Networking.Client();
		public static Alyx.Networking.Client Client { get { return client; } }

		private static NotificationIcon nid = new NotificationIcon ();

		public Program()
		{
			ShortName = "alyx-client";
		}

		protected override void OnActivated(MBS.Framework.ApplicationActivatedEventArgs e)
		{
			base.OnActivated(e);

			// write local config file, figure out where this goes
			string homePath = String.Join(System.IO.Path.DirectorySeparatorChar.ToString(), new string[]
										  {
				System.Environment.GetFolderPath (Environment.SpecialFolder.ApplicationData),
				"alyx"
			});
			// ensure the directory is created if it doesn't already exist
			System.IO.Directory.CreateDirectory(homePath);


			string FileName_BootLog = homePath + System.IO.Path.DirectorySeparatorChar.ToString() + "BootLog";

			// write the startup time to the boot log
			{
				UniversalEditor.IO.Writer wLog = new UniversalEditor.IO.Writer(new UniversalEditor.Accessors.FileAccessor(FileName_BootLog, true, false, true));
				// seek to end of stream so we can append
				wLog.Accessor.Position = wLog.Accessor.Length;
				wLog.WriteInt16((short)0x2232);
				wLog.WriteDateTime(DateTime.Now);
				wLog.Close();
			}

			Console.CancelKeyPress += Console_CancelKeyPress;

			LocalMachine machine = new LocalMachine();
			machine.Load();

			if (machine.Languages.Count > 0)
			{
				Language.CurrentLanguage = machine.Languages[0];
			}

			if (Language.CurrentLanguage == null)
			{
				Console.Error.WriteLine("no languages found!");
			}
			else
			{
				TestSentenceParser();
			}
			

			/*

			Instance inst = machine.Instances[new Guid ("{7A2CD5EF-7D24-456A-B429-0D2C6B544F7A}")];

			UniversalEditor.Color fromRGB = UniversalEditor.Color.FromRGBA (192, 168, 140);

			// should be 32, 27, 75... gets 32, 70, 156 (???)
			Console.WriteLine ("fromRGB HSL : {0} {1} {2}", fromRGB.HueInt32, fromRGB.SaturationInt32, fromRGB.LuminosityInt32);
			Console.WriteLine ("fromRGB RGB : {0} {1} {2}", fromRGB.RedInt32, fromRGB.GreenInt32, fromRGB.BlueInt32);

			UniversalEditor.Color fromHSL = UniversalEditor.Color.FromHSL (240, 100, 100);
			
			Console.WriteLine ("fromHSL HSL : {0} {1} {2}", fromHSL.HueInt32, fromHSL.SaturationInt32, fromHSL.LuminosityInt32);
			Console.WriteLine ("fromHSL RGB : {0} {1} {2}", fromHSL.RedInt32, fromHSL.GreenInt32, fromHSL.BlueInt32);

			Language langEnglish = inst.Languages[new Guid("{81B5B066-0E62-4868-81D8-0C9DD388A41B}")];
			Language.CurrentLanguage = langEnglish;
			
			// TestMind();

			// TestConversation ();

			// TestSentenceRenderer();
			TestSentenceParser();
			*/

			nid.Name = "alyx-notification";
			nid.IconNameDefault = "alyx-default";
			nid.IconNameAttention = "alyx-attention";

			/*
			client.Connected += delegate (object sender, EventArgs e) {
				Indigo.Transports.TCP.TCPTransport tcp = (client.Transport as Indigo.Transports.TCP.TCPTransport);
				nid.Text = "A.L.Y.X. connected to " + tcp.Port.ToString();
			};
			*/
			client.MessageReceived += delegate (object sender, Alyx.Networking.MessageEventArgs e1)
			{
				if (e1.Message.Contains("\0"))
				{
					e1.Message = e1.Message.Substring(0, e1.Message.IndexOf('\0'));
				}
				e1.Message = e1.Message.Trim();
				Speak(e1.Message, e1.WaitUntilFinished);
			};

			nid.Text = "A.L.Y.X. disconnected";

			try {
				Client.Connect(System.Net.IPAddress.Parse("127.0.0.1"), 51221);
				nid.Text = "A.L.Y.X. connected to 127.0.0.1:51221";
			}
			catch (System.Net.Sockets.SocketException ex) {
				nid.Text = "A.L.Y.X. disconnected";
			}

			mvarMainWindow = new MainWindow();

			SynthesisEngineReference[] engines = SynthesisEngine.GetEngines();
			if (engines.Length > 0) speaker = engines[0].Create();

			nid.ContextMenu = BuildContextMenu();

			// nid.MouseDoubleClick += nid_MouseDoubleClick;

			// iconDefault = GetImage("TrayIcon/Default.png").ToIcon();
			// iconActive = GetImage("TrayIcon/Active.png").ToIcon();

			// nid.Icon = iconDefault;
			// nid.Visible = true;
			nid.Status = NotificationIconStatus.Visible;

			MainWindow mw = new MainWindow();
			// mvarSpeechMonitorWindow = new ChildWindows.SpeechMonitorWindow();
			// mvarSpeechMonitorWindow.Show();

			if (speaker != null)
			{
				speaker.SuppressSpeechEngineNotFound = true;
			}

			RefreshAvailableVoices();

			// speaker.Voice = speaker.GetVoice("Cepstral Callie");

			if (speaker != null)
			{
				speaker.StateChanged += speaker_StateChanged;
			}

			/*
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
			*/

		}

		protected override void OnCancelKeyPress(ConsoleCancelEventArgs e)
		{
			base.OnCancelKeyPress(e);

			Console.WriteLine("Ctrl+C pressed, shutting down client...");

			client.Disconnect();
			listener.Stop();
		}

		protected override void OnStopped(EventArgs e)
		{
			base.OnStopped(e);

			// give it a moment
			System.Threading.Thread.Sleep(500);

			nid.Status = NotificationIconStatus.Hidden;

			// mind.Stop();

			// TODO: for some reason when server.Stop() the client doesn't get kicked... it still thinks it's connected
			// be sure to disconnect manually before stopping the server
			//client.Disconnect();

			/*
			{
				UniversalEditor.IO.Writer wLog = new UniversalEditor.IO.Writer (new UniversalEditor.Accessors.FileAccessor (FileName_BootLog, true, false, true));
				wLog.Accessor.Position = wLog.Accessor.Length;
				wLog.WriteInt16 ((short)0x2233);
				wLog.WriteDateTime (DateTime.Now);
				wLog.Close ();
			}
			*/
		}

		/*
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
		*/

		private static Mind mind = new Mind();

		private class VMI_Test_Camera : VisualMindInput
		{
		}

		private static void TestMind()
		{
			Language lang = Language.CurrentLanguage;

			Idea idea = new Idea(new Guid("{5846643F-70E1-4E55-A77A-CDF5F17C2A83}"));
			idea.Representations.Add(new WordInstanceIdeaRepresentation(lang.GetNoun(new Guid("{E01FDBD2-758D-42D9-B09C-B43F2B17ACEE}"))));
			mind.Ideas.Add(idea);

			// mind.Inputs.Add (new VMI_Test_Camera ());
			// mind.Inputs.Add (new VMI_Test_Camera ());

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
					new ISubject[] { lang.GetPronoun(new Guid("{C0992F4D-134A-4662-8D17-BFEC65F6DA5E}"), Person.ThirdPerson, Quantity.Singular) },	// I bought [series].
					new DirectObjectPredicate(lang.GetVerb(new Guid("{48CA88BA-CEAF-423C-8A70-3C68C42E004A}")), new ISubject[] { apple, orange, pear, cherry })
				)
			});

			(sent.Clauses[0].Subjects[0] as PronounInstance).Person = Person.ThirdPerson;
			(sent.Clauses[0].Subjects[0] as PronounInstance).Gender = Genders.Feminine;
			(sent.Clauses[0].Subjects[0] as PronounInstance).Quantity = Quantity.Plural;

			string str = sent.ToString();
			Console.WriteLine (str);
		}

		private static void Pause()
		{
			Console.Write ("Press any key to continue . . .");
			Console.ReadKey (true);
		}

		private static void TestSentenceParser(string sentence)
		{
			Sentence sent = Sentence.Parse(sentence);

			Console.WriteLine (" Input: " + sentence);
			Console.WriteLine ("Output: " + sent.ToString ());

			Pause ();
		}
		private static void TestSentenceParser()
		{
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
			
			// TestSentenceParser ("The boy went fishing after school.");

			// TestSentenceParser ("The boy often went fishing after school.");

			// [The boy] [[often went] [to [the library]]] [after school].
			// TestSentenceParser ("The boy often went to the library after school.");

			// TestSentenceParser ("The chairman of Walter Industries looks to the future with the company's expansion.");
			// TestSentenceParser ("He began to build homes after World War II.");
			// TestSentenceParser ("Walter offered to build affordable housing.");



			// TODO: fix this, when run more than once it fails epicly
			TestSentenceParser ("History is an interesting subject.");

			TestSentenceParser ("History is an interesting subject.");

			TestSentenceParser ("Jane, John, and Francheska jumped over the lazy dog.");
			TestSentenceParser ("The name of the boy is John.");

			// [What's] [the boy's name]?
			// [What is] [the boy's name]?
			// [What is] [the name of the boy]?

			// this one is weird
			// TestSentenceParser ("The founder of Wikipedia is Jimmy Wales.");

			// this next one is REALLY weird
			// TestSentenceParser ("I am going to Spring Oaks today.");

			TestSentenceParser("The quick brown fox jumped quickly over the lazy dog.");
			// TestSentenceParser("The adorable young kitten chased the red bug.");
			// TestSentenceParser("Jane, John, and Francheska jumped over the lazy dog.");
			// TestSentenceParser("Dazzle me.");

			// TestSentenceParser("What's the name of the founder of Wikipedia?");
		}

		private static void TestConversation()
		{
			// sample conversation:
			// YOU:		What's your name?
			// ALYX:	My name is Alyx.

			LocalMachine machine = new LocalMachine ();
			machine.Load ();

			Instance instance = machine.Instances[new Guid("{7A2CD5EF-7D24-456A-B429-0D2C6B544F7A}")];
			
			Language lang = Language.CurrentLanguage;
			Sentence input1 = Sentence.Parse ("What's your name?");
			string input1Str = input1.ToString ();
		}

		private static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
		{
			MBS.Framework.Application.Instance.Stop ();
			e.Cancel = true;
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static int Main()
		{
			return (new Program()).Start();
		}

		private static Language InitializeLanguage_English()
		{
			// Language lang = Language.GetByID(new Guid("{81B5B066-0E62-4868-81D8-0C9DD388A41B}"));
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
			return null; // lang;
		}

		private static void Speak(string text, bool waitUntilDone = false)
		{
			if (speaker != null)
			{
				try
				{
					ConsoleColor fc = Console.ForegroundColor;
					Console.ForegroundColor = ConsoleColor.White;
					Console.Write("A.L.Y.X.: ");
					Console.ForegroundColor = ConsoleColor.Green;
					Console.WriteLine(text.Trim());
					Console.ForegroundColor = fc;

					NotificationPopup popup = new NotificationPopup();
					popup.IconName = "alyx-active";
					popup.Content = text.Trim();
					popup.Summary = "New message from A.L.Y.X.";
					popup.Show();

					speaker.Speak(text);
					if (waitUntilDone) speaker.WaitUntilDone();
				}
				catch (SpeechEngineNotFoundException ex)
				{
					Console.WriteLine("alyx.speech: engine '" + ex.EngineName + "' not found");
				}
			}
		}

		/*
		private static void nid_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			ShowMainWindow();
		}
		*/

		private static void speaker_StateChanged(object sender, SynthesisEngineStateChangedEventArgs e)
		{
			switch (e.State)
			{
				case SynthesisEngineState.Ready:
				{
					nid.Status = NotificationIconStatus.Visible;
					// nid.Icon = iconDefault;
					break;
				}
				case SynthesisEngineState.Speaking:
				{
					nid.Status = NotificationIconStatus.Attention;
					/*
					if (mvarSpeechMonitorWindow != null)
					{
						mvarSpeechMonitorWindow.AddOutputLine(e.Text);
					}
					nid.Icon = iconActive;
					*/
					break;
				}
			}
		}

		private static MainWindow mvarMainWindow = null;
		public static void ShowMainWindow()
		{
			if (mvarMainWindow == null) mvarMainWindow = new MainWindow();
			if (mvarMainWindow.IsDisposed)
				mvarMainWindow = new MainWindow ();

			mvarMainWindow.Show();
		}

		private static Menu BuildContextMenu()
		{
			Menu menu = new Menu ();
			menu.Items.AddRange (new MenuItem[] {
				new CommandMenuItem ("_Options...", null, delegate (object sender, EventArgs e) {
					ShowMainWindow ();
				}),
				new SeparatorMenuItem(),
				BuildVoiceMenu(),
				new SeparatorMenuItem(),
				new CommandMenuItem ("_Connect...", null, delegate (object sender, EventArgs e) {
					ConnectDialog dlg = new ConnectDialog();
					if (dlg.ShowDialog() == DialogResult.OK) {
						int port = 0;

						// TODO: fix this, GetControlTextInternal on GTK returns a reference that is only valid once
						// if we check dlg.txtServerName.Text more than once for instance, we get funky results

						string strPort = dlg.txtPort.Text;
						string strServerName = dlg.txtServerName.Text;

						if (!Int32.TryParse(strPort, out port)) {
							MessageDialog.ShowDialog("Please enter a valid integer port number", "Error", MessageDialogButtons.OK, MessageDialogIcon.Error);
							return;
						}
						
						if (String.IsNullOrEmpty(strServerName)) {
							MessageDialog.ShowDialog("Please enter a valid server name or IP address", "Error", MessageDialogButtons.OK, MessageDialogIcon.Error);
							return;
						}

						System.Net.IPAddress addr = null;
						if (!System.Net.IPAddress.TryParse(strServerName, out addr))
						{
							System.Net.IPAddress[] addrs = System.Net.Dns.GetHostAddresses(strServerName);
							if (addrs.Length > 0) addr = addrs[0];
						}
						if (addr != null) {
							try {
								Client.Connect(addr, port);
							}
							catch (System.Net.Sockets.SocketException ex) {
								MessageDialog.ShowDialog("Cannot connect to the specified server.", "Error", MessageDialogButtons.OK, MessageDialogIcon.Error);
							}
						}
					}
				}),
				new CommandMenuItem ("Dis_connect", null, delegate (object sender, EventArgs e) {
				}),
				new SeparatorMenuItem(),
				new CommandMenuItem ("_Quit", null, delegate (object sender, EventArgs e) {
					MBS.Framework.Application.Instance.Stop();
				})
			});
			return menu;
		}

		public static void RefreshAvailableVoices()
		{
			CommandMenuItem menuVoice = nid.ContextMenu.Items[2] as CommandMenuItem;
			RefreshVoiceMenu(menuVoice);
		}
		public static CommandMenuItem BuildVoiceMenu()
		{
			CommandMenuItem menuVoice = new CommandMenuItem("_Voice");
			RefreshVoiceMenu(menuVoice);
			return menuVoice;
		}
		public static void RefreshVoiceMenu(CommandMenuItem menuVoice)
		{
			menuVoice.Items.Clear();
			if (speaker != null)
			{
				foreach (Voice voice in speaker.GetVoices())
				{
					CommandMenuItem mi = new CommandMenuItem(voice.Name, null, menuTrayVoice_Click);
					mi.Enabled = voice.Enabled;
					mi.Data = voice;
					menuVoice.Items.Add(mi);
				}
			}
			menuVoice.Items.Add(new SeparatorMenuItem());
			menuVoice.Items.Add(new CommandMenuItem("_Refresh Available Voices", null, menuTrayVoiceRefresh_Click));
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
			Voice voice = (mi.Data as Voice);

			try
			{
				speaker.Voice = voice;
				speaker.Speak("Hello, Michael. I am " + voice.Name + ".");
			}
			catch (Exception ex)
			{
				speaker.Speak("I can't use that voice. " + ex.Message);
				Console.Error.WriteLine(ex.Message);
				Console.Error.WriteLine(ex.StackTrace);
			}
		}

		private static void mnuTrayExit_Click(object sender, EventArgs e)
		{
			if (listener != null) listener.Stop();

			MBS.Framework.Application.Instance.Stop ();
		}
	}
}
