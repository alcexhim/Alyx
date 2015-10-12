using System;

namespace Alyx.Speech
{
	public class SpeechEngineNotFoundException : Exception
	{
		private string mvarEngineName = null;
		public string EngineName { get { return mvarEngineName; } }

		public SpeechEngineNotFoundException(string engineName, Exception innerException)
			: base(String.Format("Speech engine '{0}' not found", engineName), innerException)
		{
			mvarEngineName = engineName;
		}
	}
}