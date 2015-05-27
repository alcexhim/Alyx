using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alyx.Linguistics.LanguageParts;

namespace Alyx.Linguistics
{
	public class WordMapperMappingCriteria
	{
		public class WordMapperMappingCriteriaCollection
			: System.Collections.ObjectModel.Collection<WordMapperMappingCriteria>
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

		public bool Match(WordInstance word)
		{
			if (word is NounInstance)
			{
				NounInstance noun = (word as NounInstance);
				return
				(
					((mvarQuantity == Linguistics.Quantity.Unspecified && noun.Quantity == Linguistics.Quantity.Unspecified)
					|| noun.Quantity == mvarQuantity)
				);
			}
			else if (word is VerbInstance)
			{
				VerbInstance verb = (word as VerbInstance);
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
