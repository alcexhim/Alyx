using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alyx.Linguistics
{
	public class Word
	{
		public class WordCollection
			: System.Collections.ObjectModel.Collection<Word>
		{
			public Word this[Guid id]
			{
				get
				{
					foreach (Word item in this)
					{
						if (item.ID == id) return item;
					}
					return null;
				}
			}
		}

		private Guid mvarID = Guid.Empty;
		public Guid ID { get { return mvarID; } }

		private string mvarValue = String.Empty;
		public string Value { get { return mvarValue; } set { mvarValue = value; } }

		private WordClass.WordClassCollection mvarClasses = new WordClass.WordClassCollection();
		/// <summary>
		/// The <see cref="WordClass" />es that are applicable to this <see cref="Word" />.
		/// </summary>
		public WordClass.WordClassCollection Classes { get { return mvarClasses; } }

		private WordMapper mvarMapper = null;
		public WordMapper Mapper { get { return mvarMapper; } set { mvarMapper = value; } }

		public Word(Guid id)
		{
			mvarID = id;
		}
	}
}
