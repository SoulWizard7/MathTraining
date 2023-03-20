using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotTest : MonoBehaviour
{
    [Range(0, 360)] public float angle;
    [Range(0, 10)] public float dist;

    public float angle1;
    public float angle2;
    
    
    private void OnDrawGizmos()
    {
        Vector2 pos = RotationConversion.AngToDir(RotationConversion.DegreesToRadians(angle));
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(pos * dist, 0.1f);
        Gizmos.color = Color.yellow;

        angle1 = RotationConversion.DirToAng(pos * dist);
        angle2 = RotationConversion.RadiansToDegrees(angle1);


    }
}
