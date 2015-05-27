using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alyx.Linguistics.LanguageParts
{
	public class AdverbInstance : AdjectiveInstance
	{
		public class AdverbInstanceCollection
			: System.Collections.ObjectModel.Collection<AdverbInstance>
		{

		}
		
		public AdverbInstance(Word word) : base(word)
		{
			if (!word.Classes.Contains(WordClasses.Adverb)) throw new InvalidOperationException("Specified word cannot be used as an Adverb");
		}
	}
}
