using UnityEngine;

public class NPCPooler : MonoBehaviour
{
    [SerializeField] GameObject npc;
    private const string navMeshAreaName = "Anden";
    float cityRadius =2000f; 
    [ContextMenu("InstantiateNPC")]
   public void InstantiateCar()
   {
        //int r = Random.Range(0, carPrefabs.Length);
        Instantiate(npc, RandomNavMeshPoint.GetRandomNavMeshPoint(2000f,RandomNavMeshPoint.GetNavMeshAreaFromName(navMeshAreaName)), Quaternion.identity);

   }
}