using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Need : MonoBehaviour
{
    public enum Type
	{
        ENERGY,
        HUNGER,
        BLADDER
	}

    public Type type;
    public AnimationCurve curve;
    public float input = 1;
    public float decay = 0;

    public float value { get { return curve.Evaluate(input); } }

    void Update()
    {
        input = input - (decay * Time.deltaTime);
        input = Mathf.Clamp(input, -1, 1);
    }

}
