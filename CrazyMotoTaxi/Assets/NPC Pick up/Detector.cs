using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

public class Detector : MonoBehaviour
{

    private Rigidbody rb;
    private bool isInService = false;
    [SerializeField]
    private GameObject Seat;
    private NPCClient lastClient;

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
                lastClient.DeliveredNPC();
                lastClient.transform.SetParent(null);
                isInService = false;
            }

            if (client != null && !isInService && !client.isPickedUP) 
            {
                isInService = true;
                client.NPCPickUP();
                client.transform.SetParent(Seat.transform);
                lastClient = client;
                
            }
        }
    }
}
