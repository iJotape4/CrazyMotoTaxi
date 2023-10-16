using DG.Tweening;
using UnityEngine;
using Sequence = DG.Tweening.Sequence;

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
                lastClient.transform.SetParent(null);
                lastClient.DeliveredNPC();
                lastClient.AnimateNPCDelivered(destination.deliverypoint.transform.position);
                isInService = false;
                destination.DeliveredClient();
            }

            if (client != null && !isInService && !client.isPickedUP) 
            {
                isInService = true;
                client.NPCPickUP();
                client.transform.SetParent(Seat.transform);
                client.AnimateNPCPickup();
                lastClient = client;    
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (rb.velocity.magnitude >= 6f)
        {
            NPCClient client = other.GetComponentInParent<NPCClient>();

            if (client != null)
            {
                client.EvadeBike();
            }
            
        }
    }

}
