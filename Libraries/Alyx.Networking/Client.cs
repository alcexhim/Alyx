using Indigo.Protocols.PlainText;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
	}
}
