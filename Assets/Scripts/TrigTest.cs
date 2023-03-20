using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TrigTest : MonoBehaviour
{
    // Trigonometry test

    [Range(0, 360)] public float angleDeg;
    
    public static Vector2 AngRadToDir(float angleRad) => new (Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    public static Vector2 AngDegToDir(float angleDeg) =>  new (Mathf.Cos(angleDeg * Mathf.Deg2Rad), Mathf.Sin(angleDeg * Mathf.Deg2Rad));

    private void OnDrawGizmos()
    {
        Handles.DrawWireDisc(Vector3.zero, Vector3.forward, 1);

        float angleRad = angleDeg * Mathf.Deg2Rad;

        Vector2 v = AngDegToDir(angleDeg);
        
        Gizmos.DrawRay(default, v);
    }
}
