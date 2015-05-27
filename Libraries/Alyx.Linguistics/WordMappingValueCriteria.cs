using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alyx.Linguistics.LanguageParts;

namespace Alyx.Linguistics
{
	public class WordMappingValueCriteria
	{
		public class WordMappingValueCriteriaCollection
			: System.Collections.ObjectModel.Collection<WordMappingValueCriteria>
		{

		}

		private Tense mvarTense = Tense.Unspecified;
		public Tense Tense { get { return mvarTense; } set { mvarTense = value; } }

		private Aspect mvarAspect = Aspect.Unspecified;
		public Aspect Aspect { get { return mvarAspect; } set { mvarAspect = value; } }

		private Person mvarPerson = Person.Unspecified;
		public Person Person { get { return mvarPerson; } set { mvarPerson = value; } }

		private Quantity mvarQuantity = Quantity.Unspecified;
		public Quantity Quantity { get { return mvarQuantity; } set { mvarQuantity = value; } }

		public bool Match(Word word)
		{
			if (word is Noun)
			{
				Noun noun = (word as Noun);
				return
				(
					((mvarQuantity == Linguistics.Quantity.Unspecified && noun.Quantity == Linguistics.Quantity.Unspecified)
					|| noun.Quantity == mvarQuantity)
				);
			}
			else if (word is Verb)
			{
				Verb verb = (word as Verb);
				return
				(
					(((mvarPerson == Linguistics.Person.Unspecified && verb.Person == Linguistics.Person.Unspecified) || verb.Person == mvarPerson)
					&& ((mvarAspect == Linguistics.Aspect.Unspecified && verb.Aspect == Linguistics.Aspect.Unspecified) || verb.Aspect == mvarAspect)
					&& ((mvarTense == Linguistics.Tense.Unspecified && verb.Tense == Linguistics.Tense.Unspecified) || verb.Tense == mvarTense))
				);
			}
			return true;
		}
	}
}
