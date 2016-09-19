using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ApplicationFramework;

using Alyx.Configuration;
using Alyx.Thought;

namespace Alyx.ServerApplication
{
	class Program
	{
		private static Mind mind = new Mind ();
		private static LocalMachine localMachine = new LocalMachine ();

		static void Main(string[] args)
		{
			// some arguments we might include are:
			// --port nnnn	
			Application.Arguments.Add ("port", "The port on which to listen for incoming connections", 'p');
			Application.Arguments.Add ("help", "Display this help text");

			Application.Initialize ();

			

			int port = 51221;
			Argument argPort = Application.Arguments ["port"];
			if (argPort.Value != null) {
				if (!Int32.TryParse (argPort.Value, out port)) {
					Console.WriteLine ("Specified port must be a number");
					return;
				}
			}

			// initialize the local machine configuration
			localMachine.Load ();

			// start the Alyx server
			Server.ClientConnected += Server_ClientConnected;
			Server.Transport = new Indigo.Transports.TCP.TCPTransport(port);
			Server.Start();

			// configure the Alyx Mind
			// this is ugly. these are hardcoded scripts... we need to figure out how to do inputs and outputs
			mind.Scripts.Add (new SleepyMindScript ());
			mind.Scripts.Add (new SensorsMindScript ());

			// MindInput input = new MindInput();
			// MindOutput outputTextToSpeech = new TTSMindOutput();

			// when Input is triggered, Output executes
			TextToSpeechMindOutput outputTextToSpeech = new TextToSpeechMindOutput ();
			mind.Connections.Add(new MindConnection(new TextMindInput("Testing testing"), outputTextToSpeech));


			// foreach (MindConnection conn in mind.Connections)
			// if (conn.Input.should_execute...) then conn.Output.Execute()


			// start the Alyx Mind
			mind.Start ();

			PrintHeader ();

			Console.CancelKeyPress += Console_CancelKeyPress;
		}

		private class TextMindInput : IMindInput
		{
			private string mvarValue = null;
			public string Value { get { return mvarValue; } set { mvarValue = value; } }

			/// <summary>
			/// gets the value of this <see cref="MindInput" /> 
			/// </summary>
			/// <returns>The value.</returns>
			public object GetValue()
			{
				return mvarValue;
			}

			public TextMindInput(string value)
			{
				mvarValue = value;
			}
		}
		private class TextToSpeechMindOutput : IMindOutput
		{
			public void Execute(object param)
			{
				Server.Speak (param.ToString ());
			}
		}

		private class HasInternetAccessMindScript : MindScript
		{
			private bool mvarHasInternetAccess = false;

			protected override void OnInitialized (EventArgs e)
			{
				base.OnInitialized (e);

				string[] testDomains = new string[] { "www.google.com" };
				foreach (string testDomain in testDomains) {
					try {
						System.Net.IPAddress[] ipad = System.Net.Dns.GetHostAddresses (testDomain);
						mvarHasInternetAccess = true;
						break;
					} catch {
					}
				}
			}
		}

		/// <summary>
		/// Runs a <see cref="ClockMindInput" /> that takes sensor readings from the "sensors" program every 30
		/// seconds.
		/// </summary>
		private class SensorsMindScript : ClockMindScript
		{
			private class SENS_Adapter
			{
				private string mvarAdapterName = String.Empty;
				public string AdapterName { get { return mvarAdapterName; } set { mvarAdapterName = value; } }

				private List<SENS_Property> mvarProperties = new List<SENS_Property>();
				public List<SENS_Property> Properties { get { return mvarProperties; } }
			}
			private class SENS_Property
			{
				private string mvarPropertyName = String.Empty;
				public string PropertyName { get { return mvarPropertyName; } }

				private string mvarValue = null;
				public string Value { get { return mvarValue; } }

				public static SENS_Property Parse(string line)
				{
					string[] propparts = line.Split (new char[] { ':' });
					string propName = propparts [0].Trim ();
					string propVal = null;
					if (propparts.Length > 1) {
						propVal = propparts [1].Trim ();
					}

					if (propVal.Contains ("(") && propVal.Contains (")")) {
						string realval = propVal.Substring (0, propVal.IndexOf ('(')).Trim ();

						int indexOfParen = propVal.IndexOf ('(') + 1;
						string comment = propVal.Substring (indexOfParen, propVal.Length - indexOfParen - 1);

						propVal = realval;
					}

					SENS_Property prop = new SENS_Property (propName, propVal);
					return prop;
				}

				public SENS_Property(string propertyName, string value)
				{
					mvarPropertyName = propertyName;
					mvarValue = value;
				}
			}

