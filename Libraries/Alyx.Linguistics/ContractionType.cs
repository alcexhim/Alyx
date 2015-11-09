using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alyx.Linguistics
{
	public class ContractionType
	{
		public class ContractionTypeCollection
			: System.Collections.ObjectModel.Collection<ContractionType>
		{

		}

		private Guid mvarID = Guid.Empty;
		public Guid ID { get { return mvarID; } set { mvarID = value; } }

		private System.Collections.Specialized.StringCollection mvarPrefixes = new System.Collections.Specialized.StringCollection();
		public System.Collections.Specialized.StringCollection Prefixes { get { return mvarPrefixes; } }

		private string mvarContraction = String.Empty;
		public string Contraction { get { return mvarContraction; } set { mvarContraction = value; } }

		private string mvarValue = String.Empty;
		public string Value { get { return mvarValue; } set { mvarValue = value; } }
	}
}
