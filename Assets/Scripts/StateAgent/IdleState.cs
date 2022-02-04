using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
	float timer;

	public IdleState(StateAgent owner, string name) : base(owner, name)
	{
	}

	public override void OnEnter()
	{
		Debug.Log(name + " enter");

		timer = 2;
	}

	public override void OnExit()
	{
		Debug.Log(name + " exit");
	}

	public override void OnUpdate()
	{
		Debug.Log(name + " update");

		timer -= Time.deltaTime;
		if (timer <= 0)
		{
			owner.stateMachine.SetState(owner.stateMachine.StateFromName("patrol"));
		}
	}
}
