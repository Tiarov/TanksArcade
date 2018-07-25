using System.Collections;
using Assets.Scripts.Controllers.Weapon;
using UnityEngine;

public class ShellController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody Rigidbody;
    [SerializeField]
    private Collider Collider;
    [SerializeField]
    private GameObject ShellBody;
    [SerializeField]
    private GameObject Explosion;
    [SerializeField]
    private float Speed = 2;

    private readonly float _lifetime = 16f;
    private bool _booming;
    
    [SerializeField]
    private WeaponConfig _config;

    private void OnEnable()
    {
        if (Explosion)
            Explosion.SetActive(false);
        _booming = false;
        Collider.enabled = false;
        ShellBody.SetActive(true);

        StartCoroutine(AdjornetDisabling(_lifetime / Speed));

        Rigidbody.velocity = Vector3.zero;
        Rigidbody.AddForce(transform.forward * Speed * 30);
    }

    private void OnDisable()
    {
        Collider.enabled = false;
        StopAllCoroutines();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_config.GettingDamageTags == null)
            return;

        foreach (var _tag in _config.GettingDamageTags)
        {
            if (other.gameObject.tag == _tag)
            {
                EventManager.Damage(other.transform, _config.Damage);
                return;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        StopAllCoroutines();

        StartCoroutine(Boom());
    }

    public void Init(WeaponConfig config)
    {
        _config = config;
    }

    private IEnumerator Boom()
    {
        _booming = true;
        if (Explosion)
            Explosion.SetActive(true);

        ShellBody.SetActive(false);
        Collider.enabled = true;
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
    }

    private IEnumerator AdjornetDisabling(float time)
    {
        yield return new WaitForSeconds(time);
        if (!_booming)
            gameObject.SetActive(false);
    }

}
