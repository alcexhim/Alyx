using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alyx.Core
{
	public static class CollectionExtensions
	{
		public static void AddRange<T>(this System.Collections.Generic.ICollection<T> collection, params T[] values)
		{
			foreach (T value in values)
			{
				collection.Add(value);
			}
		}
	}
}
