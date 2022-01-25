using System.Collections;
using System.Collections.Generic;
//using System.Linq;
using UnityEngine;

public static class Search
{
	public delegate bool SearchAlgorithm(GraphNode source, GraphNode destination, ref List<GraphNode> path, int maxSteps);
	
	static public bool BuildPath(SearchAlgorithm searchAlgorithm, GraphNode source, GraphNode destination, ref List<GraphNode> path, int steps = int.MaxValue)
	{
		if (source == null || destination == null) return false;

		// reset graph nodes
		GraphNode.ResetNodes();

		// search for path from source to destination nodes		
		bool found = searchAlgorithm(source, destination, ref path, steps);

		return found;
	}

	public static bool DFS(GraphNode source, GraphNode destination, ref List<GraphNode> path, int maxSteps)
	{
		bool found = false;

		var nodes = new Stack<GraphNode>();

		nodes.Push(source);

		int steps = 0;
		while (!found && nodes.Count > 0 && steps++ < maxSteps)
		{
			var node = nodes.Peek();
			node.visited = true;

			bool forward = false;
			foreach(var edge in node.edges)
			{
				if (!edge.nodeB.visited)
				{
					nodes.Push(edge.nodeB);
					forward = true;

					if (edge.nodeB == destination)
					{
						found = true;
					}

					break;
				}
			}

			if (!forward)
			{
				nodes.Pop();
			}
		}

		path = new List<GraphNode>(nodes);
		path.Reverse();

		return found;
	}

	public static bool BFS(GraphNode source, GraphNode destination, ref List<GraphNode> path, int maxSteps)
	{
		bool found = false;

		


		return found;
	}
}
