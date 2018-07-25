using UnityEngine;

namespace Assets.Scripts.Controllers.Weapon.Types
{
    public class Punch : MonoBehaviour
    {
        [SerializeField]
        private WeaponConfig Config;

        private void OnTriggerEnter(Collider other)
        {
            foreach (var _tag in Config.GettingDamageTags)
            {
                if (other.gameObject.tag == _tag)
                {
                    EventManager.Damage(other.transform, Config.Damage);
                    return;
                }
            }
        }
    }
}
