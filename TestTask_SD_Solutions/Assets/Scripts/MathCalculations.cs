using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathCalculations  {

	public static Vector3 CalculateNewPositionByRadiusAndAngle(float radius, float angle, float baseY)
    {
        float xCoord = radius * Mathf.Cos(angle);
        float zCoord = radius * Mathf.Sin(angle);
        return new Vector3(xCoord, baseY, zCoord);
    }
    public static Vector3 MiscalculationParabolicTrajectoryOfBody(float time, float speed,float angle,float gravity)
    {
        float vz = speed * time * Mathf.Cos(angle) * Time.deltaTime;
        float vy = speed * time * Mathf.Sin(angle) * Time.deltaTime - gravity * (time * time) / 2 * Time.deltaTime;
        return new Vector3(0, vy, vz);
    }
    public static int CalculateDamageByDistance(float maxDistance, float distToTarget, int maxDamage)
    {
        float distanceMultiplayer = distToTarget / maxDistance;
        float currentDamage = maxDamage - (maxDamage * distanceMultiplayer);
        return (int) currentDamage;
    }
    public static float CalculateDistance3D(Vector3 firstPosition, Vector3 LastPosition)
    {
        Vector3 difference = firstPosition - LastPosition;
        float distance = Mathf.Sqrt(Mathf.Pow(difference.x,2)+ Mathf.Pow(difference.y, 2) + Mathf.Pow(difference.z, 2));
        return distance;
    }
    public static bool FindTrajectory(float speed, float angle,float gravity)
    {
        float maxDistance = (Mathf.Pow(speed,2) / gravity) * Mathf.Sin(2 * 45);
        float curDistance = (Mathf.Pow(speed, 2) / gravity) * Mathf.Sin(2 * gravity);

        bool trajectory = false;
        if ((maxDistance / 2) >=curDistance)
        {
            trajectory = true;
        }
        return false;
    }
    public static float CalculateAngleToHitTarget(float speed,float distance, Vector3 targetPosition, float gravity = 9.81f)
    {
        float y = targetPosition.y;

        float vSqr = Mathf.Pow(speed,2);
        float underTheRoot = Mathf.Pow(vSqr, 2) - gravity * (gravity * Mathf.Pow(distance,2) + 2 * y * vSqr);
        
        if (underTheRoot < 0f)
            return -1f;

        float rightSide = Mathf.Sqrt(underTheRoot);
        float top =  vSqr - rightSide;
        float bottom = gravity * distance;
        
        return Mathf.Atan2(top, bottom) * Mathf.Rad2Deg;

    }
}