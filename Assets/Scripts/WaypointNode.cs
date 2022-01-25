using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNode : Node
{
	public WaypointNode[] nextWaypoints;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.TryGetComponent<SearchAgent>(out SearchAgent searchAgent))
		{
			if (searchAgent.targetNode == this)
			{
				searchAgent.targetNode = nextWaypoints[Random.Range(0, nextWaypoints.Length)];
			}
		}
	}
}
