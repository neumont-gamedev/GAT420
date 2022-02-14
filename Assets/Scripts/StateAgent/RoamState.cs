using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamState : State
{
	public RoamState(StateAgent owner, string name) : base(owner, name)
	{
	}

	public override void OnEnter()
	{
		Quaternion rotation = Quaternion.AngleAxis(Random.Range(60, 90) + Mathf.Sign(Random.Range(-1f, 1f)), Vector3.up);
		Vector3 forward = rotation * owner.transform.forward;
		Vector3 destination = owner.transform.position + forward * Random.Range(10, 15);

		owner.movement.MoveTowards(destination);
		owner.movement.Resume();

		owner.atDestination.value = false;
	}

	public override void OnExit()
	{
	}

	public override void OnUpdate()
	{
		if (Vector3.Distance(owner.transform.position, owner.movement.destination) <= 1.5)
		{
			owner.atDestination.value = true;
		}
	}
}
