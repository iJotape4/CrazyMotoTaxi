using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeTaxi : MonoBehaviour
{
    NPCClient npc;
    private void Start()
    {
        npc = GetComponentInParent<NPCClient>();
    }

    private void OnTriggerEnter(Collider other)
    {
        npc.TakeTaxi();
    }
}
