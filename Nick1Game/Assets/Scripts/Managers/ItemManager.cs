using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private enum weaponTypes
    {
        Primary,
        Secondary,
        Melee
    };

    public Gun primaryWeapon;
    public Gun secondaryWeapon;
    public Gun meleeWeapon;

    private weaponTypes equippedWeaponType;
    private WeaponRenderer weaponRenderer;

    public int rifleAmmo;
    public int shotgunAmmo;
    public int pistolAmmo;

    private int maxRilfeAmmo;
    private int maxShotgunAmmo;
    private int maxPistolAmmo;

    //Add more per weapon type

    public Gun[] otherGuns;

    public void Awake()
    {
        weaponRenderer = gameObject.GetComponent<WeaponRenderer>();
    }

    public void RenderEquippedWeapon() {
        weaponRenderer.DrawWeapon();
    }

    public void EquipPrimary() {
        equippedWeaponType = weaponTypes.Primary;
    }

    public void EquipSecondary() {
        equippedWeaponType = weaponTypes.Secondary;
    }

    public void EquipMelee() {
        equippedWeaponType = weaponTypes.Melee;
    }

    public bool AddAmmo(int amount, Gun.categories ammoType) {
        //Will update the primary weapon's ammo whenever called because the amount shown to the shotgun only changes when you add
        if (primaryWeapon.CanPickupAmmo(ammoType)) {
            primaryWeapon.AddAmmo(amount);
            return true;
        } else if (secondaryWeapon.CanPickupAmmo(ammoType)) {
            secondaryWeapon.AddAmmo(amount);
            return true;
        }
        else {
            return false;
        }
    }
    public int GetAmmo(Gun.categories ammoType) {
        //Checks primary weapon's total ammo to update the corresponding weapon ammo
        bool usingWeapon = (primaryWeapon.weaponCategory == ammoType) || (secondaryWeapon.weaponCategory == ammoType);
        if (ammoType == Gun.categories.Shotgun) {
            if (usingWeapon)
                shotgunAmmo = primaryWeapon.totalAmmo;
            return shotgunAmmo;
        } else if (ammoType == Gun.categories.Rifle) {
            if (usingWeapon)
                rifleAmmo = primaryWeapon.totalAmmo;
            return rifleAmmo;
        } else if (ammoType == Gun.categories.Pistol) {
            if (usingWeapon)
                pistolAmmo = secondaryWeapon.totalAmmo;
            return pistolAmmo;
        } else {
            return -1;
        }
    }
}
