using UnityEngine;
using UnityEngine.AI;

public class RandomNavMeshPoint
{
    private static float searchRadius = 2000f;
    private static int navMeshArea = NavMesh.AllAreas;
    private static Vector3 center = Vector3.zero;

    public static Vector3 GetRandomNavMeshPoint()
    {
        Vector3 randomPoint = center + Random.insideUnitSphere* searchRadius;
        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomPoint, out hit, searchRadius, navMeshArea))
        {
            return hit.position;
        }
        return Vector3.zero;
    }

    public static Vector3 GetRandomNavMeshPoint(int _navMeshArea)
    {
       navMeshArea = _navMeshArea;
       return GetRandomNavMeshPoint();
    }

    public static Vector3 GetRandomNavMeshPoint(int _navMeshArea, Vector3 _center)
    {
        center = _center;
        return GetRandomNavMeshPoint(_navMeshArea);
    }

    public static Vector3 GetRandomNavMeshPoint(int _navMeshArea, Vector3 _center, float _searchRadius)
    {
        searchRadius = _searchRadius;
        return GetRandomNavMeshPoint(_navMeshArea, _center);
    }


    public static int GetNavMeshAreaFromName(string navmeshAreaName)
    {
        return  1 << NavMesh.GetAreaFromName(navmeshAreaName);
    }
}