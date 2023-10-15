using UnityEngine;
using UnityEngine.AI;

public class RandomNavMeshPoint
{
    const float citySize = 1000f;
    public static Vector3 GetRandomNavMeshPoint(float searchRadius = citySize)
    {
        Vector3 randomPoint = Random.insideUnitSphere * searchRadius;
        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomPoint, out hit, searchRadius, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return Vector3.zero;
    }
}