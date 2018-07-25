using UnityEngine;

public abstract class ACharacterController : MonoBehaviour
{
    [SerializeField]
    private CharactersConfig Config;
    [SerializeField]
    private HealthController HealthController;
    [SerializeField]
    protected float RotationSpeed = 120;

    protected float _speed;

    protected Transform _transform;
    private Rigidbody _rigidbody;

    private void OnEnable()
    {
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody>();
        _speed = Config.Speed;
        HealthController.Initialize(Config);
        OnEnabling();
    }

    private void OnDisable()
    {
        OnDisabling();
    }

    public virtual void Move(Vector3 vector)
    {
        _rigidbody.velocity = vector * _speed;
    }

    public virtual void Rotate(Vector3 step)
    {
        _transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(step), RotationSpeed * 10 * Time.deltaTime);
    }

    protected abstract void OnEnabling();
    protected abstract void OnDisabling();
}
