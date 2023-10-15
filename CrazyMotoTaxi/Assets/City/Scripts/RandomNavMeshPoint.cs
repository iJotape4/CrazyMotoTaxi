using UnityEngine;
using UnityEngine.AI;

public class RandomNavMeshPoint
{
    const float citySize = 2000f;
    public static Vector3 GetRandomNavMeshPoint(float searchRadius = citySize,int navMeshArea = NavMesh.AllAreas )
    {
        Vector3 randomPoint = Random.insideUnitSphere * searchRadius;
        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomPoint, out hit, searchRadius, navMeshArea))
        {
            return hit.position;
        }
        return Vector3.zero;
    }

    public static int GetNavMeshAreaFromName(string navmeshAreaName)
    {
        return  1 << NavMesh.GetAreaFromName(navmeshAreaName);
    }
}