using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Alyx.Linguistics;
using Alyx.Thought;

namespace Alyx.Linguistics.Thought
{
    public class WordInstanceIdeaRepresentation : IdeaRepresentation
    {
		private WordInstance mvarWordInstance = null;
		public WordInstance WordInstance { get { return mvarWordInstance; } }

		public WordInstanceIdeaRepresentation(WordInstance word)
		{
			mvarWordInstance = word;
		}

		public override string ToString()
		{
			return mvarWordInstance.ToString();
		}
    }
}
