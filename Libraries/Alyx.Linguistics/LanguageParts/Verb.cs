using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alyx.Linguistics.LanguageParts
{
	public class Verb : Word
	{
		public class AdverbCollection
			: System.Collections.ObjectModel.Collection<Adverb>
		{

		}

		public Verb(Guid id) : base(id) { }

		private Person mvarPerson = Person.Unspecified;
		public Person Person { get { return mvarPerson; } set { mvarPerson = value; } }

		private Tense mvarTense = Tense.Unspecified;
		public Tense Tense { get { return mvarTense; } set { mvarTense = value; } }

		private Aspect mvarAspect = Aspect.Unspecified;
		public Aspect Aspect { get { return mvarAspect; } set { mvarAspect = value; } }
	}
}
