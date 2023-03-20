using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class RotationConversion
{
    public static float TAU = 6.28f;
    
    

    public static float DegreesToRadians(float angleInDegrees)
    {
        return angleInDegrees * (TAU / 360);
    }

    public static float RadiansToDegrees(float angleInRadians)
    {
        return angleInRadians * (360 / TAU);
    }

    public static Vector2 AngToDir(float angleRad) => new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));

    public static float DirToAng(Vector2 v) => Mathf.Atan2(v.y, v.x);
    
    
}
