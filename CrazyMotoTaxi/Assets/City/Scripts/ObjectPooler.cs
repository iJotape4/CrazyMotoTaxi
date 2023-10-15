using System.Collections.Generic;
using UnityEngine;
using MyBox;

namespace City
{
    public abstract class ObjectPooler<T> : SinglentonParent<ObjectPooler<T>>
    {
        protected GameObject[] objectsPrefabs;
        public int poolSize = 30;

        [SerializeField, ReadOnly] private List<GameObject> objectPool;
        protected  abstract string navMeshAreaName { get; }
        protected  abstract string resourcesPath { get; }

        protected virtual void Start()
        {
            objectPool = new List<GameObject>();

            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = InstantiateObjectFromList();
                obj.SetActive(false);
                objectPool.Add(obj);
            }
        }

        public GameObject GetPooledObject()
        {
            for (int i = 0; i < objectPool.Count; i++)
            {
                if (!objectPool[i].activeInHierarchy)
                {
                    return objectPool[i];
                }
            }
            return null;
        }

        public void ReturnToPool(GameObject obj)
        {
            obj.SetActive(false);
        }

        public int GetActiveObjectsAmount()
        {
            int activeObjectsAmount;
            activeObjectsAmount = 0;
            foreach (var obj in objectPool)
                if (obj.activeInHierarchy)
                    activeObjectsAmount++;

            return activeObjectsAmount;
        }

        protected abstract GameObject InstantiateObjectFromList();

        [ContextMenu("Instance Car")]
        public void InstantiateObjectInWorld()
        {
            GameObject go = GetPooledObject();
            go.transform.position = GetRandomPosition();
            go.SetActive(true);
        }

        protected Vector3 GetRandomPosition()
        {
            return RandomNavMeshPoint.GetRandomNavMeshPoint(RandomNavMeshPoint.GetNavMeshAreaFromName(navMeshAreaName), PlayerPositionNotifier.playerPosition, 200f);
        }
    }
}