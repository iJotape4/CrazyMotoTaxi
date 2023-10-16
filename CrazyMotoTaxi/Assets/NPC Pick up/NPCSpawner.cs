using System.Net.Sockets;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    [SerializeField] NPCPooler npcPooler; // Reference to the NPCPooler
    private const string navMeshAreaName = "Anden";
    [SerializeField] float npcsZone = 50f;
    [SerializeField] Transform sphereCenter;

    // Start is called before the first frame update
    void Start()
    {
        if (npcPooler != null)
        {
            // Spawn NPCs from the pool 10 times
            for (int i = 0; i < 20; i++)
            {
                SpawnNPCFromPool();
            }
        }
    }

    [ContextMenu("SpawnNPCFromPool")]
    public void SpawnNPCFromPool()
    {
        if (npcPooler != null)
        {
            NPCClient npc = npcPooler.GetNextNPC();

            if (npc != null)
            {
                // Set the NPC's position to a random point within the defined area
                Vector3 spawnPosition = RandomNavMeshPoint.GetRandomNavMeshPoint(RandomNavMeshPoint.GetNavMeshAreaFromName(navMeshAreaName), sphereCenter.position, npcsZone);
                npc.transform.position = spawnPosition;
            }
        }
    }
}
