using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alyx.Linguistics
{
	public abstract class Word
	{
		private Guid mvarID = Guid.Empty;
		public Guid ID { get { return mvarID; } }

		public Word(Guid id)
		{
			mvarID = id;
		}
	}
}
