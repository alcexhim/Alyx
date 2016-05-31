using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alyx.Linguistics.LanguageParts
{
	public class NounInstance : WordInstance, ISubject
	{
		public ArticleInstance Article
		{
			get { return Language.CurrentLanguage.GetArticle(mvarDefiniteness, mvarQuantity); }
			set
			{
				mvarDefiniteness = value.Definiteness;
				mvarQuantity = value.Quantity;
			}
		}

		private Quantity mvarQuantity = Quantity.Unspecified;
		public Quantity Quantity { get { return mvarQuantity; } set { mvarQuantity = value; } }

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
				if (!Word.GetClassProperty<bool>(WordClasses.Noun, "IsProper", false))
				{
					ArticleInstance article = this.Article;
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
			if (Word.GetClassProperty<bool>(WordClasses.Noun, "IsProper", false))
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

			// determine whether to apply the possessive "NOUN's PHRASE" construct for "PHRASE of NOUN"
			// TODO: figure out if target language supports it (e.g. Spanish and Japanese do not)
			int r = rand.Next (0, 1);
			if (r == 1)
			{
				if (mvarPrepositionalPhrase != null && mvarPrepositionalPhrase.Preposition.Word.ID == new Guid("{A31F3995-EE00-40F0-8DE4-984A0C5302B2}"))
				{
					sb.Clear ();
					sb.Append (new Series (mvarPrepositionalPhrase.Subjects).ToString ());
					sb.Append ("'s");
					sb.Append (' ');
					sb.Append (this.Word.Value);
				}
			}
			return sb.ToString();
		}

		private static Random rand = new Random();
	}
}
