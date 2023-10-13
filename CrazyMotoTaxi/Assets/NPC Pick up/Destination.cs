using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{
    Transform destinationTransform;
    DestinationManager destinationManager;

    public void DeliveredClient()
    {
        GameManager.Instance.Score();
        transform.gameObject.SetActive(false);
    }

}
