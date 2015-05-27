using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alyx.Linguistics.LanguageParts
{
	public class PrepositionInstance : WordInstance
	{
		public PrepositionInstance(Word word) : base(word)
		{
			if (!word.Classes.Contains(WordClasses.Preposition)) throw new InvalidOperationException("Specified word cannot be used as an Preposition");
		}
	}
}
