using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alyx.Core.ConditionalExpressions
{
	/// <summary>
	/// A collection of <see cref="IConditionalStatement" />s
	/// </summary>
	public class ConditionalStatementCollection
		: System.Collections.ObjectModel.Collection<IConditionalStatement>
	{

	}
	/// <summary>
	/// Defines the minimum functionality required to implement a conditional statement (either a
	/// <see cref="Condition" /> itself or a <see cref="ConditionGroup" /> of multiple
	/// <see cref="Condition" />s.
	/// </summary>
	public interface IConditionalStatement
	{
		/// <summary>
		/// Evaluates the conditional statement based on the given criteria.
		/// </summary>
		/// <param name="propertyValues">The set of values against which to evaluate the conditional statement.</param>
		/// <returns>True if the conditions are satisfied; false otherwise.</returns>
		bool Test(params System.Collections.Generic.KeyValuePair<string, object>[] propertyValues);
		/// <summary>
		/// Evaluates the conditional statement based on the given criteria.
		/// </summary>
		/// <param name="propertyValues">The set of values against which to evaluate the conditional statement.</param>
		/// <returns>True if the conditions are satisfied; false otherwise.</returns>
		bool Test(System.Collections.Generic.Dictionary<string, object> propertyValues);
		/// <summary>
		/// Evaluates the conditional statement based on the given criterion.
		/// </summary>
		/// <param name="value">The value against which to evaluate the conditional statement.</param>
		/// <returns>True if the conditions are satisfied; false otherwise.</returns>
		bool Test(object value);
	}
}
