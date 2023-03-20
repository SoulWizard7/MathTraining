using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorMagnitude : MonoBehaviour
{
    public Transform vector2Transform;
    public Transform vector3Transform;

    public float aLength;
    public float aLength2;
    public float bLength;
    public float bLength2;

    
    //Just a little bit faster to type it out like this
    public static float MagnitudeVector2(Vector2 a)
    {
        return Mathf.Sqrt(a.x * a.x + a.y * a.y);
    }

    public static float MagnitudeVector3(Vector3 a)
    {
        float squaredLength = (a.x * a.x) + (a.y * a.y) + (a.z * a.z);
        return Mathf.Sqrt(squaredLength);
    }

    public static float MyDistance(Vector3 a, Vector3 b)
    {
        return MagnitudeVector3(a - b);
    }
    
    private void OnDrawGizmos()
    {
        Vector2 a = vector2Transform.position - transform.position;
        Vector3 b = vector3Transform.position - transform.position;

        aLength = MagnitudeVector2(a);
        aLength2 = a.magnitude;

        bLength = MagnitudeVector3(b);
        bLength2 = b.magnitude;


        Gizmos.DrawLine(transform.position, (Vector2)transform.position + a);
        Gizmos.DrawLine(transform.position, transform.position + b);
    }
}
