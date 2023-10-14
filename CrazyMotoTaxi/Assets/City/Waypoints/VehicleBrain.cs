using NPC.Vehicle;
using System.Collections.Generic;
using MyBox;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(PathFinder))]
public class VehicleBrain : MonoBehaviour
{
    [Header("Waypoints")]
    PathFinder pathFinder;
    NavMeshAgent navMeshAgent;
    private List<Vector3> waypoints;
    private int currentWaypointIndex = 0;
    [SerializeField] float allowedDistance = 1.0f;
    private bool destinationReached =false;

    [Header("Physics related components")]
    [SerializeField] VehicleMovement movement;
    Rigidbody rb;

    [Header("Braking")]
    [SerializeField, ReadOnly] bool isblockedFront;
    float brakeDistance= 4f;
    [SerializeField, Range(0.01f, 1f)] float brakeDistanceCalcMultiplier = 0.32f;

    Vector3 frontDirection;
    private void Awake()
    {
        pathFinder = GetComponent<PathFinder>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        movement = GetComponent<VehicleMovement>();
        pathFinder.e_waypointsGenerated += SetNewDestination;
        pathFinder.e_nextWaypoint += SetDestinationToNextWaypoint;
       // rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (destinationReached)
            return;

        frontDirection = transform.position + (transform.forward * brakeDistance);
        Debug.DrawLine(transform.position, frontDirection, isblockedFront ? Color.red : Color.green);
    }

    private void SetDestinationToNextWaypoint(Vector3 nextWaypoint)
    {
        navMeshAgent.SetDestination(nextWaypoint);       
    }
    public void SetNewDestination()
    {
        destinationReached = false;
    }


    //If Using phyisics
    //private void FixedUpdate()
    //{
    //    if (destinationReached)
    //        return;
    //    ApplySteering();
    //    CheckBlockedFront();
    //}

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