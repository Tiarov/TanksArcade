using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using Assets.Scripts.Helper.ObjectPooling;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] prefabs;
    [SerializeField]
    private int maxCount = 10;
    [SerializeField, Range(0.1f, 0.8f)]
    private float distanceFromTargetRelativeMap;
    [SerializeField]
    private float timeout;

    private int[,] _array;
    private Transform _target;
    private List<GameObject> elements;
    public bool _generatedNow = true;

    private void Awake()
    {
        foreach (var prefab in prefabs)
        {
            PoolManager.Instance.CreatePool(prefab, maxCount);
        }

        elements = new List<GameObject>(maxCount);

        AddEventListeners();
    }

    private void OnDestroy()
    {
        RemoveEventListeners();
    }

    private GameObject GenerateElement(Vector3 position)
    {
        return PoolManager.Instance.ReuseObject(prefabs[Random.Range(0, prefabs.Length)], position,
            Quaternion.identity);
    }

    private Vector3 ResolveCoordinates()
    {
        var k = _array.GetLength(0) > _array.GetLength(1) ? _array.GetLength(0) : _array.GetLength(1);
        var distance = k * distanceFromTargetRelativeMap;
        int x;
        int y;
        do
        {
            x = Random.Range(0, _array.GetLength(0));
            y = Random.Range(0, _array.GetLength(1));
        } while (!IsAllowableCoordinates(x, y, distance));

        return new Vector3(x, transform.position.y, y);
    }

    private bool IsAllowableCoordinates(int x, int z, float distance)
    {
        if (elements.Count > 0)
            foreach (var e in elements)
            {
                if (e.transform.position.x == x && e.transform.position.z == z)
                    return false;
            }

        if (_array[x, z] != 0)
            return false;

        return _target == null ||
               Vector3.Distance(_target.transform.position, new Vector3(x, transform.position.y, z)) > distance;
    }

    private bool IsFreeSpaceInList()
    {
        elements = elements.Where(e => e.transform != null && e.activeInHierarchy).ToList();

        if (elements.Count == 0 || elements.Count < maxCount)
            return true;

        return false;
    }

    private IEnumerator SpawnCircle()
    {
        while (_generatedNow)
        {
            yield return new WaitForSeconds(Random.Range(0.6f * timeout, 1.1f * timeout));
            EventManager.MonstersTargetRequest();
            if (IsFreeSpaceInList())
                elements.Add(GenerateElement(ResolveCoordinates()));
        }
    }

    private void AddEventListeners()
    {
        EventManager.MapCreatedEvent += OnMapCreated;
        EventManager.PlayerCharacterCreatedEvent += OnPlayerCharacterCreated;
        EventManager.PublicityOfTargetEvent += OnPlayerCharacterCreated;
    }

    private void RemoveEventListeners()
    {
        EventManager.MapCreatedEvent -= OnMapCreated;
        EventManager.PlayerCharacterCreatedEvent -= OnPlayerCharacterCreated;
        EventManager.PublicityOfTargetEvent -= OnPlayerCharacterCreated;
    }

    private void OnMapCreated(int[,] array)
    {
        _array = array;
        StartCoroutine(SpawnCircle());
    }

    private void OnPlayerCharacterCreated(Transform target)
    {
        _target = target;
    }
}
