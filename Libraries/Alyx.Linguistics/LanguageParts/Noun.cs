using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alyx.Linguistics.LanguageParts
{
	public class Noun : Word, ISubject
	{
		private Definiteness mvarDefiniteness = Definiteness.Unspecified;
		public Definiteness Definiteness { get { return mvarDefiniteness; } set { mvarDefiniteness = value; } }

		private Adjective.AdjectiveCollection mvarAdjectives = new Adjective.AdjectiveCollection();
		public Adjective.AdjectiveCollection Adjectives { get { return mvarAdjectives; } }

		public static Noun GetPronoun(Person person, Quantity quantity)
		{
			return null;
		}

		/// <summary>
		/// Creates a noun with the specified name.
		/// </summary>
		/// <param name="name">The name of the word in the default language.</param>
		/// <returns></returns>
		public Noun(Guid id, Adjective[] adjectives = null) : base(id)
		{
			if (adjectives != null)
			{
				foreach (Adjective adj in adjectives)
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
				string article = lang.GetArticle(mvarDefiniteness, mvarQuantity);
				if (article != null)
				{
					sb.Append(article);
					sb.Append(" ");
				}
			}
			if (mvarAdjectives.Count > 0)
			{
				for (int i = 0; i < mvarAdjectives.Count; i++)
				{
					sb.Append(mvarAdjectives[i].ToString());
					if (i < mvarAdjectives.Count - 1) sb.Append(", ");
				}
				sb.Append(" ");
			}
			sb.Append(base.ToString());
			return sb.ToString();
		}

		private Quantity mvarQuantity = Quantity.Unspecified;
		public Quantity Quantity { get { return mvarQuantity; } set { mvarQuantity = value; } }
	}
}
