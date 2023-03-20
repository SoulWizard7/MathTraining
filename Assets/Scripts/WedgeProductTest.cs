using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WedgeProductTest : MonoBehaviour
{
    public bool useList;
    public Transform A;
    public Transform B;
    public Transform C;
    public Transform Target;

    public List<Transform> PointList = new List<Transform>();
    

    private void OnDrawGizmos()
    {
        Vector2 t = Target.position;
        if (!useList)
        {
            Vector2 a = A.position;
            Vector2 b = B.position;
            Vector2 c = C.position;

            Gizmos.DrawLine(a,b);
            Gizmos.DrawLine(c,b);
            Gizmos.DrawLine(a,c);
            Gizmos.color = TriangleContains(a,b,c,t) ? Color.red : Color.green;
            Gizmos.DrawSphere(Target.position, 0.1f);
        }
        else
        {
            int amount = PointList.Count;
            Vector2[] v2List = new Vector2[amount];
            for (int i = 0; i < amount; i++)
            {
                v2List[i] = (PointList[i].position);
            }
            Gizmos.DrawLine(v2List[amount-1], v2List[0]);
            for (int i = 1; i < amount; i++)
            {
                Gizmos.DrawLine(v2List[i-1], v2List[i]);
            }
            //bool[] bools = new bool[amount];
//
            //bools[amount - 1] = GetSideSign(v2List[amount - 1], v2List[0], t);
            
            bool first = GetSideSign(v2List[amount - 1], v2List[0], t);

            Gizmos.color = ConvexContains(v2List, first, amount, t) ? Color.red : Color.green;
            
            Gizmos.DrawSphere(Target.position, 0.1f);
        }
    }

    public bool ConvexContains(Vector2[] v2List, bool first, int amount, Vector2 t)
    {
        for (int i = 1; i < amount-1; i++)
        {
            if (first != GetSideSign(v2List[i - 1], v2List[i], t))
            {
                return false;
            }
        }

        return true;
    }

    public float Wedge(Vector2 a, Vector2 b) => a.x * b.y - a.y * b.x;

    bool GetSideSign(Vector2 a, Vector2 b, Vector2 target)
    {
        Vector2 sideVec = b - a;
        Vector2 ptRel = target - a;
        return Wedge(sideVec, ptRel) > 0;
    }

    public bool TriangleContains(Vector2 a, Vector2 b, Vector2 c, Vector2 target)
    {
        bool ab = GetSideSign(a, b, target);
        bool bc = GetSideSign(b, c, target);
        bool ca = GetSideSign(c, a, target);
        return ab == bc && bc == ca;
    }
    
    
}
