using UnityEngine;
using UnityEngine.AI;

public class NavMeshUpdater : MonoBehaviour
{
    [SerializeField]
    private NavMeshSurface surface;

    void Awake()
    {
        AddEventListeners();
    }

    void OnDestroy()
    {

    }

    private void AddEventListeners()
    {
        EventManager.MapCreatedEvent += OnChangingBlocks;
    }

    private void OnChangingBlocks(int[,] array)
    {
        if (surface)
            surface.BuildNavMesh();
    }
}
