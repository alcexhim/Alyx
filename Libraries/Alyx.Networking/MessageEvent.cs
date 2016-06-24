using System;

namespace Alyx.Networking
{
	public delegate void MessageEventHandler(object sender, MessageEventArgs e);
	public class MessageEventArgs : EventArgs
	{
		private string mvarMessage = String.Empty;
		public string Message { get { return mvarMessage; } set { mvarMessage = value; } }

		private bool mvarWaitUntilFinished = false;
		public bool WaitUntilFinished { get { return mvarWaitUntilFinished; } set { mvarWaitUntilFinished = value; } }

		public MessageEventArgs (string message, bool waitUntilFinished = false)
		{
			mvarMessage = message;
			mvarWaitUntilFinished = waitUntilFinished;
		}
	}
}

