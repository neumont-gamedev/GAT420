using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : State
{
	public DeathState(StateAgent owner, string name) : base(owner, name)
	{
	}

	public override void OnEnter()
	{
		owner.movement.Stop();
		owner.animator.SetTrigger("death");
	}

	public override void OnExit()
	{
		
	}

	public override void OnUpdate()
	{
		
	}
}
