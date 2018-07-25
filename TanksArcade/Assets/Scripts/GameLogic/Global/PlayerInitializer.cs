using System.Collections;
using UnityEngine;

public class PlayerInitializer : MonoBehaviour
{
    public GameObject playerPrefab;
    public float Y = 1;

    private GameObject player;

    private void Awake()
    {
        EventManager.MapCreatedEvent += OnMapCreater;
    }

    private void OnDestroy()
    {
        EventManager.MapCreatedEvent -= OnMapCreater;
    }

    private void OnMapCreater(int[,] array)
    {
        player = CreatePlayer(playerPrefab, ResolveCoordinate(array));
        InitCamera(player.transform);

        StartCoroutine(AdjornedEnable(0.5f));
        EventManager.PlayerCharacterCreated(player.transform);
    }

    private Vector3 ResolveCoordinate(int[,] array)
    {
        int x;
        int z;
        do
        {
            x = Random.Range((int)(array.GetLength(0) / 2), array.GetLength(0));
            z = Random.Range(0, array.GetLength(1));
        } while (array[x, z] != 0);

        return new Vector3(x, Y, z);
    }

    private GameObject CreatePlayer(GameObject prefab, Vector3 position)
    {
        return Instantiate(prefab, position, Quaternion.Euler(new Vector3(0, 90, 0)), transform);
    }

    private void InitCamera(Transform target)
    {
        var folower = Camera.main.gameObject.GetComponent<CameraFollower>();
        folower.target = target;
    }

    private IEnumerator AdjornedEnable(float time)
    {
        yield return new WaitForSeconds(time);
        player.SetActive(true);
    }
}
