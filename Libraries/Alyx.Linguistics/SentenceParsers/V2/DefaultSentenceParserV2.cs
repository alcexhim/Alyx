using System;
using System.Collections.Generic;

namespace Alyx.Linguistics.SentenceParsers.V2
{
	public class DefaultSentenceParserV2 : SentenceParser
	{
		#region implemented abstract members of SentenceParser
		protected override void ParseInternal (string value, ref Sentence sentence)
		{
			string[] words = value.Split (new char[] { ' ' });
			List<WordCandidate> candidates = new List<WordCandidate> ();
			for (int i = 0; i < words.Length; i++) {
				WordCandidate candidate = new WordCandidate (words [i]);

				if (words [i].EndsWith (",")) {
					words [i] = words [i].Substring (0, words [i].Length - 1);
					candidate.Series = true;
				}

				WordInstance[] instances = Language.CurrentLanguage.Words.GetWordInstances (candidate.Value);
				candidate.Instances.AddRange (instances);

				candidates.Add (candidate);
			}

			for (int i = 0; i < candidates.Count; i++) {
				if (candidates [i].Instances.ContainsClass (WordClasses.Article)) {
					// next candidate is either adjective or noun

				}
			}
		}
		#endregion
	}
}

