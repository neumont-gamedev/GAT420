using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentPath : MonoBehaviour
{
    public PathNodes pathNodes;
    public Node targetNode { get; set; }
	public bool complete { get => targetNode == null; }

	public void Move(Movement movement)
	{
		if (targetNode != null)
		{
			movement.MoveTowards(targetNode.transform.position);
		}
	}
}
