using NPC.Vehicle;
using System.Collections.Generic;
using MyBox;
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
    private bool destinationReached;
    Rigidbody rb;
    float brakeDistance= 4f;
    [SerializeField, Range(0.01f, 1f)] float brakeDistanceCalcMultiplier = 0.32f;
    [SerializeField, ReadOnly] bool isblockedFront;

    Vector3 frontDirection;
    private void Awake()
    {
        pathFinder = GetComponent<PathFinder>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        movement = GetComponent<VehicleMovement>();
       // rb = GetComponent<Rigidbody>();
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
        if (destinationReached)
            return;

        frontDirection = transform.position + (transform.forward * brakeDistance);
        Debug.DrawLine(transform.position, frontDirection, isblockedFront ? Color.red : Color.green);


        // Check if the agent has reached the current waypoint.
        if (Vector3.Distance( transform.position, waypoints[currentWaypointIndex]) <= allowedDistance)
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
                destinationReached = true;
            }
        }
    }

    //private void FixedUpdate()
    //{
    //    if (destinationReached)
    //        return;
    //    ApplySteering();
    //    CheckBlockedFront();
    //}

    private void SetDestinationToNextWaypoint()
    {
        if (currentWaypointIndex < waypoints.Count)
        {
            navMeshAgent.SetDestination(waypoints[currentWaypointIndex]);
        }
    }


    //If Using phyisics
    void ApplySteering() // Applies steering to the Current waypoint
    {
        Vector3 relativeVector = transform.InverseTransformPoint(waypoints[currentWaypointIndex]);
        float steeringAngle = (relativeVector.x / relativeVector.magnitude);

        movement.ApplySteering(steeringAngle);
    }

    void CheckBlockedFront()
    {
        NavMeshHit frontRay;
        isblockedFront = NavMesh.Raycast(transform.position, frontDirection, out frontRay, NavMesh.AllAreas);

        if (isblockedFront)       
            movement.ApplyBrakes(true);     
        else
        {
            CalculateBrakeDistance();
            movement.ApplyAcceleration(1000f);
        }
        
    }

    void CalculateBrakeDistance()
    {
        float newDistance = Mathf.Pow(rb.velocity.magnitude, 2) * brakeDistanceCalcMultiplier;
        if (newDistance <= 4f)
            brakeDistance = 4f;
        else
            brakeDistance = newDistance;
    }
}