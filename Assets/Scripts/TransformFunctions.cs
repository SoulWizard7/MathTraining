using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TransformFunctions : MonoBehaviour
{
    public Vector3 SphereLocalPos;
    public Vector3 worldCoord;
    public Vector3 localCoord;
    
    public static Vector3 LocalToWorld(Transform origin, Vector3 local)
    {
        return origin.TransformPoint(local);
        
        Vector3 position = origin.position;
        position += local.x * origin.right;
        position += local.y * origin.up;
        position += local.z * origin.forward;
        return position;
    }
    
    public static Vector3 WorldToLocal(Transform origin, Vector3 world)
    {
        return origin.InverseTransformPoint(world);
        
        Vector3 rel = world - origin.position;
        float x = Vector3.Dot(rel, Vector3.right);
        float y = Vector3.Dot(rel, Vector3.up);
        float z = Vector3.Dot(rel, Vector3.forward);
        return new(x, y, z);
    }
    
    Vector2 WorldToLocalV2( Vector2 world)
    {
        Vector2 rel = world - (Vector2)transform.position;
        float x = Vector2.Dot(rel, transform.right);
        float y = Vector2.Dot(rel, transform.up);
        return new(x, y);
    }
    
    private void OnDrawGizmos()
    {
        localCoord = WorldToLocal(transform, worldCoord);
        Vector3 LocalPos = LocalToWorld(transform, SphereLocalPos);
        
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, .5f);
        
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(worldCoord, .5f);
        
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(LocalPos, .5f);
    }
}
