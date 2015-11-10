using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alyx.Thought
{
	public class Emotion
	{
		public class EmotionCollection
			: System.Collections.ObjectModel.Collection<Emotion>
		{

		}

		private EmotionClass mvarEmotionClass = null;
		public EmotionClass EmotionClass { get { return mvarEmotionClass; } }

		private double mvarValue = 0.0;
		public double Value { get { return mvarValue; } set { mvarValue = value; } }

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			if (mvarEmotionClass != null)
			{
				sb.Append(mvarEmotionClass.ToString());
				sb.Append(": ");
			}
			sb.Append(mvarValue.ToString());
			return sb.ToString();
		}
	}
}
