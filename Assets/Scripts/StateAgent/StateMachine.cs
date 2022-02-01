using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
	Dictionary<State, List<KeyValuePair<Transition, State>>> stateTransitions = new Dictionary<State, List<KeyValuePair<Transition, State>>>();

	State currentState;

	public void Update()
	{
		if (currentState == null) return;

		// check state transitions
		var transitions = stateTransitions[currentState];
		foreach (var transition in transitions)
		{
			if (transition.Key.ToTransition())
			{
				SetState(transition.Value);
				break;
			}
		}

		// update state
		currentState.OnUpdate();
	}

	public void SetState(State newState)
	{
		currentState.OnExit();
		currentState = newState;
		newState.OnEnter();
	}
}
