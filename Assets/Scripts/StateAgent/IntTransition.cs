using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntTransition : Transition
{
	IntRef parameter;
	int condition;
	Predicate predicate;

	public IntTransition(IntRef parameter, Predicate predicate, int condition)
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
				result = ((int)parameter == condition);
				break;
			case Predicate.LESS:
				result = ((int)parameter < condition);
				break;
			case Predicate.GREATER:
				result = ((int)parameter > condition);
				break;
			default:
				break;
		}

		return result;
	}
}
