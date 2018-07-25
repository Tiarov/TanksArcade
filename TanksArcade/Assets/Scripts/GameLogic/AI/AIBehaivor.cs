using UnityEngine;

public enum AiState
{
    Idle, Walk, Atak
}

public class AIBehaivor : MonoBehaviour
{
    [SerializeField]
    private AMonsterController _monster;
    [SerializeField]
    private float atakDistance = 1.1f;
    private Transform _monsterTarget;
    [SerializeField]
    private Transform currentTarget;
    private AiState _state;

    private void OnEnable()
    {
        _state = AiState.Idle;
        _monsterTarget = _monster.transform;

        AddEventListeners();
        EventManager.MonstersTargetRequest();
    }

    private void OnDisable()
    {
        RemoveEventListeners();
    }

    private void Update()
    {
        switch (_state)
        {
            case AiState.Idle:
                OnIdle();
                break;
            case AiState.Walk:
                CheckDistance();
                break;
            case AiState.Atak:
                Atak();
                break;
        }
    }

    private bool CheckTarget()
    {
        return currentTarget && currentTarget.gameObject.activeInHierarchy;
    }

    private void OnIdle()
    {
        if (CheckTarget())
            _state = AiState.Atak;
    }

    private void CheckDistance()
    {
        if (!CheckTarget())
            _state = AiState.Idle;

        if (Vector3.Distance(currentTarget.position, _monsterTarget.position) < atakDistance)
            _state = AiState.Atak;
        else
            _monster.GoToTarget(currentTarget);
    }

    private void Atak()
    {
        if (!CheckTarget())
            _state = AiState.Idle;

        if (Vector3.Distance(currentTarget.position, _monsterTarget.position) > atakDistance)
            _state = AiState.Walk;
        else
            _monster.Atak(currentTarget);
    }

    private void AddEventListeners()
    {
        EventManager.PublicityOfTargetEvent += OnPublicityOfTarget;
    }

    private void RemoveEventListeners()
    {
        EventManager.PublicityOfTargetEvent -= OnPublicityOfTarget;
    }

    private void OnPublicityOfTarget(Transform target)
    {
        currentTarget = target;
    }
}
