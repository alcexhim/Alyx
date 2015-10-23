using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alyx.Linguistics
{
	public abstract class WordInstance
	{
		public class WordInstanceCollection : System.Collections.ObjectModel.Collection<WordInstance>
		{

		}

		private Word mvarWord = null;
		public Word Word { get { return mvarWord; } }

		public WordInstance(Word word)
		{
			mvarWord = word;
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();

			WordMapper mapper = null;
			if (mvarWord.Mapper != null) mapper = mvarWord.Mapper;

			if (mapper == null)
			{
				Language lang = Language.CurrentLanguage;
				
				List<WordMapper> mappers = new List<WordMapper>();
				if (lang != null)
				{
					foreach (WordMapper mapper1 in lang.WordMappers)
					{
						bool test = true;
						if (mapper1.Condition != null) test = mapper1.Condition.Test
						(
							new KeyValuePair<string, object>("Word", this.Word),
							new KeyValuePair<string, object>("WordClasses", this.Word.Classes),
							new KeyValuePair<string, object>("ID", this.Word.ID.ToString("B").ToUpper())
						);
						if (!test) continue;
						mappers.Add(mapper1);
					}
				}
				if (mappers.Count > 0) mapper = mappers[0];
			}

			string value = mapper.GetValue(this);
			sb.Append(value);
			return sb.ToString();
		}
	}
}
