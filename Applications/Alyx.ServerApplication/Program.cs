using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MBS.Framework;

using Alyx.Configuration;
using Alyx.Thought;
using MBS.Networking;
using System.ComponentModel;
using Alyx.ServerApplication.SampleMindScripts;
using Alyx.Thought.MindOutputs;

namespace Alyx.ServerApplication
{
	class Program : MBS.Framework.Application
	{
		private static Mind mind = new Mind ();
		private static LocalMachine localMachine = new LocalMachine ();

		static int Main(string[] args)
		{
			return (new Program()).Start();
		}

		public Program()
		{
			ShortName = "alyx-server";
			CommandLine.Options.Add("port", 'p', 51221, CommandLineOptionValueType.Single, "The port on which to listen for incoming connections");
			CommandLine.Options.Add("help", CommandLineOptionValueType.None, null, '\0', "Display this help text");
		}

		public override string[] AdditionalShortNames => new string[] { "alyx" };

		protected override void OnActivated(ApplicationActivatedEventArgs e)
		{
			base.OnActivated(e);


			// some arguments we might include are:
			// --port nnnn	

			int port = 51221;
			CommandLineOption argPort = CommandLine.Options["port"];
			if (argPort.Value != null)
			{
				if (!Int32.TryParse(argPort.Value.ToString(), out port))
				{
					Console.WriteLine("Specified port must be a number");
					return;
				}
			}

			// initialize the local machine configuration
			localMachine.Load();

			// start the Alyx server
			Server.ClientConnected += Server_ClientConnected;
			Server.Transport = new MBS.Networking.Transports.TCP.TCPTransport();
			Server.Start();

			TextMindInput textMindInput = new TextMindInput("TextMindInput");
			mind.Properties.PropertyValueChanged += Mind_PropertyValueChanged;
			// configure the Alyx Mind
			// this is ugly. these are hardcoded scripts... we need to figure out how to do inputs and outputs
			mind.Scripts.Add(new SleepyMindScript());
			//mind.Scripts.Add(new SensorsMindScript());
			mind.Scripts.Add(new InternetConnectivityCheckerMindScript());
			mind.Scripts.Add(new TestValueMindScript());
			mind.Scripts.Add(new GoogleCalendarMindScript());

			// MindInput input = new MindInput();
			// MindOutput outputTextToSpeech = new TTSMindOutput();

			// FIXME: connect the output from SleepyMindScript to the TextToSpeechInput 

			// when Input is triggered, Output executes
			ConsoleMindOutput outputConsole = new ConsoleMindOutput();
			TextToSpeechMindOutput outputTextToSpeech = new TextToSpeechMindOutput(Server);
			mind.Connections.Add(new MindConnection(textMindInput, outputConsole));


			// foreach (MindConnection conn in mind.Connections)
			// if (conn.Input.should_execute...) then conn.Output.Execute()

			// start the Alyx Mind
			mind.Start();

			PrintHeader();
		}

		void Mind_PropertyValueChanged(object sender, PropertyValueChangedEventArgs e)
		{
			//Console.WriteLine("mind debug: the value of '{0}' used to be '{1}', is now '{2}'", mind.GetPropertyName(e.ID), e.OldValue, e.NewValue);
		}


		private class TextMindInput : IMindInput
		{
			public string Name { get; set; } = null;
			private string Value { get; set; } = null;

			public TextMindInput(string name)
			{
				Name = name;
			}

			public object GetValue()
			{
				return Value;
			}

			public void SetValue(object value)
			{
				Value = value?.ToString();
			}
		}

		protected override void OnStopping(CancelEventArgs e)
		{
			base.OnStopping(e);

			// make sure all threads have stopped
			mind.Stop();
			Server.Stop();
		}

		private static void PrintHeader()
		{
			Console.WriteLine ("A.L.Y.X. server on port " + Server.Protocol.Port.ToString ());
			Console.WriteLine ("press Ctrl+C for options when running interactively");
			Console.WriteLine ();
		}

		protected override void OnCancelKeyPress(ConsoleCancelEventArgs e)
		{
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
					Application.Instance.Stop ();
					break;
				} else if (line == "exit") {
					Console.WriteLine ();
					PrintHeader ();
					mind.Start ();
					break;
				}
				else if (line == "help")
				{
					Console.WriteLine("clear               clears the console");
					Console.WriteLine("exit                returns to REPL");
					Console.WriteLine("say (string)        requests server to speak string");
					Console.WriteLine("shutdown            shuts down the server");
					continue;
				}
				else if (line == "list")
				{
					IEnumerable<KeyValuePair<Guid, object>> kvps = mind.Properties.GetAll();
					foreach (KeyValuePair<Guid, object> kvp in kvps)
					{
						Console.WriteLine(String.Format("{0} = '{1}'", mind.Properties.GetName(kvp.Key), kvp.Value));
					}
				}
				else if (line == "clear") {
					Console.Clear ();
					continue;
				}
			}
		}

		private static void Server_ClientConnected(object sender, ClientConnectedEventArgs e)
		{
			Console.WriteLine ("alyx-server: client connected");

		}

		public static Alyx.Networking.Server Server { get; } = new Alyx.Networking.Server();
	}
}
