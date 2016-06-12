using System;
using System.Collections.Generic;

using Alyx.Linguistics.LanguageParts;

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

			for (int iw = 0; iw < candidates.Count; iw++) {
				foreach (WordPattern pattern in Language.CurrentLanguage.WordPatterns)
				{
					bool patternWrong = false;

					for (int ip = 0; ip < pattern.Parts.Count; ip++) {
						if (pattern.Parts [ip].WordClass != null) {
							if (!(candidates [iw].Instances.ContainsClass (pattern.Parts [ip].WordClass))) {
								if (!pattern.Parts [ip].IsOptional) {
									// the currently-selected pattern part is not optional, so if we lose here our pattern is wrong
									if (iw + 1 < candidates.Count) {
										// we are not the last candidate, so check to see if the next candidate has an instance
										if (candidates [iw].Instances.Count == 0 && candidates [iw + 1].Instances.Count > 0) {
											// our current candidate doesn't have a instance, but the next candidate does

											if (!(candidates [iw + 1].Instances.ContainsClass (pattern.Parts [ip].WordClass))) {
												// next candidate has a WordInstance which doesn't match the pattern
												// so assume our current candidate is an unknown and create a WordInstance for it

												if (pattern.Parts [ip].WordClass == WordClasses.Noun) {
													Word word = new Word (Guid.NewGuid ());
													word.Value = candidates [iw].Value;
													word.Sources.Add (Language.CurrentLanguage.WordSources [WordSourceGuids.Learned]);
													word.Classes.Add (WordClasses.Noun);
													Language.CurrentLanguage.Words.Add (word);

													NounInstance inst = new NounInstance (word);
													candidates [iw].Instances.Add (inst);
													candidates [iw].PreferredInstance = inst;

													if (candidates [iw - 1].PreferredInstance is ArticleInstance) {
														inst.Article = (candidates [iw - 1].PreferredInstance as ArticleInstance);
													}
													continue;
												}
											}
										}
									}
									patternWrong = true;
									break;
								}
							} else {
								WordInstance[] insts = candidates [iw].Instances.GetByClass (pattern.Parts [ip].WordClass);
								if (insts.Length > 0) {
									candidates [iw].PreferredInstance = insts [0];
									iw++;
								}
							}
						}
					}

					if (patternWrong)
						continue;
				}
			}
		}
		#endregion
	}
}

