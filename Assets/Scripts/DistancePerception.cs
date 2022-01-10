using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistancePerception : Perception
{
	[SerializeField] float radius;
	[SerializeField] float maxAngle;

	public override GameObject[] GetGameObjects()
	{
		List<GameObject> result = new List<GameObject>();

		Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
		foreach (Collider collider in colliders)
		{
			if (tagName == "" || collider.CompareTag(tagName))
			{
				result.Add(collider.gameObject);
			}
		}

		return result.ToArray();
	}
}
