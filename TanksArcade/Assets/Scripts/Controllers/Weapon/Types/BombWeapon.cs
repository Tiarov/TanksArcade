using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Controllers.Weapon
{
    public class BombWeapon : AWeaponController
    {
        [SerializeField]
        private SphereCollider Collider;
        [SerializeField]
        private float radius = 1.3f;
        [SerializeField]
        private GameObject explosion;

        protected override void OnStarting()
        {

        }

        protected override void OnEnabling()
        {
            explosion.SetActive(false);
            Collider.radius = radius;
            Damaging();
        }

        protected override void OnDisabling()
        {
            explosion.SetActive(false);
        }

        protected override void Damaging()
        {
            Collider.enabled = true;
            explosion.SetActive(true);
        }

        private IEnumerator Explosion()
        {
            Collider.enabled = true;
            explosion.SetActive(true);
            yield return new WaitForFixedUpdate();
            Collider.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            foreach (var _tag in GettingDamageTags)
            {
                if (other.gameObject.tag == _tag)
                {
                    EventManager.Damage(other.transform, Damage);
                    return;
                }
            }
        }
    }
}
