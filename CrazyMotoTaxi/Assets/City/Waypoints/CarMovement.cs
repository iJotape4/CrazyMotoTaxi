using NPC.Vehicle;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarMovement : MonoBehaviour
{
    PathFinder pathFinder;
    private List<Vector3> waypoints;
    private int currentWaypointIndex = 0;
    NavMeshAgent navMeshAgent;
    [SerializeField] float allowedDistance = 1.0f;
    [SerializeField] VehicleMovement movement;
    private void Awake()
    {
        pathFinder = GetComponent<PathFinder>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    // Start is called before the first frame update
    void Start()
    {
        waypoints = pathFinder.waypoints;

        if (waypoints != null && waypoints.Count > 0)       
            SetDestinationToNextWaypoint();      
        else       
           Debug.LogWarning("No waypoints assigned.");
        
    }

    private void Update()
    {
        // Check if the agent has reached the current waypoint.
        if (navMeshAgent.remainingDistance <= allowedDistance)
        {

            currentWaypointIndex++;

            // If there are more waypoints, move to the next one.
            if (currentWaypointIndex < waypoints.Count)
            {
                SetDestinationToNextWaypoint();
            }
            else
            {
                // All waypoints reached, you can handle completion logic here.
                Debug.Log("All waypoints reached.");
            }
        }
    }

    private void SetDestinationToNextWaypoint()
    {
        if (currentWaypointIndex < waypoints.Count)
        {
            navMeshAgent.SetDestination(waypoints[currentWaypointIndex]);
        }
    }
}