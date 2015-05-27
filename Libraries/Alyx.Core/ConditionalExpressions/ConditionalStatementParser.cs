using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.ObjectModels.Markup;

namespace Alyx.Core.ConditionalExpressions
{
	public static class ConditionalStatementParser
	{
		public static IConditionalStatement Parse(MarkupTagElement tag)
		{
			if (tag == null) return null;

			switch (tag.FullName)
			{
				case "Condition":
				{
					MarkupAttribute attVariable = tag.Attributes["Variable"];
					if (attVariable == null) return null;

					ConditionComparison comparison = ConditionComparison.Equal;
					MarkupAttribute attComparison = tag.Attributes["Comparison"];
					if (attComparison != null)
					{
						switch (attComparison.Value.ToLower())
						{
							case "endswith": comparison = ConditionComparison.EndsWith; break;
							case "equal": comparison = ConditionComparison.Equal; break;
							case "greaterthan": comparison = ConditionComparison.GreaterThan; break;
							case "lessthan": comparison = ConditionComparison.LessThan; break;
							case "referenceequal": comparison = ConditionComparison.ReferenceEqual; break;
							case "startswith": comparison = ConditionComparison.StartsWith; break;
							case "contains": comparison = ConditionComparison.Contains; break;
						}
					}
					string value = null;
					MarkupAttribute attValue = tag.Attributes["Value"];
					if (attValue != null) value = attValue.Value;

					Condition condition = new Condition(attVariable.Value, comparison, value);

					MarkupAttribute attNegate = tag.Attributes["Negate"];
					if (attNegate != null)
					{
						condition.Negate = (attNegate.Value.ToLower() == "true");
					}
					return condition;
				}
				case "ConditionGroup":
				{
					ConditionGroup group = new ConditionGroup();

					MarkupAttribute attCombination = tag.Attributes["Combination"];
					if (attCombination != null)
					{
						switch (attCombination.Value.ToLower())
						{
							case "and": group.Combination = ConditionCombination.And; break;
							case "or": group.Combination = ConditionCombination.Or; break;
							case "xor": group.Combination = ConditionCombination.Xor; break;
						}
					}

					foreach (MarkupElement el1 in tag.Elements)
					{
						MarkupTagElement tag1 = (el1 as MarkupTagElement);
						IConditionalStatement statement = Parse(tag1);
						if (statement != null) group.Conditions.Add(statement);
					}
					return group;
				}
			}
			return null;
		}
	}
}
