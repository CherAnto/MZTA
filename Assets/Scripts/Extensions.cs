using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static Color ChangeA(this Color color, float newA)
    {
        //color =  new Color(0.5f, 0.5f, 0.5f, newA);
        color =  new Color(color.r, color.g, color.b, newA);
        return color;
    }
}
