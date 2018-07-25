using UnityEngine;

namespace Assets.Scripts.UI
{
    [CreateAssetMenu(fileName = "Player input Config settings", menuName = "Configs/Input settings")]
    public class UserInputConfig : ScriptableObject
    {
        [SerializeField]
        public KeyCode ForwardButton = KeyCode.UpArrow;
        [SerializeField]
        public KeyCode RearButton = KeyCode.DownArrow;
        [SerializeField]
        public KeyCode RightButton = KeyCode.RightArrow;
        [SerializeField]
        public KeyCode LeftButton = KeyCode.LeftArrow;

        [SerializeField]
        public KeyCode PreviousWeaponButton = KeyCode.Q;
        [SerializeField]
        public KeyCode NextWeaponButton = KeyCode.W;
        [SerializeField]
        public KeyCode AttakButton = KeyCode.X;
    }
}
