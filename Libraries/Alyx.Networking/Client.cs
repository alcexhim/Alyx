using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.Accessors;
using UniversalEditor.IO;

using MBS.Networking.Protocols.PlainText;
using Alyx.Networking.Protocols;
using MBS.Networking;

namespace Alyx.Networking
{
	public class Client : MBS.Networking.Client
	{
		private AlyxNetworkingProtocol ptp = new AlyxNetworkingProtocol();

		public Client()
		{
			base.Protocol = ptp;
			base.Service = null;
			base.Transport = new MBS.Networking.Transports.TCP.TCPTransport();
		}

		public void SendChatMessage(string message)
		{
			Request req = new Request();
			req.Method = "MESSAGE";
			req.Path = "/";
			req.Protocol = "alyx/1.0";

			req.Content = message;

			string content = ptp.CreatePacket(req);
			Write(content);
		}

		public event MessageEventHandler MessageReceived;
		protected virtual void OnMessageReceived(MessageEventArgs e)
		{
			if (MessageReceived != null)
				MessageReceived (this, e);
		}

		protected override void OnDataReceived(DataReceivedEventArgs e)
		{
			base.OnDataReceived(e);

			Packet packet = ptp.ReadPacket(e.Client.GetStream(), "Alyx/1.0");
			if (packet is Request)
			{
				Request req = (packet as Request);
				if (req.Method == "OPTIONS")
				{
					Response resp = new Response(200, "OK", "Alyx/1.0");

					resp.Headers.Add ("User-Name", System.Environment.UserName);
					resp.Headers.Add ("Domain-Name", System.Environment.UserDomainName);
					resp.Headers.Add ("Computer-Name", System.Environment.MachineName);
					resp.Headers.Add ("Processor-Count", System.Environment.ProcessorCount.ToString ());
					resp.Headers.Add ("Command-Line", System.Environment.CommandLine);
					
					resp.Headers.Add ("OS-Platform", System.Environment.OSVersion.Platform.ToString ());
					resp.Headers.Add ("OS-ServicePack", System.Environment.OSVersion.ServicePack);
					resp.Headers.Add ("OS-Version", System.Environment.OSVersion.Version.ToString ());
					resp.Headers.Add ("OS-Version-String", System.Environment.OSVersion.VersionString);
					
					string data = ptp.CreatePacket(resp);
					
					MemoryAccessor ma = new MemoryAccessor();
					Writer writer = new Writer(ma);
					writer.Write(data);
					writer.Close();

					byte[] buffer = ma.ToArray();
					e.Client.Write(buffer, 0, buffer.Length);
				}
				else if (req.Method == "MESSAGE")
				{
					bool waitUntilFinished = false;
					Header header = req.Headers ["Wait-Until-Finished"];
					if (header != null)
						waitUntilFinished = (header.Value.ToLower ().Equals ("true"));

					OnMessageReceived (new MessageEventArgs (req.Content, waitUntilFinished));
				}
			}
		}
	}
}
