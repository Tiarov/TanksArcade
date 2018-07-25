using Assets.Scripts.Controllers.Weapon;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class PlayerController : MonoBehaviour
    {
        private readonly Vector3 _rotationStep = new Vector3(0, 2, 0);

        public UserInputConfig Config;

        [SerializeField]
        private ACharacterController aCharacterController;
        [SerializeField]
        private WeaponsManager WeaponsManager;

        private Vector3 _velocityVector;
        private Quaternion _quaternion;
        private Transform _ownerTransform;
        private bool _pressed;

        private KeyCode _forwardButton;
        private KeyCode _rearButton;
        private KeyCode _leftRotateButton;
        private KeyCode _rightRotateButton;
        private KeyCode _nextWeaponButton;
        private KeyCode _previousWeaponButton;
        private KeyCode _atakButton;

        private void Start()
        {
            _ownerTransform = aCharacterController.transform;
        }

        private void OnEnable()
        {
            _forwardButton = Config.ForwardButton;
            _rearButton = Config.RearButton;
            _leftRotateButton = Config.LeftButton;
            _rightRotateButton = Config.RightButton;

            _nextWeaponButton = Config.NextWeaponButton;
            _previousWeaponButton = Config.PreviousWeaponButton;

            _atakButton = Config.AttakButton;

            foreach (var wc in WeaponsManager.Weapons)
            {
                var gv = wc as GunWeapon;
                if (gv)
                    gv.TryAssign(transform);
            }
        }

        private void FixedUpdate()
        {
            //Move();
        }
        private void Update()
        {
            Rotate();
            Move();
            Atak();
            SwitchWeapon();
        }

        private void Rotate()
        {
            _pressed = false;
            if (Input.GetKey(_leftRotateButton))
            {
                _pressed = !_pressed;
                _quaternion.eulerAngles = _ownerTransform.rotation.eulerAngles - _rotationStep;
            }

            if (Input.GetKey(_rightRotateButton))
            {
                _pressed = !_pressed;
                _quaternion.eulerAngles = _ownerTransform.rotation.eulerAngles + _rotationStep;
            }

            if (_pressed)
                aCharacterController.Rotate(_quaternion.eulerAngles);
        }

        private void Move()
        {
            if (Input.GetKey(_forwardButton))
                aCharacterController.Move(-_ownerTransform.forward);

            if (Input.GetKey(_rearButton))
                aCharacterController.Move(_ownerTransform.forward);
        }

        private void Atak()
        {
            if (Input.GetKey(_atakButton))
                WeaponsManager.Atak();
        }

        private void SwitchWeapon()
        {
            if (Input.GetKeyDown(_nextWeaponButton))
                WeaponsManager.NextWeapon();
            if (Input.GetKeyDown(_previousWeaponButton))
                WeaponsManager.PrewiousWaepon();
        }
    }
}
