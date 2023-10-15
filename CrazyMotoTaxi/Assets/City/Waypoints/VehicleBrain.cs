using NPC.Vehicle;
using System.Collections.Generic;
using MyBox;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[RequireComponent(typeof(PathFinder))]
public class VehicleBrain : MonoBehaviour
{
    [Header("Waypoints")]
    PathFinder pathFinder;
    NavMeshAgent navMeshAgent;
    private List<Vector3> waypoints;
    private int currentWaypointIndex = 0;
    [SerializeField] float allowedDistance = 1.0f;
    private bool destinationReached = false;

    [Header("Physics related components")]
    [SerializeField] VehicleMovement movement;
    Rigidbody rb;

    [Header("Braking")]
    [SerializeField, ReadOnly] bool isblockedFront;
    float brakeDistance = 4f;
    [SerializeField, Range(0.01f, 1f)] float brakeDistanceCalcMultiplier = 0.32f;
    private bool isTraversingOffMeshLink = false;
    Vector3 currentWaypoint;

    Vector3 frontDirection;
    [SerializeField] Transform carFront;
    private void Awake()
    {
        pathFinder = GetComponent<PathFinder>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        movement = GetComponent<VehicleMovement>();
        pathFinder.e_waypointsGenerated += SetNewDestination;
        pathFinder.e_nextWaypoint += (currentWayPoint) => StartCoroutine( SetDestinationToNextWaypoint(currentWayPoint));
        pathFinder.e_positionBugged += FixPosition;
        // rb = GetComponent<Rigidbody>();
    }

    //TODO : If want to avoid the high velocity when traversig offmesh links
    //IEnumerator TraverseOffMeshLink(OffMeshLinkData currentOffMeshLinkData)
    //{
    //    isTraversingOffMeshLink = true;
    //    navMeshAgent.enabled = false;


    //    // Realiza el cruce del Off Mesh Link manualmente
    //    float journeyLength = Vector3.Distance(transform.position, currentOffMeshLinkData.endPos);
    //    float journeyTime = 10.0f;  // Puedes ajustar esto
    //    float startTime = Time.time;
    //    while (Time.deltaTime - startTime < journeyTime)
    //    {
    //        float distanceCovered = (Time.time - startTime) * journeyLength / journeyTime;
    //        float fractionOfJourney = distanceCovered / journeyLength;
    //        transform.position = Vector3.Lerp(transform.position, currentOffMeshLinkData.endPos, fractionOfJourney);
    //        yield return null;
    //    }

    //    // Reactiva el componente NavMeshAgent
    //    navMeshAgent.enabled = true;
    //    // Marca el cruce como completado
    //    isTraversingOffMeshLink = false;
    //    navMeshAgent.isStopped = false;

    //    while (!navMeshAgent.isOnNavMesh)
    //        yield return null;
    //    navMeshAgent.SetDestination(currentWaypoint);
    //}

    private void Update()
    {
        if (destinationReached)
            return;

        //TODO : If want to avoid the high velocity when traversig offmesh links

        //if (navMeshAgent.isOnOffMeshLink && !isTraversingOffMeshLink)
        //{
        //    StartCoroutine(TraverseOffMeshLink(navMeshAgent.currentOffMeshLinkData));
        //}

        frontDirection = transform.position + (transform.forward * brakeDistance);
        //Debug.DrawLine(transform.position, frontDirection, isblockedFront ? Color.red : Color.green);
    }

    private IEnumerator SetDestinationToNextWaypoint(Vector3 nextWaypoint)
    {
        while (!navMeshAgent.isOnNavMesh)
            yield return null;
        
        currentWaypoint = nextWaypoint;
        navMeshAgent.SetDestination(currentWaypoint);       
    }

    public void SetNewDestination()
    {
        destinationReached = false;
    }

    public void FixPosition()
    {
        navMeshAgent.SetDestination(-carFront.position);
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