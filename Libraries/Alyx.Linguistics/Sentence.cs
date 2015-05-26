using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alyx.Linguistics
{
	public class Sentence
	{
		private Clause.ClauseCollection mvarClauses = new Clause.ClauseCollection();
		public Clause.ClauseCollection Clauses { get { return mvarClauses; } }

		public Sentence()
		{
		}
		public Sentence(Clause[] clauses)
		{
			foreach (Clause item in clauses)
			{
				mvarClauses.Add(item);
			}
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			foreach (Clause clause in mvarClauses)
			{
				sb.Append(clause.ToString());
			}
			string text = sb.ToString();
			return sb.ToString();
		}
	}
}
