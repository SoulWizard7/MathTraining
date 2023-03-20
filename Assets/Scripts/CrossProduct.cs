using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossProduct : MonoBehaviour
{
    public Transform A;
    public Transform B;

    public bool flip;
    public bool normalize;


    Vector3 CrossProd(Vector3 a, Vector3 b, bool flip, bool normalize)
    {
        if (normalize)
        {
            a.Normalize();
            b.Normalize();
        }
        
        if (!flip)
        {
            return Vector3.Cross(a, b);
        }
        else
        {
            return Vector3.Cross(b, a);
        }
    }
    
    
    private void OnDrawGizmos()
    {
        Vector3 self = transform.position;
        
        Vector3 a = A.position - self;
        Vector3 b = B.position - self;

        Vector3 c = CrossProd(a, b, flip, normalize);
        
        
        if (normalize)
        {
            a.Normalize();
            b.Normalize();
        }

        Gizmos.color = Color.green;
        Gizmos.DrawLine(self, a + self);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(self, b + self);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(self, c + self);
        Gizmos.DrawSphere(c + self, .05f);
    }
}
