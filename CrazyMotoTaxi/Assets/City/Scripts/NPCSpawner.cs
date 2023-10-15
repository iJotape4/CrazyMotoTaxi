using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    [SerializeField] GameObject npc;
    private const string navMeshAreaName = "Anden";
    [SerializeField] float npcsZone =50f;
    [SerializeField] Transform sphereCenter;
    [ContextMenu("InstantiateNPC")]
   public void InstantiateCar()
   {
        //int r = Random.Range(0, carPrefabs.Length);
        Instantiate(npc, RandomNavMeshPoint.GetRandomNavMeshPoint(RandomNavMeshPoint.GetNavMeshAreaFromName(navMeshAreaName), sphereCenter.position, npcsZone), Quaternion.identity);
   }
}