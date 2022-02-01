using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathBuilder : MonoBehaviour
{
	delegate bool SearchAlgorithm(GraphNode source, GraphNode destination, out List<GraphNode> path, int maxSteps);
	static SearchAlgorithm searchAlgorithm;

	static public void BuildPath(GraphNode source, GraphNode destination, List<GraphNode> path, int steps = int.MaxValue)
	{
		if (source == null || destination == null) return;

		// search for path from source to destination nodes		
		bool found = searchAlgorithm(source, destination, out path, steps);
	}
}
