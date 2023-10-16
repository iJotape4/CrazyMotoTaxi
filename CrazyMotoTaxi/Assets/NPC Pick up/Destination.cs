using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{
    public GameObject deliverypoint;
    public void DeliveredClient()
    {
        GameManager.Instance.StopTaxiFare();
        transform.gameObject.SetActive(false);
    }

}
