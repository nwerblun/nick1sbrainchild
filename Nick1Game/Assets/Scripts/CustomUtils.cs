using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomUtils
{
    public static Vector2 ClampMagnitude(Vector2 v, float max, float min)
    {
        double sm = v.sqrMagnitude;
        if (sm > (double)max * (double)max) return v.normalized * max;
        else if (sm < (double)min * (double)min) return v.normalized * min;
        return v;
    }
}
