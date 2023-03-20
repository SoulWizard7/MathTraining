
using System;
using UnityEngine;
using UnityEditor;

public class GizmoClock : MonoBehaviour
{
    public bool setTimeAtStart;

    [Range(0, 60)] public int seconds;
    [Range(0, 60)] public int minutes;
    [Range(0, 60)] public int hours;
    
    private Vector2 secVec;
    private Vector2 minVec;
    private Vector2 hourVec;
    private float sec = 15;
    private float min = 15;
    private float hour = 3;


    private float time;
    
    public static Vector2 AngRadToDir(float angleRad) => new (Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    public static Vector2 AngDegToDir(float angleDeg) =>  new (Mathf.Cos(angleDeg * Mathf.Deg2Rad), Mathf.Sin(angleDeg * Mathf.Deg2Rad));

    private void Start()
    {
        if(!setTimeAtStart) return;

        sec -= seconds;
        min -= minutes + ((float)seconds / 60);
        hour -= hours + ((float)minutes / 60) + ((float)seconds / 3600);
    }

    private void Update()
    {
        time = Time.deltaTime;
        
        sec -= time;
        secVec = AngRadToDir(sec / 60 * Mathf.PI * 2);
        min -= time / 60;
        minVec = AngRadToDir(min / 60 * Mathf.PI * 2);
        hour -= time / 3600;
        hourVec = AngRadToDir(hour / 12 * Mathf.PI * 2);
    }

    private void OnDrawGizmos()
    {
        Handles.DrawWireDisc(transform.position, Vector3.forward, 1);
        Gizmos.DrawRay(transform.position, secVec * 0.9f);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, minVec);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, hourVec * .8f);

        float ang = 0;

        for (int i = 0; i < 12; i++)
        {
            Vector2 dir = AngDegToDir(ang);
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(transform.position + ((Vector3)dir * 0.9f), transform.position + (Vector3)dir);
            ang += 30;
        }

        ang = 6;

        for (int i = 1; i < 60; i++)
        {
            Vector2 dir = AngDegToDir(ang);
            Gizmos.color = Color.gray;
            Gizmos.DrawLine(transform.position + ((Vector3)dir * 0.95f), transform.position + (Vector3)dir);

            if ((i % 5) - 4 == 0)
            {
                ang += 6;
                i++;
            }
            ang += 6;
        }
    }
}
