
using System;
using UnityEngine;
using UnityEngine.AI;

public class GeometryAnalysis : MonoBehaviour
{
    NavMeshTriangulation navMeshData;
    Vector3[] vertices;
    int[] triangles;

    // Start is called before the first frame update
    void Start()
    {
        navMeshData = NavMesh.CalculateTriangulation();
        vertices = navMeshData.vertices;
        triangles = navMeshData.indices;

        for (int i = 0; i < triangles.Length; i += 3)
        {
            Vector3 vertexA = vertices[triangles[i]];
            Vector3 vertexB = vertices[triangles[i + 1]];
            Vector3 vertexC = vertices[triangles[i + 2]];

            float angle = CalculateAngle(vertexA, vertexB, vertexC);
            Debug.Log("Ángulo entre vértices: " + i+" "+(i+1)+" "+(i+2)+" = "  + angle);
        }
    }

    float CalculateAngle(Vector3 a, Vector3 b, Vector3 c)
    {
        Vector3 AB = b - a;
        Vector3 BC = c - b;
        float dotProduct = Vector3.Dot(AB.normalized, BC.normalized);
        float angle = Mathf.Acos(Mathf.Clamp(dotProduct, -1f, 1f));
        return Mathf.Rad2Deg * angle;

    }
}

