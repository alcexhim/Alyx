using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.Accessors;
using UniversalEditor.IO;

using Indigo.Protocols.PlainText;

namespace Alyx.Networking
{
	public class Client : Indigo.Client
	{
		private Indigo.Protocols.PlainText.PlainTextProtocol ptp = new Indigo.Protocols.PlainText.PlainTextProtocol();

		public Client()
		{
			base.Protocol = ptp;
			base.Service = null;
			base.Transport = new Indigo.Transports.TCP.TCPTransport(51221);
		}

		public void SendChatMessage(string message)
		{
			Request req = new Request();
			req.Method = "MESSAGE";
			req.Path = "/";
			req.Protocol = "alyx/1.0";

			req.Content = message;

			string content = ptp.CreatePacket(req);

			// base.Transport.Send(content);
		}

		protected override void OnDataReceived(Indigo.DataReceivedEventArgs e)
		{
			base.OnDataReceived(e);

			Packet packet = ptp.ReadPacket(e.Stream, "Alyx/1.0");
			if (packet is Request)
			{
				Request req = (packet as Request);
				if (req.Method == "OPTIONS")
				{
					Response resp = new Response(200, "OK", "Alyx/1.0");
					
					string data = ptp.CreatePacket(resp);
					
					MemoryAccessor ma = new MemoryAccessor();
					Writer writer = new Writer(ma);
					writer.Write(data);
					writer.Close();

					byte[] buffer = ma.ToArray();
					e.Stream.Write(buffer, 0, buffer.Length);
				}
			}
		}
	}
}
