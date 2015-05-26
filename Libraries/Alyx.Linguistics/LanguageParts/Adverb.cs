using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alyx.Linguistics.LanguageParts
{
	public class Adverb : Adjective
	{
		public class AdverbCollection
			: System.Collections.ObjectModel.Collection<Adverb>
		{

		}

		public Adverb(Guid id) : base(id) { }
	}
}
