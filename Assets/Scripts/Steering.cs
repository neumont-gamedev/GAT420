using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steering : MonoBehaviour
{
	[SerializeField] float wanderDistance = 1;
	[SerializeField] float wanderRadius = 3;
	[SerializeField] float wanderDisplacement = 5;

	float wanderAngle = 0;

	public Vector3 Seek(AutonomousAgent agent, GameObject target)
	{
		Vector3 force = CalculateSteering(agent, target.transform.position - agent.transform.position);

		return force;
	}

	public Vector3 Flee(AutonomousAgent agent, GameObject target)
	{
		Vector3 force = CalculateSteering(agent, agent.transform.position - target.transform.position);

		return force;
	}

	public Vector3 Wander(AutonomousAgent agent)
	{
		// randomly adjust angle +/- displacement
		wanderAngle = wanderAngle + Random.Range(-wanderDisplacement, wanderDisplacement);
		// create rotation quaternion around y-axis (up)
		Quaternion rotation = Quaternion.AngleAxis(wanderAngle, Vector3.up);
		// calculate point on circle radius
		Vector3 point = rotation * (Vector3.forward * wanderRadius);

		// set point in front of agent at distance length
		Vector3 forward = agent.transform.forward * wanderDistance;

		Vector3 force = CalculateSteering(agent, forward + point);

		return force;
	}

	public Vector3 Cohesion(AutonomousAgent agent, GameObject[] targets)
	{
		Vector3 centerOfTargets = Vector3.zero;
		foreach (GameObject target in targets)
		{
			centerOfTargets += target.transform.position;
		}

		centerOfTargets /= targets.Length;

		Vector3 force = CalculateSteering(agent, centerOfTargets - agent.transform.position);

		return force;
	}

	public Vector3 Separation(AutonomousAgent agent, GameObject[] targets, float radius)
	{
		Vector3 separation = Vector3.zero;
		foreach (GameObject target in targets)
		{
			Vector3 direction = (agent.transform.position - target.transform.position);
			if (direction.magnitude < radius)
			{
				separation += direction / direction.sqrMagnitude;
			}
		}

		Vector3 force = CalculateSteering(agent, separation);

		return force;
	}

	public Vector3 Alignment(AutonomousAgent agent, GameObject[] targets)
	{
		if (targets.Length == 0) return Vector3.zero;

		Vector3 averageVelocity = Vector3.zero;
		foreach (GameObject target in targets)
		{
			averageVelocity += target.GetComponent<AutonomousAgent>().velocity;
		}
		averageVelocity /= targets.Length;

		Vector3 force = CalculateSteering(agent, averageVelocity);

		return force;
	}

	public Vector3 CalculateSteering(AutonomousAgent agent, Vector3 vector)
	{
		Vector3 direction = vector.normalized;
		Vector3 desired = direction * agent.maxSpeed;
		Vector3 steer = desired - agent.velocity;
		Vector3 force = Vector3.ClampMagnitude(steer, agent.maxForce);

		return force;
	}
}
