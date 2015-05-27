using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alyx.Linguistics
{
	public static class WordClasses
	{
		private static WordClass mvarNoun = new WordClass(new Guid("{225DF6CF-D334-4227-A308-B31F709B82DA}"));
		/// <summary>
		/// A word or lexical item denoting any abstract or concrete entity; a person (ex. police
		/// officer, Michael); place (ex. coastline, London); thing (ex. necktie, television); idea
		/// (ex. happiness); or quality (ex. bravery)
		/// </summary>
		public static WordClass Noun { get { return mvarNoun; } }

		private static WordClass mvarPronoun = new WordClass(new Guid("{916C924A-0FD2-4939-9CD8-1441FF7B5E64}"));
		/// <summary>
		/// An abstract substitute for a noun or noun phrase (ex. them, he).
		/// </summary>
		public static WordClass Pronoun { get { return mvarPronoun; } }

		private static WordClass mvarAdjective = new WordClass(new Guid("{97A4620E-686D-4F4F-9E99-FD104D16DBCE}"));
		/// <summary>
		/// A qualifier of a noun or pronoun (ex. big, brave).
		/// </summary>
		public static WordClass Adjective { get { return mvarAdjective; } }

		private static WordClass mvarVerb = new WordClass(new Guid("{3661BAB1-0FE0-47AB-BCD8-BAE3F9FBDC1B}"));
		/// <summary>
		/// A word denoting an action (ex. walk), occurrence (ex. happen), or state of being (ex. be).
		/// </summary>
		public static WordClass Verb { get { return mvarVerb; } }

		private static WordClass mvarAdverb = new WordClass(new Guid("{C6C06034-6FED-4815-9121-516871A9DE93}"));
		/// <summary>
		/// A qualifier of an adjective, verb, clause, sentence, or other adverb (ex. very, quite).
		/// </summary>
		public static WordClass Adverb { get { return mvarAdverb; } }

		private static WordClass mvarPreposition = new WordClass(new Guid("{C08DF48F-EDC8-4DF5-BC93-491A3D93B5CD}"));
		/// <summary>
		/// An establisher of relation and syntactic context (ex. in, of).
		/// </summary>
		public static WordClass Preposition { get { return mvarPreposition; } }

		private static WordClass mvarConjunction = new WordClass(new Guid("{E63F6105-A6FA-4A4D-BB82-12695D4FF760}"));
		/// <summary>
		/// A syntactic connector (ex. and, but).
		/// </summary>
		public static WordClass Conjunction { get { return mvarConjunction; } }

		private static WordClass mvarInterjection = new WordClass(new Guid("{87394734-657E-462F-93ED-08FAC2365ABC}"));
		/// <summary>
		/// An emotional greeting or exclamation (ex. Hurrah, Alas).
		/// </summary>
		public static WordClass Interjection { get { return mvarInterjection; } }

		private static WordClass mvarArticle = new WordClass(new Guid("{96157C04-35B6-456D-B9C0-949A806BE70D}"));
		/// <summary>
		/// A grammatical marker of definiteness (ex. the) or indefiniteness (ex. a, an).
		/// </summary>
		public static WordClass Article { get { return mvarArticle; } }
	}
}
