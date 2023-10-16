using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnerTest : MonoBehaviour
{
    public NPCPooler npcPooler;
    public Transform[] spawnPoints; // Define three spawn points in the Inspector.

    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            NPCClient npc = npcPooler.GetNextNPC();
            npc.transform.position = spawnPoints[i].position;
        }
    }
}
