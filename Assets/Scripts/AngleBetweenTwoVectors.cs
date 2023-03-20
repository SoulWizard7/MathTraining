using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleBetweenTwoVectors : MonoBehaviour
{
    // NOT WORKING
    
    
    public Transform A;
    public Transform B;

    public float angle;
    public float angle1;
    
    float CalculateAngleBetweenNormalizedVectors(Vector2 vectorA, Vector2 vectorB)
    {
        vectorA.Normalize();
        vectorB.Normalize();
        
        double radians = Vector2.Dot(vectorA, vectorB);

        return (float)Math.Acos(radians);
    }
    
    public static float CalculateAngleBetweenVectors(Vector2 A, Vector2 B)
    {
        float dotProduct = Vector2.Dot(A, B);
        float cosAngle = dotProduct / (A.magnitude * B.magnitude);
        float angleInRadians = (float)Math.Acos(cosAngle);
        float angleInDegrees = angleInRadians * (180 / (float)Math.PI);
        return angleInDegrees;
    }
    
    private void OnDrawGizmos()
    {
        Vector2 self = transform.position;
        
        Vector2 a = A.position - transform.position;
        Vector2 b = B.position - transform.position;

        angle = CalculateAngleBetweenNormalizedVectors(a, b);
        angle1 = CalculateAngleBetweenVectors(a, b);

        Gizmos.color = Color.red;
        
        a.Normalize();
        b.Normalize();
        
        Gizmos.DrawSphere(self + a , 0.1f);
        Gizmos.DrawSphere(self + b , 0.1f);
        Gizmos.DrawSphere(self , 0.1f);

        
        Gizmos.color = Color.green;
        
        Gizmos.DrawLine(self, a + self);
        Gizmos.DrawLine(self, b + self);
    }
}
