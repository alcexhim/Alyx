using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alyx.Linguistics.LanguageParts
{
	public class Noun : Word, ISubject
	{
		private Noun(Guid id) : base(id) { }

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
		public static Noun Create(Guid id)
		{
			Noun item = new Noun(id);
			return item;
		}
	}
}
