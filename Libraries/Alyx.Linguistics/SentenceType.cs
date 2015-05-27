using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alyx.Linguistics
{
	public class SentenceType
	{
		private Guid mvarID = Guid.Empty;
		public Guid ID { get { return mvarID; } }

		public SentenceType(Guid id)
		{
			mvarID = id;
		}
	}
}
