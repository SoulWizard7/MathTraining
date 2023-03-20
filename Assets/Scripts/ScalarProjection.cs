using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalarProjection : MonoBehaviour
{
    public Transform ANormal;

    public Transform B;

    public float scalarProjection;
    public Vector2 vectorProjection;
    
    float SProjection(Vector2 normal, Vector2 vectorB)
    {
        return Vector2.Dot(normal, vectorB);
    }
    private void OnDrawGizmos()
    {
        Vector2 self = transform.position;
        Vector2 aNormal = ANormal.position - transform.position;
        Vector2 b = B.position - transform.position;
        Gizmos.DrawLine(transform.position, aNormal + self);

        aNormal.Normalize();
        scalarProjection = SProjection(aNormal, b);
        vectorProjection = aNormal * scalarProjection;

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(aNormal + self, 0.1f);
        Gizmos.DrawLine(transform.position, aNormal + self);
        Gizmos.DrawLine(transform.position, b + self);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(vectorProjection + self, b + self);
    }
}
