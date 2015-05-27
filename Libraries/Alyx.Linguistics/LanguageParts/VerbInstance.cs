using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alyx.Linguistics.LanguageParts
{
	public class VerbInstance : WordInstance
	{
		public class VerbInstanceCollection
			: System.Collections.ObjectModel.Collection<VerbInstance>
		{

		}

		public VerbInstance(Word word, Person person = Linguistics.Person.Unspecified, Tense tense = Linguistics.Tense.Unspecified, Aspect aspect = Linguistics.Aspect.Unspecified)
			: base (word)
		{
			if (!word.Classes.Contains(WordClasses.Verb)) throw new InvalidOperationException("Specified word cannot be used as a Verb");
			mvarPerson = person;
			mvarTense = tense;
			mvarAspect = aspect;
		}

		private AdverbInstance.AdverbInstanceCollection mvarAdverbs = new AdverbInstance.AdverbInstanceCollection();
		public AdverbInstance.AdverbInstanceCollection Adverbs { get { return mvarAdverbs; } }

		private Person mvarPerson = Person.Unspecified;
		public Person Person { get { return mvarPerson; } set { mvarPerson = value; } }

		private Tense mvarTense = Tense.Unspecified;
		public Tense Tense { get { return mvarTense; } set { mvarTense = value; } }

		private Aspect mvarAspect = Aspect.Unspecified;
		public Aspect Aspect { get { return mvarAspect; } set { mvarAspect = value; } }
	}
}
