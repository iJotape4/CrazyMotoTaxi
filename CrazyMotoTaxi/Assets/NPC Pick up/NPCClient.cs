using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor.PackageManager;
using UnityEngine;

public class NPCClient : MonoBehaviour
{
    private Destination destination;
    public bool isPickedUP = false;
    private Animator animator;
    public GameObject areaOfPickup;



    [ContextMenu("Asignar destino")]
    public void AsignDestination()
    {
        destination = DestinationManager.Instance.AssignRandomDestination();
    }

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void ResetNPC()
    {
        destination = null;
        isPickedUP = false;
    }

    public void NPCPickUP()
    {
        if (!isPickedUP)
        {
            // Store the original positions
            Vector3 originalClientPosition = transform.position;
            Vector3 originalDestinationPosition = destination.transform.position;

            destination.gameObject.SetActive(true);
            isPickedUP = true;

            // Play the "Jump" animation.
            animator.SetTrigger("JumpGT");

            // Calculate the distance between the client and destination
            float distance = Vector3.Distance(originalClientPosition, originalDestinationPosition);

            // Calculate the payment based on the distance
            float payment = CalculateMaxPayment(distance);

            // Add the payment to the score
            GameManager.Instance.StartTaxiFare(payment);
        }
    }

    // Function to calculate the payment based on distance
    private float CalculateMaxPayment(float distance)
    {
        // Payment rate of 3000 per 100 units of distance
        float paymentRate = 30;
        return (distance * paymentRate);
    }

    public void DeliveredNPC()
    {
        animator.SetTrigger("JumpOff");
    }

    public void TakeTaxi()
    {
        animator.SetTrigger("TakeTaxi");
    }

    public void EvadeBike()
    {
        animator.SetTrigger("AboutToCrash");
        areaOfPickup.SetActive(false);
        AnimateEvadeBike();

    }


    public void AnimateNPCPickup()
    {
        Vector3 endPosition = new Vector3(0f, 0f, 0f);
        Vector3 endRotation = new Vector3(0f, 0f, 0f);

        // Create the jump animation
        Sequence jumpSequence = DOTween.Sequence();

        // Add a jump animation with local position
        jumpSequence.Append(transform.DOLocalJump(endPosition, 1.0f, 1, 1.0f).SetEase(Ease.OutQuad));

        // Add a rotation animation to set y-component to 0
        jumpSequence.Join(transform.DOLocalRotate(endRotation, 1.0f, RotateMode.Fast));

    }




    public void AnimateNPCDelivered(Vector3 endPosition)
    {
        transform.position = transform.position;
        Vector3 jumpHeight = Vector3.up * 0.5f; // Adjust the jump height as needed

        transform.DOJump(endPosition, 1.0f, 1, 1.0f).SetEase(Ease.OutQuad);

    }

    public void AnimateEvadeBike()
    {
        // Set the distance to move to the right
        Vector3 moveDistance = Vector3.right * 2.0f;

        // Create the move animation
        transform.DOLocalMove(transform.localPosition + moveDistance, 0.5f)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                // Set the GameObject's areaOfPickup to true when the animation is complete
                areaOfPickup.SetActive(true);
            });
    }
}
