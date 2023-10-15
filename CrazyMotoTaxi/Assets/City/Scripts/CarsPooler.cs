using UnityEngine;

public class CarsPooler : MonoBehaviour
{
    [SerializeField] GameObject[] carPrefabs;
    private const string carsPrefabsPath = "Cars";
    [SerializeField] float cityRadius = 100f;
    private const string navMeshAreaName = "Walkable";
    // Start is called before the first frame update
    void Start()
    {
        carPrefabs = Resources.LoadAll<GameObject>(carsPrefabsPath);
    }

   [ContextMenu("InstantiateCar")]
   public void InstantiateCar()
   {
        int r = Random.Range(0, carPrefabs.Length);
        Instantiate(carPrefabs[r], RandomNavMeshPoint.GetRandomNavMeshPoint(RandomNavMeshPoint.GetNavMeshAreaFromName(navMeshAreaName), Vector3.zero, cityRadius), Quaternion.identity);

   }
}
