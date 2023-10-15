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
                AnimateNPCPickup(client, Seat.transform);
                lastClient = client;
                
            }
        }
    }

    private void AnimateNPCPickup(NPCClient client, Transform targetTransform)
    {
        // Calculate the jump start and end positions
        Vector3 startPosition = client.transform.position;
        Vector3 jumpHeight = Vector3.up * 2.0f; // Adjust the jump height as needed
        Vector3 endPosition = targetTransform.position;

        // Create the jump animation
        Sequence jumpSequence = DOTween.Sequence();
        jumpSequence.Append(client.transform.DOJump(jumpHeight, 1.0f, 1, 1.0f).SetEase(Ease.OutQuad));
        jumpSequence.Append(client.transform.DOMove(endPosition, 0.5f).SetEase(Ease.InQuad));
    }
}
