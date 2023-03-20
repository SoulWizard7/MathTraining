using UnityEngine;

namespace DefaultNamespace
{
    public class ProjectileMovement : MonoBehaviour
    {
        float CalculateTimeToMaxHorizontalDistance1(float ProjectileSpeed, float LaunchAngle,
            float MaxHorizontalDistance)
        {	
            /*
            float rad = FMath::DegreesToRadians(LaunchAngle);
            float rad = Mathf.Deg2Rad DegreesToRadians(LaunchAngle);
            float cosRad = FMath::Cos(rad);
            float HorizontalSpeed = ProjectileSpeed * cosRad;
            return MaxHorizontalDistance / HorizontalSpeed;	*/
            return 0;
        }
    }
}