using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alyx.Linguistics
{
	public abstract class WordInstance
	{
		public class WordInstanceCollection : System.Collections.Generic.List<WordInstance>
		{

			public bool ContainsClass(WordClass clasz)
			{
				foreach (WordInstance inst in this) {
					if (inst.Word.Classes.Contains (clasz))
						return true;
				}
				return false;
			}
			public WordInstance[] GetByClass(WordClass clasz)
			{
				List<WordInstance> list = new List<WordInstance> ();
				foreach (WordInstance inst in this) {
					if (inst.Word.Classes.Contains (clasz))
						list.Add (inst);
				}
				return list.ToArray ();
			}

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

			string value = this.Word.Value;
			if (mapper != null) value = mapper.GetValue(this);
			if (value == null) value = this.Word.Value;

			sb.Append(value);
			return sb.ToString();
		}
	}
}
