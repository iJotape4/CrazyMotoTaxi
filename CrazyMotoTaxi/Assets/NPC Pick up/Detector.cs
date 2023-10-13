using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Detector : MonoBehaviour
{

    private Rigidbody rb;
    private bool isInService = false;

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (rb.velocity.magnitude <= 0.2f)
        {
            Destination destination = other.GetComponent<Destination>();
            NPCClient client = other.GetComponentInParent<NPCClient>();

            if (destination  != null && isInService) 
            {
                destination.DeliveredClient();
                isInService = false;
            }

            if (client != null && !isInService) 
            {
                isInService = true;
                client.NPCPickUP();
            }
        }
    }
}
