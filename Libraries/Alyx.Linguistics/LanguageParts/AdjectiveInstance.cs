using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alyx.Linguistics.LanguageParts
{
	public class AdjectiveInstance : WordInstance, ISubject
	{
		public class AdjectiveCollection
			: System.Collections.ObjectModel.Collection<AdjectiveInstance>
		{

			public void AddRange(AdjectiveInstance[] values)
			{
				for (int i = 0; i < values.Length; i++)
				{
					this.Add (values [i]);
				}
			}

		}

		public AdjectiveInstance(Word word) : base(word)
		{
			if (!word.Classes.Contains(WordClasses.Adjective)) throw new InvalidOperationException("Specified word cannot be used as an Adjective");
		}

		private AdverbInstance.AdverbInstanceCollection mvarAdverbs = new AdverbInstance.AdverbInstanceCollection();
		public AdverbInstance.AdverbInstanceCollection Adverbs { get { return mvarAdverbs; } }
	}
}
