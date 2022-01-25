using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(PathViewer))]
public class PathViewerEditor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		PathViewer viewer = (PathViewer)target;

		if (GUILayout.Button("Build Path")) viewer.BuildPath();
		if (GUILayout.Button(">>")) viewer.steps++;
		if (GUILayout.Button("<<")) viewer.steps--;
		if (GUILayout.Button("Show")) viewer.ShowNodes();
		if (GUILayout.Button("Hide")) viewer.HideNodes();
	}
}
