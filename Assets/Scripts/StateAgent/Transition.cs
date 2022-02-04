using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Transition
{
	public enum Predicate
	{
		EQUAL,
		LESS,
		GREATER
	}

	public abstract bool ToTransition();
}
