using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

		// create queue of graph nodes
		var nodes = new Queue<GraphNode>();

		// set source node visited to true
		source.visited = true;
		// enqueue source node
		nodes.Enqueue(source);

		// set the current number of steps
		int steps = 0;
		while (!found && nodes.Count > 0 && steps++ < maxSteps)
		{
			// dequeue node
			var node = nodes.Dequeue();
			// go through edges of node
			foreach (var edge in node.edges)
			{
				// if nodeB is not visited
				if (edge.nodeB.visited == false)
				{
					// set nodeB visited to true
					edge.nodeB.visited = true;
					// set nodeB parent to node
					edge.nodeB.parent = node;
					// enqueue nodeB
					nodes.Enqueue(edge.nodeB);
				}
				// check if nodeB is the destination node
				if (edge.nodeB == destination)
				{
					// set found to true
					found = true;
					break;
				}
			}
		}

		// create a list of graph nodes (path)
		path = new List<GraphNode>();
		// if found is true
		if (found)
		{
			// set node to destination
			var node = destination;
			// while node not null
			while (node != null)
			{
				// add node to list path
				path.Add(node);
				// set node to node parent
				node = node.parent;
			}

			// reverse path
			path.Reverse();
		}
		else
		{
			// did not find destination, convert nodes queue to path
			path = nodes.ToList();
		}


		return found;
	}
}
