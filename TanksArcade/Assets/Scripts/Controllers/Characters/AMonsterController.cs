using UnityEngine;
using UnityEngine.AI;

public abstract class AMonsterController : ACharacterController
{
    [SerializeField]
    private NavMeshAgent _agent;

    public virtual void GoToTarget(Transform target)
    {
        if (target)
            _agent.SetDestination(target.position);
    }

    public virtual void Atak(Transform target)
    {
        _transform.LookAt(target.position);
    }

    protected override void OnEnabling()
    {
        _agent.speed = _speed;
        _agent.angularSpeed = RotationSpeed;
    }
}
