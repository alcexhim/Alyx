using System;
using System.Text;

namespace Alyx.Core.ConditionalExpressions
{
	public class Condition : IConditionalStatement
	{
		private string mvarPropertyName = String.Empty;
		/// <summary>
		/// The name of the property against which to test when the <see cref="Test" /> method is called
		/// passing in a property reference.
		/// </summary>
		public string PropertyName
		{
			get { return mvarPropertyName; }
			set { mvarPropertyName = value; }
		}
		
		private ConditionComparison mvarComparison = ConditionComparison.Equal;
		/// <summary>
		/// The type of comparison to use when testing this <see cref="Condition" />.
		/// </summary>
		public ConditionComparison Comparison
		{
			get { return mvarComparison; }
			set { mvarComparison = value; }
		}
		
		private object mvarValue = null;
		/// <summary>
		/// The value against which to test when the <see cref="Test" /> method is called.
		/// </summary>
		public object Value
		{
			get { return mvarValue; }
			set { mvarValue = value; }
		}

		private bool mvarNegate = false;
		/// <summary>
		/// True if the comparison should be negated; false otherwise.
		/// </summary>
		public bool Negate { get { return mvarNegate; } set { mvarNegate = value; } }

		
		/// <summary>
		/// Creates a <see cref="Condition" /> with the specified property name, comparison, and value.
		/// </summary>
		/// <param name="propertyName">The name of the property against which to test when the <see cref="Test" /> method is called passing in a property reference.</param>
		/// <param name="comparison">The type of comparison to use.</param>
		/// <param name="value">The value against which to test when the <see cref="Test" /> method is called.</param>
		public Condition(string propertyName, ConditionComparison comparison, object value)
		{
			mvarPropertyName = propertyName;
			mvarComparison = comparison;
			mvarValue = value;
		}

		/// <summary>
		/// Evaluates the conditional statement based on the given criteria.
		/// </summary>
		/// <param name="propertyValues">The set of values against which to evaluate the conditional statement.</param>
		/// <returns>True if the conditions are satisfied; false otherwise.</returns>
		public bool Test(params System.Collections.Generic.KeyValuePair<string, object>[] propertyValues)
		{
			bool retval = true;
			foreach (System.Collections.Generic.KeyValuePair<string, object> propertyValue in propertyValues)
			{
				if (propertyValue.Key == mvarPropertyName)
				{
					retval &= Test(propertyValue.Value);
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
			bool retval = true;
			foreach (System.Collections.Generic.KeyValuePair<string, object> propertyValue in propertyValues)
			{
				if (propertyValue.Key == mvarPropertyName)
				{
					retval &= Test(propertyValue.Value);
				}
			}
			return retval;
		}
		/// <summary>
		/// Evaluates the conditional statement based on the given criterion.
		/// </summary>
		/// <param name="value">The value against which to evaluate the conditional statement.</param>
		/// <returns>True if the conditions are satisfied; false otherwise.</returns>
		public bool Test (object propertyValue)
		{
			// would you like meatballs with your spaghetti code?
			bool returnValue = false;

			switch (mvarComparison)
			{
				case ConditionComparison.Equal:
				{
					if (propertyValue == null)
					{
						// our comparison object is null, so we can't .Equals it
						// just do regular == with the constant null in that case
						returnValue |= (mvarValue == null);
					}
					else
					{
						returnValue |= (propertyValue.Equals(mvarValue));
					}
					break;
				}
				case ConditionComparison.ReferenceEqual:
				{
					if (propertyValue == null)
					{
						// our comparison object is null, so we can't .Equals it
						// just do regular == with the constant null in that case
						returnValue |= (mvarValue == null);
					}
					else
					{
						returnValue |= (propertyValue == mvarValue);
					}
					break;
				}
				case ConditionComparison.GreaterThan:
				{
					if (propertyValue is IComparable)
					{
						if (propertyValue == null)
						{
							// can ANYTHING ever be greater than or less than null?
							returnValue |= false;
						}
						else
						{
							// we need to directly invoke IComparable.CompareTo here since we can't (usually)
							// do > or < on objects... not sure what to do if the object doesn't implement
							// IComparable though
							returnValue |= ((propertyValue as IComparable).CompareTo(mvarValue) > 0);
						}
					}
					break;
				}
				case ConditionComparison.LessThan:
				{
					if (propertyValue is IComparable)
					{
						if (propertyValue == null)
						{
							// can ANYTHING ever be greater than or less than null?
							returnValue |= false;
						}
						else
						{
							// we need to directly invoke IComparable.CompareTo here since we can't (usually)
							// do > or < on objects... not sure what to do if the object doesn't implement
							// IComparable though
							returnValue |= ((propertyValue as IComparable).CompareTo(mvarValue) < 0);
						}
					}
					break;
				}
				case ConditionComparison.Contains:
				{
					if (propertyValue is System.Collections.IEnumerable)
					{
						System.Collections.IEnumerable ie = (propertyValue as System.Collections.IEnumerable);
						foreach (object val in ie)
						{
							if (mvarValue.Equals(val.ToString()))
							{
								returnValue |= true;
								break;
							}
						}
					}
					else if (propertyValue is String)
					{
						returnValue |= (propertyValue as String).Contains(mvarValue.ToString());
					}
					else
					{
						// we need to directly invoke IComparable.CompareTo here since we can't (usually)
						// do > or < on objects... not sure what to do if the object doesn't implement
						// IComparable though
						returnValue |= ((propertyValue as IComparable).CompareTo(mvarValue) < 0);
					}
					break;
				}
				case ConditionComparison.StartsWith:
				{
					if (propertyValue is String)
					{
						returnValue |= (propertyValue as String).StartsWith(mvarValue.ToString());
					}
					break;
				}
				case ConditionComparison.EndsWith:
				{
					if (propertyValue is String)
					{
						returnValue |= (propertyValue as String).EndsWith(mvarValue.ToString());
					}
					break;
				}
			}

			if (mvarNegate)
			{
				// we have a Not in there, so negate our return value
				returnValue = !returnValue;
			}

			// did you have as much fun reading this as I did writing it?
			bool from_hell = returnValue;
			return from_hell;
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("( ");
			if (mvarNegate)
			{
				sb.Append("NOT ");
			}
			sb.Append(mvarPropertyName);
			switch (mvarComparison)
			{
				case ConditionComparison.Contains:
				{
					sb.Append(" CONTAINS ");
					break;
				}
				case ConditionComparison.EndsWith:
				{
					sb.Append(" ENDS-WITH ");
					break;
				}
				case ConditionComparison.Equal:
				{
					sb.Append(" EQUAL-TO ");
					break;
				}
				case ConditionComparison.GreaterThan:
				{
					sb.Append(" GREATER-THAN ");
					break;
				}
				case ConditionComparison.LessThan:
				{
					sb.Append(" LESS-THAN ");
					break;
				}
				case ConditionComparison.ReferenceEqual:
				{
					sb.Append(" REF-EQUAL-TO ");
					break;
				}
				case ConditionComparison.StartsWith:
				{
					sb.Append(" STARTS-WITH ");
					break;
				}
			}
			sb.Append("'");
			sb.Append(mvarValue);
			sb.Append("'");
			sb.Append(" )");
			return sb.ToString();
		}
	}
}
