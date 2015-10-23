using Alyx.Core.ConditionalExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alyx.Linguistics
{
	public class Article
	{
		public class ArticleCollection
			: System.Collections.ObjectModel.Collection<Article>
		{
			private Dictionary<string, Article> articlesByValue = new Dictionary<string, Article>();

			protected override void ClearItems()
			{
				base.ClearItems();
				articlesByValue.Clear();
			}
			protected override void InsertItem(int index, Article item)
			{
				base.InsertItem(index, item);
				articlesByValue[item.Value] = item;
			}
			protected override void RemoveItem(int index)
			{
				if (articlesByValue.ContainsKey(this[index].Value))
				{
					articlesByValue.Remove(this[index].Value);
				}
				base.RemoveItem(index);
			}
			protected override void SetItem(int index, Article item)
			{
				if (index >= 0 && index < Count)
				{
					if (articlesByValue.ContainsKey(this[index].Value)) articlesByValue.Remove(this[index].Value);
				}
				base.SetItem(index, item);
				articlesByValue[item.Value] = item;
			}

			public Article this[Definiteness definiteness, Quantity quantity]
			{
				get
				{
					foreach (Article article in this)
					{
						foreach (WordMapperMappingCriteria criteria in article.Criteria)
						{
							if (((criteria.Definiteness == Definiteness.Unspecified && definiteness == Definiteness.Unspecified) || criteria.Definiteness == definiteness)
								&& ((criteria.Quantity == Quantity.Unspecified && quantity == Quantity.Unspecified) || criteria.Quantity == quantity))
							{
								return article;
							}
						}
					}
					return null;
				}
			}
			public Article this[string value]
			{
				get
				{
					if (articlesByValue.ContainsKey(value)) return articlesByValue[value];
					return null;
				}
			}

		}

		private WordMapperMappingCriteria.WordMapperMappingCriteriaCollection mvarCriteria = new WordMapperMappingCriteria.WordMapperMappingCriteriaCollection();
		public WordMapperMappingCriteria.WordMapperMappingCriteriaCollection Criteria { get { return mvarCriteria; } }

		private IConditionalStatement mvarCondition = null;
		public IConditionalStatement Condition { get { return mvarCondition; } set { mvarCondition = value; } }

		private string mvarValue = String.Empty;
		public string Value { get { return mvarValue; } set { mvarValue = value; } }
	}
}
