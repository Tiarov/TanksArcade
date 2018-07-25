using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameLogic.Global
{
    public class WallsInstantiator
    {
        private GameObject _wallPrefab;

        public WallsInstantiator(GameObject wallPrefab)
        {
            _wallPrefab = wallPrefab;
        }

        public List<GameObject> InstantiateWalls(int[,] arrayMap, int usefulValue, Transform parent,
            bool globalPosition = false)
        {
            var resultList = new List<GameObject>();
            for (int i = 0; i < arrayMap.GetLength(0); i++)
            {
                for (int j = 0; j < arrayMap.GetLength(1); j++)
                {
                    if (arrayMap[i, j] == usefulValue)
                    {
                        resultList.Add(InstantiateWall(i, j, parent, globalPosition));
                    }
                }
            }

            return resultList;
        }

        public GameObject InstantiateWall(int x, int z, Transform parent, bool globalPosition = false)
        {
            var go = GameObject.Instantiate(_wallPrefab, parent, globalPosition);
            if (globalPosition)
            {
                go.transform.position = new Vector3(x, 0, z);
            }
            else
            {
                go.transform.localPosition = new Vector3(x, 0, z);
            }

            return go;
        }
    }
}
