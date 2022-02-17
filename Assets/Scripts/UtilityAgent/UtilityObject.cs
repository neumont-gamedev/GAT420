using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityObject : MonoBehaviour
{
    [System.Serializable]
    public class Effector
	{
        public Need.Type type;
        [Range(-1, 1)] public float change;
	}

    public Effector[] effectors;
    public Dictionary<Need.Type, float> registry = new Dictionary<Need.Type, float>();

    void Start()
    {
        foreach(var effector in effectors)
		{
            registry[effector.type] = effector.change;
		}
    }

    public float GetEffectorChange(Need.Type type)
	{
        registry.TryGetValue(type, out float change);

        return change;
	}

    public bool HasEffector(Need.Type type)
	{
        return registry.ContainsKey(type);
	}
}
