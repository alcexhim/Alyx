using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.Accessors;
using UniversalEditor.IO;

namespace Alyx.Networking
{
    public class Server : Indigo.Server
    {
		private Indigo.Protocols.PlainText.PlainTextProtocol ptp = new Indigo.Protocols.PlainText.PlainTextProtocol();

		protected override void OnClientConnected(Indigo.ClientConnectedEventArgs e)
		{
			base.OnClientConnected(e);

			MemoryAccessor ma = new MemoryAccessor();
			Writer writer = new Writer(ma);

			Indigo.Protocols.PlainText.Request req = new Indigo.Protocols.PlainText.Request();
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

			Indigo.Protocols.PlainText.Packet packet1 = Indigo.Protocols.PlainText.Packet.Parse(s, "Alyx/1.0");
			if (packet1 is Indigo.Protocols.PlainText.Response)
			{
				Indigo.Protocols.PlainText.Response resp = (packet1 as Indigo.Protocols.PlainText.Response);
				int code = resp.ResponseCode;
				if (code == 200)
				{
					foreach (Indigo.Protocols.PlainText.Header header in resp.Headers) {
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

		private void Say_Hello(Indigo.Client client)
		{
			string[] WelcomeLiterals = new string[]
			{
				"Good to see you again {0}",
				"Hello {0}",
				"Welcome back {0}"
			};

			Speak(String.Format(GetRandomArrayItem<string>(WelcomeLiterals), GetRandomArrayItem<string>(mvarUserNames)), client);
		}
		private void Say_Goodbye(Indigo.Client client)
		{
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
		public void Speak(string message, Indigo.Client client = null)
		{
			Indigo.Protocols.PlainText.Request req = new Indigo.Protocols.PlainText.Request();
			req.Method = "MESSAGE";
			req.Path = "/";
			req.Protocol = "alyx/1.0";

			req.Content = message;

			string content = ptp.CreatePacket(req);
			if (client == null) {
				foreach (Indigo.Client cli in this.Clients) {
					cli.Transport.GetClientImplementation ().Write (content);
				}
			} else { 
				client.Transport.GetClientImplementation ().Write (content);
			}
		}

		public Server()
		{
			base.Protocol = ptp;
			base.Transport = new Indigo.Transports.TCP.TCPTransport(51220);
		}
	}
}
