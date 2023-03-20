using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPlacer : MonoBehaviour
{
    public Transform turret;
    public Transform camera;

    public float curSlope;
    public float maxSlope;

    public Material green;
    public Material red;
    
    private void OnDrawGizmos()
    {
        if (turret == null || camera == null) return;
        
        Ray ray = new Ray(camera.transform.position, camera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            turret.GetChild(0).GetComponent<MeshRenderer>().material = green;
            
            curSlope = hit.normal.y;

            if (curSlope < maxSlope)
            {
                turret.GetChild(0).GetComponent<MeshRenderer>().material = red;
            }
            
            
            turret.position = hit.point;
            Vector3 yAxis = hit.normal;
            Vector3 xAxis = Vector3.Cross(yAxis, ray.direction).normalized;
            Vector3 zAxis = Vector3.Cross(camera.transform.right, yAxis).normalized;
            
            Gizmos.color = Color.red;
            Gizmos.DrawRay(hit.point, xAxis);
            Gizmos.color = Color.green;
            Gizmos.DrawRay(hit.point, yAxis);
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(hit.point, zAxis);
            
            turret.rotation = Quaternion.LookRotation(zAxis, yAxis);
        }
        else
        {
            turret.GetChild(0).GetComponent<MeshRenderer>().material = red;
        }
    }
}


