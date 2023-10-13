using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationManager : MonoBehaviour
{
    Destination[] destinations;


    private void Start()
    {
        destinations = GetComponentsInChildren<Destination>();
    }

    public Destination AssignRandomDestination()
    {
        int randomIndex = Random.Range(0, destinations.Length);
        return destinations[randomIndex];
    }
}
