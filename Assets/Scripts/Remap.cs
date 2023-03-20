using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Remap : MonoBehaviour
{
    public float minRange;
    public float maxRange;
    public float minValue = 0f;
    public float maxValue = 500f;
    public float curValue;

    public Transform player;
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(player.position, .5f);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, minRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, maxRange);

        curValue = GetCurValue(player.transform.position);
    }

    float GetCurValue(Vector3 position)
    {
        float dist = VectorMagnitude.MyDistance(transform.position, position);
        return RemapFunction(maxRange, minRange,  minValue,maxValue, dist);
    }

    static float RemapFunction(float inMin, float inMax, float outMin, float outMax, float distance)
    {
        float t = Mathf.InverseLerp(inMax, inMin, distance);
        return Mathf.Lerp(outMin, outMax, t);
    }
}
