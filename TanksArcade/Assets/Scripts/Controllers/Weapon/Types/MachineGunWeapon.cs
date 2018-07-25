using Assets.Scripts.Controllers.Weapon;
using UnityEngine;


public class MachineGunWeapon : GunWeapon
{
    [SerializeField]
    protected Transform[] Places;

    protected override void Damaging()
    {
        foreach (var place in Places)
        {
            ZepuskSnarhyada(place);
        }
    }
}
