using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathViewer : MonoBehaviour
{
	[Range(0, 500)] public int steps = 0;
	[SerializeField] GraphNodeSelector nodeSelector;

	public bool visible { get; set; } = true;

	int prevSteps;
	bool found = false;
	List<GraphNode> path = new List<GraphNode>();

	private void Start()
	{
		prevSteps = steps;
	}

	private void Update()
	{
		// build path
		steps = Mathf.Clamp(steps, 0, 500);
		if (steps != prevSteps)
		{
			BuildPath();
		}
		prevSteps = steps;

		if (visible)
		{
			// show node connections
			var nodes = Node.GetNodes<GraphNode>();
			nodes.ToList().ForEach(node => node.edges.ForEach(edge => Debug.DrawLine(edge.nodeA.transform.position, edge.nodeB.transform.position)));

			// reset graph nodes color
			nodes.ToList().ForEach(node => node.GetComponent<Renderer>().material.color = node.visited ? Color.black : Color.white);

			// set all path nodes color
			Color color = (found) ? Color.yellow : Color.magenta;
			path.ForEach(node => node.GetComponent<Renderer>().material.color = color);
		}
	}

	public void BuildPath()
	{
		// reset graph nodes
		GraphNode.ResetNodes();

		// build path
		found = Search.BuildPath(Search.BFS, nodeSelector.sourceNode, nodeSelector.destinationNode, ref path, steps);
	}

	public void ShowNodes()
	{
		visible = true;
		Node.ShowNodes<GraphNode>();
	}

	public void HideNodes()
	{
		visible = false;
		Node.HideNodes<GraphNode>();
	}
}
