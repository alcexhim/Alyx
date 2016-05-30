using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Alyx.Linguistics.LanguageParts;

namespace Alyx.Linguistics.SentenceParsers.V1
{
	public class SentenceParserContext
	{
		private WordInstance mvarWord = null;
		public WordInstance Word { get { return mvarWord; } set { mvarWord = value; } }

		private ArticleInstance mvarArticle = null;
		public ArticleInstance Article { get { return mvarArticle; } set { mvarArticle = value; } }

		private Stack<Word> mvarUnknownWords = new Stack<Word>();
		/// <summary>
		/// A <see cref="Stack`1" /> of <see cref="Word" />s missing from the dictionary awaiting prediction.
		/// </summary>
		public Stack<Word> UnknownWords { get { return mvarUnknownWords; } }

		private Stack<ConjunctionInstance> mvarConjunctions = new Stack<ConjunctionInstance>();
		public Stack<ConjunctionInstance> Conjunctions { get { return mvarConjunctions; } }

		private List<AdjectiveInstance> mvarAdjectives = new List<AdjectiveInstance>();
		public List<AdjectiveInstance> Adjectives { get { return mvarAdjectives; } }

		private Clause mvarClause = null;
		public Clause Clause { get { return mvarClause; } set { mvarClause = value; } }

		private PrepositionInstance mvarPreposition = null;
		public PrepositionInstance Preposition { get { return mvarPreposition; } set { mvarPreposition = value; } }

		private ISubject mvarPredicateObject = null;
		public ISubject PredicateObject { get { return mvarPredicateObject; } set { mvarPredicateObject = value; } }

		private VerbInstance mvarVerb = null;
		public VerbInstance Verb { get { return mvarVerb; } set { mvarVerb = value; } }
	}
}
