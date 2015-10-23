using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alyx.Linguistics.LanguageParts
{
	public class NounInstance : WordInstance, INounInstance
	{
		private Definiteness mvarDefiniteness = Definiteness.Unspecified;
		public Definiteness Definiteness { get { return mvarDefiniteness; } set { mvarDefiniteness = value; } }

		private AdjectiveInstance.AdjectiveCollection mvarAdjectives = new AdjectiveInstance.AdjectiveCollection();
		public AdjectiveInstance.AdjectiveCollection Adjectives { get { return mvarAdjectives; } }

		/// <summary>
		/// Creates a noun with the specified name.
		/// </summary>
		/// <param name="name">The name of the word in the default language.</param>
		/// <returns></returns>
		public NounInstance(Word word, AdjectiveInstance[] adjectives = null) : base(word)
		{
			if (!word.Classes.Contains(WordClasses.Noun)) throw new InvalidOperationException("Specified word cannot be used as a Noun");

			if (adjectives != null)
			{
				foreach (AdjectiveInstance adj in adjectives)
				{
					mvarAdjectives.Add(adj);
				}
			}
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();

			Language lang = Language.CurrentLanguage;
			if (lang != null)
			{
				ArticleInstance article = lang.GetArticle(mvarDefiniteness, mvarQuantity);
				if (article != null)
				{
					string value = article.ToString();
					if (!String.IsNullOrEmpty(value))
					{
						sb.Append(article.ToString());
						sb.Append(' ');
					}
				}
			}
			if (mvarAdjectives.Count > 0)
			{
				for (int i = 0; i < mvarAdjectives.Count; i++)
				{
					sb.Append(mvarAdjectives[i].ToString());
					if (i < mvarAdjectives.Count - 1)
					{
						sb.Append(',');
						sb.Append(' ');
					}
				}
				sb.Append(' ');
			}
			sb.Append(base.ToString());
			return sb.ToString();
		}

		private Quantity mvarQuantity = Quantity.Unspecified;
		public Quantity Quantity { get { return mvarQuantity; } set { mvarQuantity = value; } }
	}
}
