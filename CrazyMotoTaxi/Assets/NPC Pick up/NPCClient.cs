using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class NPCClient : MonoBehaviour
{
    private Destination destination;
    public bool isPickedUP = false;
    private Animator animator;

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
    }

    public void NPCPickUP()
    {
        if (!isPickedUP) 
        {
            destination.gameObject.SetActive(true);
            isPickedUP = true;

            // Play the "Jump" animation.
            animator.SetTrigger("JumpGT");
        }
        
    }

    public void DeliveredNPC()
    {
        animator.SetTrigger("JumpOff");
    }
}
