using System.Collections;
using UnityEngine;

namespace City
{
    public class CarsPooler : ObjectPooler<CarsPooler>
    {
        [SerializeField] float cityRadius = 300f;
        [SerializeField, Range(2,20)] int maxObjectsAmount =8;
        [SerializeField, Range(1f,10f)] float poolingTick =5f;
        protected override string navMeshAreaName { get => "Walkable";}
        protected override string resourcesPath { get => "Cars"; }

        protected override void Start()
        {
            objectsPrefabs = Resources.LoadAll<GameObject>(resourcesPath);
            new GameObject("----- Cars Pooler -------");
            base.Start();
            StartCoroutine(PoolerBrain());
        }

        protected override GameObject InstantiateObjectFromList()
        {
            int r = Random.Range(0, objectsPrefabs.Length);
            GameObject go = Instantiate(objectsPrefabs[r], GetRandomPosition(), Quaternion.identity, null);
            return go;             
        } 
        
        IEnumerator PoolerBrain()
        {
            while (true)
            {
                Debug.Log("routineWorking");
                int objectsAmount = GetActiveObjectsAmount();
                Debug.Log(objectsAmount);
                if (objectsAmount >= maxObjectsAmount)
                    yield return new WaitForSeconds(poolingTick);

                else             
                    for (int i = 0; i < maxObjectsAmount - objectsAmount; i++)
                        InstantiateObjectInWorld();
                
                yield return null;
            }
        }
    }
}