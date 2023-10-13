using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class NPCClient : MonoBehaviour
{
    private DestinationManager destinationManager;
    private Destination destination;
    [SerializeField]
    private Collider detectionCollider;


    private void Start()
    {
        destinationManager = FindObjectOfType<DestinationManager>();
    }


    [ContextMenu("Asignar destino")]
    public void AsignDestination()
    {
        destination = destinationManager.AssignRandomDestination();
    }

    public void ResetNPC()
    {
        destination = null;
    }

    public void NPCPickUP()
    {
        destination.gameObject.SetActive(true);
        detectionCollider.enabled = false;

    }
}
