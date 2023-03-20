using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PythagorennTheorem : MonoBehaviour
{
    //Must have 90 degree angle
    public Transform A;
    public Transform B;
    public double DistAtoB;

    double Pythagorenn(Transform A, Transform B)
    {
        Vector2 a = A.transform.position - transform.position;
        Vector2 b = B.transform.position - transform.position;

        float aLength = a.magnitude;
        float bLength = b.magnitude;

        return Math.Sqrt((aLength * aLength) + (bLength * bLength));
    }

    private void OnDrawGizmos()
    {
        DistAtoB = Pythagorenn(A, B);
    }
}
