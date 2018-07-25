using UnityEngine;

namespace Assets.Scripts.Helper.ObjectPooling
{
    public class ObjectInPool
    {
        public GameObject GameObject { get; private set; }
        public Transform Transform { get; private set; }


        public ObjectInPool(GameObject gameObject)
        {
            GameObject = gameObject;
            Transform = gameObject.transform;

            GameObject.SetActive(false);
        }

        public GameObject Reuse(Vector3 position, Quaternion rotation)
        {
            Transform.position = position;
            Transform.rotation = rotation;

            GameObject.SetActive(true);

            return GameObject;
        }
    }
}
