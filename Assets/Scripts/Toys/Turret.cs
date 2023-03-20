using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public enum Shape
    {
        Cone,
        Sphere,
        Wedge
    }

    public Shape shape;
    
    
    public Transform target;
    public float maxRadius;
    public float minRadius;
    [Range(0, 360)]public float fovDegrees = 45;
    public float maxHeight;
    public float minHeight;
    private float FovRad => fovDegrees * Mathf.Deg2Rad;
    private float AngThresh => Mathf.Cos(FovRad / 2);

    public float rangeToTarget3D;
    public float rangeToTarget2D;
    private void OnDrawGizmos()
    {
        if (target == null) return;
        
        Gizmos.matrix = Handles.matrix = transform.localToWorldMatrix;

        if (Contains(target.position))
        {
            Handles.color = Color.red;
            Gizmos.color = Color.red;
        }
        else
        {
            Handles.color = Color.white;
            Gizmos.color = Color.white;
        }

        switch (shape)
        {
            case Shape.Wedge : DrawWedgeGizmo();
                break;
            case Shape.Sphere : DrawSphereGizmo();
                break;
            case Shape.Cone : DrawConeGizmo();
                break;
        }
    }

    public bool Contains(Vector3 targetPos)
    {
        return shape switch
        {
            Shape.Wedge => WedgeContains(targetPos),
            Shape.Sphere => SphereContains(targetPos),
            Shape.Cone => ConeContains(targetPos),
            _           => throw new NotImplementedException()
        };
    }

    void DrawConeGizmo()
    {
        Vector3 top = new Vector3(0, maxHeight, 0);
        
        float p = AngThresh;
        float x = Mathf.Sqrt(1 - p * p);
        
        Vector3 vRight = new Vector3(x, 0, p);
        Vector3 vLeft = new Vector3(-x, 0, p);
        
        Vector3 vUp = new Vector3(0, x, p);
        Vector3 vDown = new Vector3(0, -x, p);

        Vector3 vRightMaxRadius = vRight * maxRadius;
        Vector3 vLeftMaxRadius = vLeft * maxRadius;
        
        Vector3 vRightMinRadius = vRight * minRadius;
        Vector3 vLeftMinRadius = vLeft * minRadius;

        Vector3 vUpMinRadius = vUp * minRadius;
        Vector3 vUpMaxRadius = vUp * maxRadius;
        
        Vector3 vDownMinRadius = vDown * minRadius;
        Vector3 vDownMaxRadius = vDown * maxRadius;
        
        Handles.DrawWireArc(top, Vector3.up, vLeftMaxRadius, fovDegrees, maxRadius);
        Handles.DrawWireArc(top, Vector3.right, vUpMaxRadius, fovDegrees, maxRadius);
        
        Handles.DrawWireArc(top, Vector3.up, vLeftMinRadius, fovDegrees, minRadius);
        Handles.DrawWireArc(top, Vector3.right, vUpMinRadius, fovDegrees, minRadius);

        //Handles.DrawWireArc(top, Vector3.forward, vLeftMaxRadius, 360, maxRadius); //Dif way of drawing rings
        //Handles.DrawWireArc(top, Vector3.forward, vLeftMinRadius, 360, minRadius); 
        DrawRing(minRadius, top);
        DrawRing(maxRadius, top);
        
        Gizmos.DrawLine(top + vLeftMinRadius, top + vLeftMaxRadius);
        Gizmos.DrawLine(top + vRightMinRadius, top + vRightMaxRadius);
        Gizmos.DrawLine(top + vUpMinRadius, top + vUpMaxRadius);
        Gizmos.DrawLine(top + vDownMinRadius, top + vDownMaxRadius);

    }

    void DrawRing(float turretRadius, Vector3 height)
    {
        float angle = FovRad / 2;
        float dist = turretRadius * Mathf.Cos(angle);
        float r = turretRadius * Mathf.Sin(angle);

        Handles.DrawWireDisc(new Vector3(0, 0, dist) + height, Vector3.forward, r);
    }

    bool ConeContains(Vector3 targetPos)
    {
        targetPos.y -= maxHeight;
        if (SphereContains(targetPos) == false) return false;

        Vector3 dirToTarget = ((targetPos) - transform.position).normalized;
        float projAngle = Vector3.Dot(transform.forward, dirToTarget);
   
        return projAngle > AngThresh;
    }

    void DrawSphereGizmo()
    {
        Gizmos.DrawWireSphere(default, minRadius);
        Gizmos.DrawWireSphere(default, maxRadius);
    }

    void DrawWedgeGizmo()
    {
        Vector3 top = new Vector3(0, maxHeight, 0);
        Vector3 bottom = new Vector3(0, -minHeight, 0);

        float p = AngThresh;
        float x = Mathf.Sqrt(1 - p * p);
        
        Vector3 vRight = new Vector3(x, 0, p);
        Vector3 vLeft = new Vector3(-x, 0, p);

        Vector3 vRightMaxRadius = vRight * maxRadius;
        Vector3 vLeftMaxRadius = vLeft * maxRadius;
        
        Vector3 vRightMinRadius = vRight * minRadius;
        Vector3 vLeftMinRadius = vLeft * minRadius;
        
        Handles.DrawWireArc(bottom, Vector3.up, vLeftMaxRadius, fovDegrees, maxRadius);
        Handles.DrawWireArc(top, Vector3.up, vLeftMaxRadius, fovDegrees, maxRadius);
        
        Handles.DrawWireArc(bottom, Vector3.up, vLeftMinRadius, fovDegrees, minRadius);
        Handles.DrawWireArc(top, Vector3.up, vLeftMinRadius, fovDegrees, minRadius);

        Vector3 a = default;
        
        Gizmos.DrawLine(vRightMinRadius + top, vRightMaxRadius + top);
        Gizmos.DrawLine(vRightMinRadius + bottom, vRightMaxRadius + bottom);
        Gizmos.DrawLine(vLeftMinRadius + top, vLeftMaxRadius + top);
        Gizmos.DrawLine(vLeftMinRadius + bottom, vLeftMaxRadius + bottom);
        Gizmos.DrawRay(bottom + vLeftMaxRadius, Vector3.up * (maxHeight + minHeight));
        Gizmos.DrawRay(bottom + vRightMaxRadius, Vector3.up * (maxHeight + minHeight));
        Gizmos.DrawRay(bottom + vLeftMinRadius, Vector3.up * (maxHeight + minHeight));
        Gizmos.DrawRay(bottom + vRightMinRadius, Vector3.up * (maxHeight + minHeight));
    }

    public bool SphereContains(Vector3 targetPos)
    {
        float dist = Vector3.Distance(transform.position, targetPos);
        if (dist < maxRadius && dist > minRadius) return true;
        return false;
    }

    public bool WedgeContains(Vector3 targetPos)
    {
        Vector3 vecToTargetWorld = (targetPos - transform.position);
        
        // world to localspace
        Vector3 vecToTargetLocal = transform.InverseTransformVector(vecToTargetWorld);
        rangeToTarget3D = Mathf.Sqrt((vecToTargetLocal.x * vecToTargetLocal.x) + (vecToTargetLocal.y * vecToTargetLocal.y) + (vecToTargetLocal.z * vecToTargetLocal.z));

        // height position check
        if (vecToTargetLocal.y < -minHeight || vecToTargetLocal.y > maxHeight)
        {
            return false; // outside height range
        }

        // angular check
        Vector3 flatDirToTarget = vecToTargetLocal;
        flatDirToTarget.y = 0;
        rangeToTarget2D = flatDirToTarget.magnitude;
        flatDirToTarget /= rangeToTarget2D; // normalizes flatDirToTarget
        flatDirToTarget.Normalize();
        if (flatDirToTarget.z < AngThresh)
        {
            return false;
        }

        if (rangeToTarget2D > maxRadius || rangeToTarget2D < minRadius)
        {
            return false; // outside radius range
        }
        return true;
    }
}
