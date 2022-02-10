using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Transition
{
	public enum Predicate
	{
		EQUAL,
		LESS,
		LESS_EQUAL,
		GREATER
	}

	public abstract bool ToTransition();
}
