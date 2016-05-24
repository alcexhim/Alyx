using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alyx.Linguistics.LanguageParts
{
	public class NounInstance : WordInstance, ISubject
	{
		private Definiteness mvarDefiniteness = Definiteness.Unspecified;
		public Definiteness Definiteness { get { return mvarDefiniteness; } set { mvarDefiniteness = value; } }

		private AdjectiveInstance.AdjectiveCollection mvarAdjectives = new AdjectiveInstance.AdjectiveCollection();
		public AdjectiveInstance.AdjectiveCollection Adjectives { get { return mvarAdjectives; } }

		private PrepositionalPhrase mvarPrepositionalPhrase = null;
		public PrepositionalPhrase PrepositionalPhrase { get { return mvarPrepositionalPhrase; } set { mvarPrepositionalPhrase = value; } }

		/// <summary>
		/// Creates a noun from the specified <see cref="Word" />.
		/// </summary>
		/// <param name="word">The <see cref="Word" /> used to create this <see cref="NounInstance" />.</param>
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
				// proper nouns don't get articles
				if (!mvarIsProper)
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

			string str = base.ToString();
			if (mvarIsProper)
			{
				// TODO: replace with configuration from current language
				if (str.Length > 1)
				{
					str = str.Substring(0, 1).ToUpper() + str.Substring(1);
				}
				else
				{
					str = str.ToUpper();
				}
			}
			sb.Append(str);

			if (mvarPrepositionalPhrase != null) {
				sb.Append (' ');
				sb.Append (mvarPrepositionalPhrase.ToString ());
			}
			return sb.ToString();
		}

		private Quantity mvarQuantity = Quantity.Unspecified;
		public Quantity Quantity { get { return mvarQuantity; } set { mvarQuantity = value; } }

		private bool mvarIsProper = false;
		public bool IsProper { get { return mvarIsProper; } set { mvarIsProper = value; } }
	}
}
