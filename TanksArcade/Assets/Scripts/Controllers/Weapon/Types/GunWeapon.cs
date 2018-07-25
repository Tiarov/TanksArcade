using System;
using Assets.Scripts.Helper.ObjectPooling;
using UnityEngine;

namespace Assets.Scripts.Controllers.Weapon
{
    public class GunWeapon : AWeaponController
    {
        [SerializeField]
        protected GameObject Shell;
        [SerializeField]
        protected int poolCount = 30;
        [SerializeField]
        private Transform Place;

        private Transform _owner;

        public bool TryAssign(Transform owner)
        {
            if (owner.gameObject.activeInHierarchy)
                return false;

            _owner = owner;
            return true;
        }

        protected override void OnStarting()
        {
            ResolveShell();
            CheckPool();
        }

        protected override void OnEnabling()
        {
        }

        protected override void OnDisabling()
        {
        }

        protected override void Damaging()
        {
            ZepuskSnarhyada(Place);
        }

        protected void ZepuskSnarhyada(Transform place)
        {
            PoolManager.Instance.ReuseObject(Shell, place.position, place.rotation);
        }

        private void CheckPool()
        {
            var testO = PoolManager.Instance.ReuseObject(Shell, Vector3.zero, Quaternion.identity);
            if (!testO)
            {
                PoolManager.Instance.CreatePool(Shell, poolCount);
            }
            else
            {
                testO.SetActive(false);
            }
        }

        private void ResolveShell()
        {
            if (Shell.transform != null && transform.parent == transform)
                return;

            Shell = Instantiate(Shell, transform.position, Quaternion.identity, transform);
            Shell.SetActive(false);
            var sc = Shell.GetComponent<ShellController>();
            sc.Init(Config);
        }
    }
}
