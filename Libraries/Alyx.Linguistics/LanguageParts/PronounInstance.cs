using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alyx.Linguistics.LanguageParts
{
	public class PronounInstance : WordInstance, INounInstance
	{
		public PronounInstance(Word word) : base(word)
		{
			if (!word.Classes.Contains(WordClasses.Pronoun)) throw new InvalidOperationException("Specified word cannot be used as a Pronoun");
		}

		private Person mvarPerson = Person.Unspecified;
		public Person Person { get { return mvarPerson; } set { mvarPerson = value; } }

		private Quantity mvarQuantity = Quantity.Unspecified;
		public Quantity Quantity { get { return mvarQuantity; } set { mvarQuantity = value; } }

		private Gender mvarGender = Genders.Unspecified;
		public Gender Gender { get { return mvarGender; } set { mvarGender = value; } }
	}
}
