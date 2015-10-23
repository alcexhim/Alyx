using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alyx.Linguistics.LanguageParts
{
	public class ArticleInstance : WordInstance
	{
		public ArticleInstance(Word word, Definiteness definiteness = Linguistics.Definiteness.Unspecified, Quantity quantity = Linguistics.Quantity.Unspecified) : base(word)
		{
			if (!word.Classes.Contains(WordClasses.Article)) throw new InvalidOperationException("Specified word cannot be used as an Article");
			mvarDefiniteness = definiteness;
			mvarQuantity = quantity;
		}

		private Definiteness mvarDefiniteness = Definiteness.Unspecified;
		public Definiteness Definiteness { get { return mvarDefiniteness; } set { mvarDefiniteness = value; } }

		private Quantity mvarQuantity = Quantity.Unspecified;
		public Quantity Quantity { get { return mvarQuantity; } set { mvarQuantity = value; } }
	}
}
