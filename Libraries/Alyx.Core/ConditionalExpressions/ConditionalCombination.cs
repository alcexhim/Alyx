using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alyx.Core.ConditionalExpressions
{
	/// <summary>
	/// The type of combination applied to a series of conditional statements.
	/// </summary>
	public enum ConditionCombination
	{
		/// <summary>
		/// Returns true if all of the conditional statements in this group are true.
		/// </summary>
		And,
		/// <summary>
		/// Returns true if at least one of the conditional statements in this group are true.
		/// </summary>
		Or,
		Xor
	}
}
