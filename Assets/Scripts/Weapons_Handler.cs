using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class Weapon
{
    public float mainDamage;
    public float secondaryDamage;
    public float attackSpeed;
    public float critRate;
    public float critDmg;

    public float bonusAttackDmg;
    public float bonusAttackSpeed;
    public float bonusCritRate;
    public float bonusCritDmg;

    public WeaponType type;

    [HideInInspector] public AttackType attackType;
    public enum WeaponType
    {
        SCYTHE,
        ORB,
        STAFF,
    }

    public enum AttackType
    {
        NONE,
        BASIC,
        HEAVY
    }


}

public class Weapons_Handler : MonoBehaviour
{

    public Weapon[] weapons;
    public Weapon currWeapon;



    void Start()
    {
        currWeapon = weapons[0];
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.T) || Input.GetAxis("Mouse ScrollWheel") > 0f || (Input.GetAxis("Mouse ScrollWheel") < 0f))
        && currWeapon.attackType == Weapon.AttackType.NONE)
        {
            SwitchWeapon();
        }
        if (Input.GetMouseButtonDown(0) && currWeapon.attackType == Weapon.AttackType.NONE)
        {
            currWeapon.attackType = Weapon.AttackType.BASIC;
        }
        if (Input.GetMouseButtonDown(1) && currWeapon.attackType == Weapon.AttackType.NONE)
        {
            currWeapon.attackType = Weapon.AttackType.HEAVY;
        }
    }

    void SwitchWeapon()
    {
        switch (currWeapon.type)
        {
            case Weapon.WeaponType.SCYTHE:
                currWeapon.type = Weapon.WeaponType.ORB;
                break;

            case Weapon.WeaponType.ORB:
                currWeapon.type = Weapon.WeaponType.STAFF;
                break;

            case Weapon.WeaponType.STAFF:
                currWeapon.type = Weapon.WeaponType.SCYTHE;
                break;
        }
    }
}

