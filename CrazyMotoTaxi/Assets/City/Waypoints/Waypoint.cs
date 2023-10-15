using System.Collections.Generic;
using UnityEngine;

namespace City
{
    [RequireComponent(typeof(RadialRaycast))]
    public class Waypoint : MonoBehaviour
    {
        public Vector3 position;
        public List<Waypoint> Neighbors;

        public float gCost; // Coste de movimiento desde el nodo de inicio hasta este nodo
        public float hCost; // Heurística: estimación del coste desde este nodo hasta el nodo objetivo
        public float fCost => gCost + hCost; // Coste total (fCost = gCost + hCost)
        public Waypoint parent;

        RadialRaycast radialRaycast;

        public Waypoint(Vector3 position)
        {
            this.position = position;
            Neighbors = new List<Waypoint>();
        }

        private void Start()
        {
            position = transform.position;
          
        }

#if UNITY_EDITOR
        [ExecuteInEditMode]
        [ContextMenu("RecalculateNeihbors")]
        public void GetNeighBors()
        {
            radialRaycast = GetComponent<RadialRaycast>();
            radialRaycast.e_objectFound += AddNeighBor;
            radialRaycast.DrawRadialRay();
        }

        public void AddNeighBor(Transform neighbor)
        {
            Waypoint newNeighbor = neighbor.GetComponent<Waypoint>();
            if(!Neighbors.Contains(newNeighbor)) 
                Neighbors.Add(newNeighbor);
        }
#endif
    }
}