using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotator : MonoBehaviour
{
    public Turret turret;
    public Transform gunTransform;
    public Transform target;
    public float lerpSpeed;

    private Vector3 vecToTarget;

    private void Update()
    {
        if (turret.WedgeContains(target.position))
        {
            vecToTarget = target.position - gunTransform.position;
        }
        
        Quaternion curRot = gunTransform.rotation;
        Quaternion targetRot = Quaternion.LookRotation(vecToTarget, transform.up);
        
        

        gunTransform.rotation = Quaternion.Slerp(curRot, targetRot, Time.deltaTime * lerpSpeed);
        
        
    }
/*
    private void OnDrawGizmos()
    {
        if (turret.IsTargetInside(target.position))
        {
            Vector3 vecToTarget = target.position - gunTransform.position;
            gunTransform.rotation = Quaternion.LookRotation(vecToTarget, transform.up);
        }
    }
    */
}
