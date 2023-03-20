using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



public class RotateCube : MonoBehaviour
{
    
    
    private bool flip;
    public bool rotate;
    
    public bool invert;
    public float rotation = 1;
    public float rotSpeed = 5;

    public bool rotate2;
    public bool rotate3;
    public bool rotate4;
    public bool CreateNewQuat;
    public Vector3 rotatedir;

    private Quaternion curRot;
    private Quaternion newRot;

    [Range(0,1)] public float lerp = 2;
    private void Update()
    {
        if (CreateNewQuat)
        {
            CreateNewQuat = false;
            SetNewQuat();
        }
        if (rotate)
        {
            if (rotation > 360 || rotation < 0)
            {
                flip = flip ? false : true;
            }

            if (flip)
            {
                rotation += Time.deltaTime * rotSpeed;
            }
            else
            {
                rotation -= Time.deltaTime * rotSpeed;
            }
            transform.rotation = Quaternion.AngleAxis(rotation, Vector3.back);
        }

        if (invert)
        {
            invert = false;
            InvertQuat();
        }

        if (rotate2)
        {
            rotate2 = false;
            Quaternion curRot1 = transform.rotation;
            Vector3 newRot1 = curRot1 * rotatedir;
            transform.rotation = Quaternion.Euler(newRot1);
        }
        if (rotate3)
        {
            if (lerp < 0.5f)
            {
                lerp += Time.deltaTime;
                transform.rotation = Quaternion.Lerp(curRot, newRot, lerp);
            }
            else
            {
                SetNewQuat();
                lerp = 0;
            }
        }

        if (rotate4)
        {
            transform.rotation = Quaternion.Lerp(curRot, newRot, lerp);
        }
    }

    void SetNewQuat()
    {
        newRot = Quaternion.Euler(curRot * rotatedir);
        curRot = transform.rotation;
    }

    void InvertQuat()
    {
        Quaternion curRot = transform.rotation;
        curRot = Quaternion.Inverse(curRot);
        transform.rotation = curRot;
    }
    
}

