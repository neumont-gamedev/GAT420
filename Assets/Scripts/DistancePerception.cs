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
				// check if within angle
				Vector3 direction = (collider.transform.position - transform.position).normalized;
				float cos = Vector3.Dot(transform.forward, direction);
				float angle = Mathf.Acos(cos) * Mathf.Rad2Deg;
				// add collider.gameObject to result
				if (angle <= maxAngle)
				{
					result.Add(collider.gameObject);
				}
			}
		}

		return result.ToArray();
	}
}
