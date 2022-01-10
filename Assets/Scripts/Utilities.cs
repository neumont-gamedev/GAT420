using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    public static Vector3 Wrap(Vector3 v, Vector3 min, Vector3 max)
    {
        Vector3 result = v;

        if (v.x > max.x) result.x = min.x + (max.x - v.x);
        else if (v.x < min.x) result.x = max.x - (min.x - v.x);

        if (v.y > max.y) result.y = min.y + (max.y - result.y);
        else if (v.y < min.y) result.y = max.y - (min.y - result.y);

        if (v.z > max.z) result.z = min.z + (max.z - result.z);
        else if (v.z < min.z) result.z = max.z - (min.z - result.z);

        return result;
    }
}
