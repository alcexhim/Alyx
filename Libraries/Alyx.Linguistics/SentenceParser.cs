using System;

namespace Alyx.Linguistics
{
	public abstract class SentenceParser
	{
		private Language mvarLanguage = null;
		public Language Language { get { return mvarLanguage; } set { mvarLanguage = value; } }

		public Sentence Parse(string value)
		{
			if (mvarLanguage == null)
				mvarLanguage = Language.CurrentLanguage;

			value = mvarLanguage.ReplaceContractions(value);

			SentenceType type = null;
			foreach (SentenceTypeMapping mapping in mvarLanguage.SentenceTypeMappings)
			{
				if ((mapping.Prefix == null || value.StartsWith(mapping.Prefix)) && (mapping.Suffix == null || value.EndsWith(mapping.Suffix)))
				{
					if (mapping.ID == SentenceTypes.Declarative.ID)
					{
						type = SentenceTypes.Declarative;
						break;
					}
					else if (mapping.ID == SentenceTypes.Exclamatory.ID)
					{
						type = SentenceTypes.Exclamatory;
						break;
					}
					else if (mapping.ID == SentenceTypes.Imperative.ID)
					{
						type = SentenceTypes.Imperative;
						break;
					}
					else if (mapping.ID == SentenceTypes.Interrogative.ID)
					{
						type = SentenceTypes.Interrogative;
						break;
					}
				}
			}

			Sentence sentence = new Sentence (type);
			ParseInternal (value, ref sentence);
			return sentence;
		}
		protected abstract void ParseInternal(string value, ref Sentence sentence);
	}
}

