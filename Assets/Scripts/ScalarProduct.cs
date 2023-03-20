using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalarProduct : MonoBehaviour
{
    // Usecase unknown
    
    public Transform A;
    public Transform B;

    public float DotProduct;
    
    float SProduct(Vector2 vectorA, Vector2 vectorB)
    {
        return Vector2.Dot(vectorA, vectorB);
    }
    
    private void OnDrawGizmos()
    {
        Vector2 self = transform.position;
        
        Vector2 a = A.position - transform.position;
        Vector2 b = B.position - transform.position;
        
        
        
        DotProduct = SProduct(a, b);

        Gizmos.color = Color.red;
        
        
        //Gizmos.DrawLine(transform.position, a + self);
        //Gizmos.DrawLine(transform.position, b + self);


        a.Normalize();
        
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, a * DotProduct );

        Gizmos.DrawSphere(self + a * DotProduct, 0.1f);
        
        
    }
}
