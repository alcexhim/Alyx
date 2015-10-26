using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alyx.Linguistics.LanguageParts
{
	public class ConjunctionInstance : WordInstance
	{
		/// <summary>
		/// Creates a conjunction from the specified <see cref="Word" />.
		/// </summary>
		/// <param name="word">The <see cref="Word" /> used to create this <see cref="ConjunctionInstance" />.</param>
		/// <returns></returns>
		public ConjunctionInstance(Word word)
			: base(word)
		{
			if (!word.Classes.Contains(WordClasses.Conjunction)) throw new InvalidOperationException("Specified word cannot be used as a Conjunction");

		}
	}
}
