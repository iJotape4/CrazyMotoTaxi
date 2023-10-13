using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPooler : MonoBehaviour
{
    public GameObject npcPrefab; // The NPC prefab you want to pool.
 
    public int maxPoolSize = 10; // Maximum number of cards to pool.

    private Queue<NPCClient> npcPool = new Queue<NPCClient>();

    void Awake()
    {
        // Pre-fill the pool with card instances.
        FillPool(maxPoolSize);
    }

    public NPCClient GetNextNPC()
    {
        if (npcPool.Count == 0)
        {
            CreateNewNPC();
        }

        NPCClient npc = npcPool.Dequeue();
        npc.AsignDestination();
        npc.gameObject.SetActive(true);

        return npc;
    }

    public void ReturnNPCToPool(NPCClient npc)
    {
        npc.ResetNPC(); // Reset the card's state for the next round.
        npc.transform.SetParent(transform);
        npc.gameObject.SetActive(false);

        npcPool.Enqueue(npc);
    }

    private void FillPool(int size)
    {
        for (int i = 0; i < size; i++)
        {
            CreateNewNPC();
        }
    }

    private void CreateNewNPC()
    {
        GameObject npcObj = Instantiate(npcPrefab, transform);
        NPCClient npc = npcObj.GetComponent<NPCClient>();
        npc.gameObject.SetActive(false);

        npcPool.Enqueue(npc);
    }
}
