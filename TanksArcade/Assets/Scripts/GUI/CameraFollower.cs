using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform target;
    public bool move;
    public float dump = 1f;

    [SerializeField]
    private Vector3 _offsetVector;

    private void FixedUpdate()
    {
        if (move && target)
            Move();
    }

    private void Move()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + _offsetVector, dump * Time.fixedDeltaTime);
    }
}
