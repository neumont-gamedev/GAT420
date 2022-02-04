using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoolTransition : Transition
{
	BoolRef parameter;
	bool condition;

	public BoolTransition(BoolRef parameter, bool condition)
	{
		this.parameter = parameter;
		this.condition = condition;
	}

	public override bool ToTransition()
	{
		return (parameter == condition);
	}
}
