using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alyx.Networking
{
    public class Server : Indigo.Server
    {
		private Indigo.Protocols.PlainText.PlainTextProtocol ptp = new Indigo.Protocols.PlainText.PlainTextProtocol();
		
		public Server()
		{
			base.Protocol = ptp;
			base.Transport = new Indigo.Transports.TCP.TCPTransport(51220);
		}
    }
}
