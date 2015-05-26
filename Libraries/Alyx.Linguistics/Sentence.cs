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
	}
}
