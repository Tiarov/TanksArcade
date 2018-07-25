using UnityEngine;

namespace Assets.Scripts.Controllers.Weapon
{
    [CreateAssetMenu(fileName = "Weapon Config", menuName = "Configs/Weapon Config")]
    public class WeaponConfig : ScriptableObject
    {
        public float Damage;
        public float Culdown;
        public string[] GettingDamageTags;
    }
}
