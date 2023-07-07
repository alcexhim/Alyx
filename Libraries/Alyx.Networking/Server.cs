using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alyx.Networking.Protocols;
using UniversalEditor.Accessors;
using UniversalEditor.IO;

namespace Alyx.Networking
{
    public class Server : MBS.Networking.Server
    {
		private AlyxNetworkingProtocol ptp = new AlyxNetworkingProtocol();

		protected override void OnClientConnected(MBS.Networking.ClientConnectedEventArgs e)
		{
			base.OnClientConnected(e);

			MemoryAccessor ma = new MemoryAccessor();
			Writer writer = new Writer(ma);

			MBS.Networking.Protocols.PlainText.Request req = new MBS.Networking.Protocols.PlainText.Request();
			req.Method = "OPTIONS";
			req.Path = "/";
			req.Protocol = "Alyx/1.0";

			string packet = ptp.CreatePacket(req);
			writer.WriteFixedLengthString(packet);
			writer.Close();

			e.Client.Write(ma.ToArray());
			e.Client.Flush();

			e.Client.WaitForData();

			byte[] buffer = new byte[4096];
			e.Client.Read(buffer);

			string s = System.Text.Encoding.Default.GetString(buffer);

			MBS.Networking.Protocols.PlainText.Packet packet1 = MBS.Networking.Protocols.PlainText.Packet.Parse(s, "Alyx/1.0");
			if (packet1 is MBS.Networking.Protocols.PlainText.Response)
			{
				MBS.Networking.Protocols.PlainText.Response resp = (packet1 as MBS.Networking.Protocols.PlainText.Response);
				int code = resp.ResponseCode;
				if (code == 200)
				{
					foreach (MBS.Networking.Protocols.PlainText.Header header in resp.Headers)
					{
						Console.WriteLine ("    " + header.ToString ());
					}
					// we're good!
					Say_Hello (e.Client);
				}
			}
		}

		private Random random = new Random();

		private T GetRandomArrayItem<T>(T[] array)
		{
			int rand = random.Next(0, array.Length);
			return array [rand];
		}

		private string[] mvarUserNames = new string[] {
			"Michael"
		};

		private void Say_Hello(MBS.Networking.Client client)
		{
			// FIXME: these should be loaded in from Personality and XML
			string[] WelcomeLiterals = new string[]
			{
				"Good to see you again {0}",
				"Hello {0}",
				"Welcome back {0}"
			};

			Speak(String.Format(GetRandomArrayItem<string>(WelcomeLiterals), GetRandomArrayItem<string>(mvarUserNames)), client);
		}
		private void Say_Goodbye(MBS.Networking.Client client)
		{
			// FIXME: these should be loaded in from Personality and XML
			string[] GoodbyeLiterals = new string[]
			{
				"See you later {0}",
				"Goodbye {0}",
				"Have a nice day {0}"
			};

			Speak(String.Format(GetRandomArrayItem<string>(GoodbyeLiterals), GetRandomArrayItem<string>(mvarUserNames)), client);
		}

		/// <summary>
		/// Sends a SAY command over the wire to the connected client.
		/// </summary>
		/// <param name="value">The text to speak.</param>
		public void Speak(string message, MBS.Networking.Client client = null)
		{
			MBS.Networking.Protocols.PlainText.Request req = new MBS.Networking.Protocols.PlainText.Request();
			req.Method = "MESSAGE";
			req.Path = "/";
			req.Protocol = "alyx/1.0";

			req.Content = message;

			string content = ptp.CreatePacket(req);
			if (client == null)
			{
				/*
				foreach (MBS.Networking.Client cli in this.Clients)
				{
					cli.Transport.GetClientImplementation().Write(content);
				}
				*/
			}
			else
			{
				client.Write(content);
			}
		}

		public Server()
		{
			base.Protocol = ptp;
			base.Transport = new MBS.Networking.Transports.TCP.TCPTransport();
		}
	}
}
