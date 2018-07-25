using UnityEngine;

namespace Assets.Scripts.GameLogic.Global
{
    public class LocationController : MonoBehaviour
    {
        [SerializeField]
        private int XSize = 26;
        [SerializeField]
        private int ZSize = 23;
        [SerializeField]
        private GameObject wallGo;
        [SerializeField]
        public Transform WallsParent;


        public int[,] map
        {
            get
            {
                return _map;
            }
        }

        private int[,] _map;

        private void Start()
        {
            BuildMap();
            EventManager.MapCreatedEvent(_map);
        }

        private void BuildMap()
        {
            _map = InitializeArrayMap();
            InstantiateBlocksByArray(_map, WallsParent);
        }

        private int[,] InitializeArrayMap()
        {
            var arrayBuilder = new MapArrayGenerator();
            var array = arrayBuilder.BuildArray(XSize, ZSize, 2, 3, 5, 0.7f, 0.2f);

            return array;
        }

        private void InstantiateBlocksByArray(int[,] array, Transform parent)
        {
            var wI = new WallsInstantiator(wallGo);
            wI.InstantiateWalls(array, -1, parent, false);
        }
    }
}
