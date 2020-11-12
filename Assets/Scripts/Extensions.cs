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
     
    public static Vector3 FromScreenToWorldCoords(this Vector2 vec, Camera camera = null)
    {
        if (camera == null)
            camera = Camera.main;
        return camera.ScreenToWorldPoint(vec);
    }
}
