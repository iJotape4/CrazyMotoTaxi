using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationManager : SinglentonParent<DestinationManager>
{
    public Destination[] destinations;


    private void Start()
    {
        foreach (Destination dest in destinations) 
        {
            dest.gameObject.SetActive(false);
        }
    }

    public Destination AssignRandomDestination()
    {
        int randomIndex = Random.Range(0, destinations.Length);
        return destinations[randomIndex];
    }
}
