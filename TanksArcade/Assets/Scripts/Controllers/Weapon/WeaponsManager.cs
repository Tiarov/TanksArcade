using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Controllers.Weapon;
using UnityEngine;

public class WeaponsManager : MonoBehaviour
{
    [SerializeField]
    private List<AWeaponController> _weapons;

    public List<AWeaponController> Weapons
    {
        get { return _weapons; }
    }

    private int _indexOfActive;

    private delegate void StepMethod();

    private void Start()
    {
        if (_weapons == null || _weapons.Count == 0)
            return;

        _weapons[0].gameObject.SetActive(true);
        _indexOfActive = 0;

        for (int i = 1; i < _weapons.Count; i++)
        {
            _weapons[i].gameObject.SetActive(true);
            _weapons[i].gameObject.SetActive(false);
        }
    }

    public bool AddWeapon(GameObject weapon)
    {
        if (_weapons == null)
            _weapons = new List<AWeaponController>();

        foreach (var aWeaponController in _weapons)
        {
            if (aWeaponController.gameObject.GetInstanceID() == weapon.GetInstanceID())
                return false;
        }

        var wController = weapon.GetComponent<AWeaponController>();
        if (!wController)
            return false;

        var newWeapon = weapon.transform == null || weapon.transform.parent != transform ? Instantiate(weapon, transform.position, transform.rotation, transform) : weapon;
        newWeapon.transform.localPosition = Vector3.zero;
        newWeapon.transform.localRotation = Quaternion.identity;

        wController = newWeapon.GetComponent<AWeaponController>();
        _weapons.Add(wController);
        newWeapon.SetActive(true);
        newWeapon.SetActive(false);

        return true;
    }

    public void NextWeapon()
    {
        Switch(Next);
    }

    public void PrewiousWaepon()
    {
        Switch(Previous);
    }

    public void Atak()
    {
        if (_weapons != null || _weapons.Count != 0)
            _weapons[_indexOfActive].TryUseWeapon();
    }

    private void Switch(StepMethod method)
    {
        if (_weapons == null || _weapons.Count == 0)
            return;

        _weapons[_indexOfActive].gameObject.SetActive(false);

        method();

        _weapons[_indexOfActive].gameObject.SetActive(true);
    }

    private void Next()
    {
        if (++_indexOfActive >= _weapons.Count)
            _indexOfActive = 0;
    }

    private void Previous()
    {
        if (--_indexOfActive < 0)
            _indexOfActive = _weapons.Count - 1;
    }
}
