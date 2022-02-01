using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Perception : MonoBehaviour
{
	public string tagName;

	public abstract GameObject[] GetGameObjects();
}
