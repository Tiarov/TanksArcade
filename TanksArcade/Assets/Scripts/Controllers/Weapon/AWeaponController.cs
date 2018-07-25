using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Controllers.Weapon
{
    public abstract class AWeaponController : MonoBehaviour
    {
        [SerializeField]
        protected WeaponConfig Config;

        protected float Damage;
        protected float Culdown;
        protected string[] GettingDamageTags;
        private bool _isActive = true;

        private void Start()
        {
            OnStarting();
        }

        private void OnEnable()
        {
            Damage = Config.Damage;
            Culdown = Config.Culdown;
            GettingDamageTags = Config.GettingDamageTags;
            StartCoroutine(DeactivateWeapon(Culdown));

            OnEnabling();
        }

        private void OnDisable()
        {
            OnDisabling();
        }

        public bool TryUseWeapon()
        {
            if (!_isActive)
                return false;

            StartCoroutine(DeactivateWeapon(Culdown));
            Damaging();

            return true;
        }

        protected abstract void OnEnabling();

        protected abstract void OnDisabling();

        protected abstract void OnStarting();

        protected abstract void Damaging();

        private IEnumerator DeactivateWeapon(float time)
        {
            _isActive = false;
            yield return new WaitForSeconds(time);
            _isActive = true;
        }
    }
}
