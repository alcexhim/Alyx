using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alyx.Core.ConditionalExpressions
{
	/// <summary>
	/// The type of comparison to use with the conditional statement.
	/// </summary>
	public enum ConditionComparison
	{
		/// <summary>
		/// Returns true if the two values are equal by value.
		/// </summary>
		Equal = 1,
		/// <summary>
		/// Returns true if the two values are equal by reference (or by value if they are value types).
		/// </summary>
		ReferenceEqual = 2,
		/// <summary>
		/// Returns true if the first value is greater than the second value.
		/// </summary>
		GreaterThan = 3,
		/// <summary>
		/// Returns true if the first value is less than the second value.
		/// </summary>
		LessThan = 4,
		/// <summary>
		/// Determines if a string expression starts with the given value.
		/// </summary>
		StartsWith = 5,
		/// <summary>
		/// Determines if a string expression ends with the given value.
		/// </summary>
		EndsWith = 6,
		/// <summary>
		/// Determines if a string or string array contains the given value.
		/// </summary>
		Contains = 7
	}
}
