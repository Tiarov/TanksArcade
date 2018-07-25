using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Helper.ObjectPooling
{
    public class PoolManager : MonoBehaviour
    {
        private static PoolManager _instance;
        private readonly Dictionary<int, Queue<ObjectInPool>> _poolDictionary = new Dictionary<int, Queue<ObjectInPool>>();

        public static PoolManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<PoolManager>();
                }
                return _instance;
            }
        }

        public void CreatePool(GameObject prefab, int poolSize)
        {
            GameObject newObjectPool = new GameObject(prefab.name + " Pool");
            newObjectPool.transform.parent = transform;

            var poolKey = prefab.GetInstanceID();
            if (_poolDictionary.ContainsKey(poolKey))
                return;

            _poolDictionary.Add(poolKey, new Queue<ObjectInPool>());

            for (int i = 0; i < poolSize; i++)
            {
                ObjectInPool newObject = new ObjectInPool(Instantiate(prefab, newObjectPool.transform));
                _poolDictionary[poolKey].Enqueue(newObject);
            }

        }

        public GameObject ReuseObject(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            var poolId = prefab.GetInstanceID();

            if (!_poolDictionary.ContainsKey(poolId))
                return null;

            var objectToReuse = _poolDictionary[poolId].Dequeue();
            if (objectToReuse.GameObject.activeInHierarchy)
            {
                _poolDictionary[poolId].Enqueue(objectToReuse);
                foreach (var objectInPool in _poolDictionary[poolId])
                {
                    if (!objectInPool.GameObject.activeInHierarchy)
                        return objectInPool.Reuse(position, rotation);
                }
                objectToReuse = new ObjectInPool(Instantiate(prefab, objectToReuse.Transform.parent));
            }

            _poolDictionary[poolId].Enqueue(objectToReuse);

            return objectToReuse.Reuse(position, rotation);
        }
    }
}
