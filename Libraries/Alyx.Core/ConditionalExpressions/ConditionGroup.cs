using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alyx.Core.ConditionalExpressions
{
	/// <summary>
	/// A group of <see cref="IConditionalStatement" />s joined by a <see cref="ConditionalCombination" />.
	/// </summary>
	public class ConditionGroup : IConditionalStatement
	{
		/// <summary>
		/// Creates a new <see cref="ConditionGroup" /> with no conditional statements specified and a
		/// default <see cref="ConditionalCombination" /> of <see cref="ConditionalCombination.And" />.
		/// </summary>
		public ConditionGroup()
		{
			// I know it's initialized to this but I'm doing it here for clarity's sake (and because
			// it's documented here... if you change it, make sure to update the documentation! don't
			// rely on the field initializer)
			mvarCombination = ConditionCombination.And;
		}
		/// <summary>
		/// Creates a new <see cref="ConditionGroup" /> with the specified
		/// <see cref="ConditionCombination" /> and <see cref="IConditionalStatement" />s.
		/// </summary>
		/// <param name="combination">The <see cref="ConditionCombination" /> used to join <see cref="IConditionalStatement" />s when testing this <see cref="ConditionGroup" />.</param>
		/// <param name="statements">The <see cref="Condition" />s and <see cref="ConditionGroup" />s that are part of this <see cref="ConditionGroup" />.</param>
		public ConditionGroup(ConditionCombination combination, params IConditionalStatement[] statements)
		{
			mvarCombination = combination;
			for (int i = 0; i < statements.Length; i++)
			{
				mvarConditions.Add(statements[i]);
			}
		}

		private ConditionalStatementCollection mvarConditions = new ConditionalStatementCollection();
		/// <summary>
		/// Gets all <see cref="IConditionalStatement" />s in this <see cref="ConditionGroup" />.
		/// </summary>
		public ConditionalStatementCollection Conditions
		{
			get { return mvarConditions; }
		}

		private ConditionCombination mvarCombination = ConditionCombination.And;
		/// <summary>
		/// The type of combination used to join the <see cref="Condition" />s in this
		/// <see cref="ConditionGroup" />.
		/// </summary>
		public ConditionCombination Combination
		{
			get { return mvarCombination; }
			set { mvarCombination = value; }
		}

		/// <summary>
		/// Evaluates the conditional statement based on the given criteria.
		/// </summary>
		/// <param name="propertyValues">The set of values against which to evaluate the conditional statement.</param>
		/// <returns>True if the conditions are satisfied; false otherwise.</returns>
		public bool Test(params System.Collections.Generic.KeyValuePair<string, object>[] propertyValues)
		{
			bool retval = false;
			if (mvarCombination == ConditionCombination.And)
			{
				retval = true;
			}
			for (int i = 0; i < mvarConditions.Count; i++)
			{
				switch (mvarCombination)
				{
					case ConditionCombination.And:
						retval &= mvarConditions[i].Test(propertyValues);
						break;
					case ConditionCombination.Or:
						retval |= mvarConditions[i].Test(propertyValues);
						break;
					case ConditionCombination.Xor:
						retval ^= mvarConditions[i].Test(propertyValues);
						break;
				}
			}
			return retval;
		}
		/// <summary>
		/// Evaluates the conditional statement based on the given criteria.
		/// </summary>
		/// <param name="propertyValues">The set of values against which to evaluate the conditional statement.</param>
		/// <returns>True if the conditions are satisfied; false otherwise.</returns>
		public bool Test(System.Collections.Generic.Dictionary<string, object> propertyValues)
		{
			bool retval = false;
			if (mvarCombination == ConditionCombination.And)
			{
				retval = true;
			}
			for (int i = 0; i < mvarConditions.Count; i++)
			{
				switch (mvarCombination)
				{
					case ConditionCombination.And:
						retval &= mvarConditions[i].Test(propertyValues);
						break;
					case ConditionCombination.Or:
						retval |= mvarConditions[i].Test(propertyValues);
						break;
					case ConditionCombination.Xor:
						retval ^= mvarConditions[i].Test(propertyValues);
						break;
				}
			}
			return retval;
		}

		/// <summary>
		/// Evaluates the conditional statement based on the given criterion.
		/// </summary>
		/// <param name="value">The value against which to evaluate the conditional statement.</param>
		/// <returns>True if the conditions are satisfied; false otherwise.</returns>
		public bool Test(object value)
		{
			bool retval = true;

			for (int i = 0; i < mvarConditions.Count; i++)
			{
				switch (mvarCombination)
				{
					case ConditionCombination.And:
						retval &= mvarConditions[i].Test(value);
						break;
					case ConditionCombination.Or:
						retval |= mvarConditions[i].Test(value);
						break;
					case ConditionCombination.Xor:
						retval ^= mvarConditions[i].Test(value);
						break;
				}
			}
			return retval;
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("( ");
			for (int i = 0; i < mvarConditions.Count; i++)
			{
				sb.Append(mvarConditions[i].ToString());
				if (i < mvarConditions.Count - 1)
				{ 
					switch (mvarCombination)
					{
						case ConditionCombination.And:
						{
							sb.Append(" AND ");
							break;
						}
						case ConditionCombination.Or:
						{
							sb.Append(" OR ");
							break;
						}
						case ConditionCombination.Xor:
						{
							sb.Append(" XOR ");
							break;
						}
					}
				}
			}
			sb.Append(" )");
			return sb.ToString();
		}
	}
}
