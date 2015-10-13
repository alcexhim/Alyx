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
					// we're good!
				}
			}
		}

		public Server()
		{
			base.Protocol = ptp;
			base.Transport = new Indigo.Transports.TCP.TCPTransport(51220);
		}
    }
}
