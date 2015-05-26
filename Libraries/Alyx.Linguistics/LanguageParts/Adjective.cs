using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alyx.Linguistics.LanguageParts
{
	public class Adjective : Word
	{
		public class AdjectiveCollection
			: System.Collections.ObjectModel.Collection<Adjective>
		{

		}

		public Adjective(Guid id) : base(id) { }

		private Adverb.AdverbCollection mvarAdverbs = new Adverb.AdverbCollection();
		public Adverb.AdverbCollection Adverbs { get { return mvarAdverbs; } }
	}
}
