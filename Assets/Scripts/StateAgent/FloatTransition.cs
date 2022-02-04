using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatTransition : Transition
{
	FloatRef parameter;
	float condition;
	Predicate predicate;

	public FloatTransition(FloatRef parameter, Predicate predicate, float condition)
	{
		this.parameter = parameter;
		this.predicate = predicate;
		this.condition = condition;
	}

	public override bool ToTransition()
	{
		bool result = false;

		switch (predicate)
		{
			case Predicate.EQUAL:
				result = (parameter == condition);
				break;
			case Predicate.LESS:
				result = (parameter < condition);
				break;
			case Predicate.GREATER:
				result = (parameter > condition);
				break;
			default:
				break;
		}

		return result;
	}
}
