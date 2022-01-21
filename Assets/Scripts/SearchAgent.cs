using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchAgent : Agent
{
    [SerializeField] Node initialNode;

	public Node targetNode { get; set; }

	private void Start()
	{
		targetNode = initialNode;
	}

	void Update()
    {
        if (targetNode != null)
		{
            movement.MoveTowards(targetNode.transform.position);
		}
    }
}