			private void GetSensorReadings()
			{
				System.Diagnostics.Process p = new System.Diagnostics.Process ();
				p.StartInfo = new System.Diagnostics.ProcessStartInfo ("sensors");
				p.StartInfo.RedirectStandardOutput = true;
				p.StartInfo.CreateNoWindow = true;
				p.StartInfo.UseShellExecute = false;

				try
				{
					p.Start ();
					p.WaitForExit ();
				}
				catch  (Exception ex) {
					ConsoleColor fg = Console.ForegroundColor;
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine (ex.GetType ().Name + ": " + ex.Message);
					Console.ForegroundColor = fg;
				}

				string s = p.StandardOutput.ReadToEnd ();
				string[] lines = s.Split (new char[] { '\n' });

				List<SENS_Adapter> listAdapters = new List<SENS_Adapter> ();

				{
					SENS_Adapter adapter = null;
					foreach (string line in lines) {
						if (String.IsNullOrEmpty (line)) {
							if (adapter != null)
								listAdapters.Add (adapter);

							adapter = null;
							continue;
						}
						if (adapter == null) {
							adapter = new SENS_Adapter ();
							adapter.AdapterName = line;
						} else {
							adapter.Properties.Add (SENS_Property.Parse (line));
						}
					}
				}
				/*
				foreach (SENS_Adapter adapter in listAdapters)
				{
					Console.WriteLine (adapter.AdapterName);
					foreach (SENS_Property prop in adapter.Properties) {
						Console.Write ("    ");
						Console.Write (prop.PropertyName);
						Console.Write (": ");
						Console.WriteLine (prop.Value);
					}
				}
				*/
			}

			protected override void OnInitialized (EventArgs e)
			{
				base.OnInitialized (e);
				GetSensorReadings ();
			}

			private int sec = 1;
			protected override void OnSecondChanged (EventArgs e)
			{
				base.OnSecondChanged (e);

				sec ++;
				if (sec == 30) {

					GetSensorReadings ();
					sec = 1;
				}
			}
		}

		/// <summary>
		/// This script runs every hour and checks to see if hour is 01, if it is it tells us we should go to bed.
		/// DISCLAIMER: I wrote this when I was tired and wanted to go to bed...
		/// </summary>
		private class SleepyMindScript : ClockMindScript
		{
			protected override void OnHourChanged (EventArgs e)
			{
				base.OnHourChanged (e);

				if (DateTime.Now.Hour == 01) {
					Server.Speak ("It's getting late. You should probably get ready to go to sleep.");
				}
			}
		}

		/// <summary>
		/// Runs a <see cref="MindScript" /> which responds to year, month, day, hour, minute, and second changes.
		/// </summary>
		private class ClockMindScript : MindScript
		{
			protected virtual void OnYearChanged(EventArgs e)
			{
			}
			protected virtual void OnMonthChanged(EventArgs e)
			{
			}
			protected virtual void OnDayChanged(EventArgs e)
			{
			}
			protected virtual void OnHourChanged(EventArgs e)
			{
			}
			protected virtual void OnMinuteChanged(EventArgs e)
			{
			}

			protected virtual void OnSecondChanged(EventArgs e)
			{
			}

			private int _prevYear = DateTime.Now.Year;
			private int _prevMonth = DateTime.Now.Month;
			private int _prevDay = DateTime.Now.Day;
			private int _prevHour = DateTime.Now.Hour;
			private int _prevMinute = DateTime.Now.Minute;
			private int _prevSecond = DateTime.Now.Second;

			protected override void ProcessInternal ()
			{
				if (DateTime.Now.Year != _prevYear) {
					OnYearChanged (EventArgs.Empty);
					_prevYear = DateTime.Now.Year;
				}
				if (DateTime.Now.Month != _prevMonth) {
					OnMonthChanged (EventArgs.Empty);
					_prevMonth = DateTime.Now.Month;
				}
				if (DateTime.Now.Day != _prevDay) {
					OnDayChanged (EventArgs.Empty);
					_prevDay = DateTime.Now.Day;
				}
				if (DateTime.Now.Hour != _prevHour) {
					OnHourChanged (EventArgs.Empty);
					_prevHour = DateTime.Now.Hour;
				}
				if (DateTime.Now.Minute != _prevMinute) {
					OnMinuteChanged (EventArgs.Empty);
					_prevMinute = DateTime.Now.Minute;
				}
				if (DateTime.Now.Second != _prevSecond) {
					OnSecondChanged (EventArgs.Empty);
					_prevSecond = DateTime.Now.Second;
				}
			}
		}

		private static void PrintHeader()
		{
			Console.WriteLine ("A.L.Y.X. server on port " + (Server.Transport as Indigo.Transports.TCP.TCPTransport).Port.ToString ());
			Console.WriteLine ("press Ctrl+C for options when running interactively");
			Console.WriteLine ();
		}

		private static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e) {
			e.Cancel = true;
			mind.Stop ();

			while (true) {
				Console.Write ("ALYX> ");
				string line = Console.ReadLine ();

				if (line.StartsWith ("say ")) {
					Server.Speak (line.Substring (4));
					continue;
				}
				else if (line == "shutdown") {
					Server.Stop ();
					break;
				} else if (line == "exit") {
					Console.WriteLine ();
					PrintHeader ();
					mind.Start ();
					break;
				}
				else if (line == "clear") {
					Console.Clear ();
					continue;
				}
			}
		}

		private static void Server_ClientConnected(object sender, Indigo.ClientConnectedEventArgs e)
		{
			Console.WriteLine ("alyx-server: client connected");
		}
		
		private static Alyx.Networking.Server server = new Alyx.Networking.Server();
		public static Alyx.Networking.Server Server { get { return server; } }
	}
}
