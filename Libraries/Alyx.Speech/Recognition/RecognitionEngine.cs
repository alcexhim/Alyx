using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alyx.Speech.Recognition
{
	public abstract class RecognitionEngine
	{
		/// <summary>
		/// Starts listening for speech recognition.
		/// </summary>
		public void Start()
		{
			StartInternal();
		}
		/// <summary>
		/// Stops listening for speech recognition.
		/// </summary>
		public void Stop()
		{
			StopInternal();
		}

		protected abstract void StartInternal();
		protected abstract void StopInternal();
	}
}
