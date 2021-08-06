using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapons_Handler : MonoBehaviour
{

    public enum Weapon
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

    // public bool melee_weapon_equiped = true;
    //public bool range_weapon_equiped = false;
    public Weapon currentWeapon;
    public AttackType attackType;
    public float[] initialDamages;
    [HideInInspector] public float[] damages;

    void Start()
    {
        currentWeapon = Weapon.SCYTHE;
        attackType = AttackType.NONE;
        damages = new float[3];
        damages[0] = initialDamages[0];
        damages[1] = initialDamages[1];
        damages[2] = initialDamages[2];
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.T) || Input.GetAxis("Mouse ScrollWheel") > 0f || (Input.GetAxis("Mouse ScrollWheel") < 0f))
        && attackType == AttackType.NONE)
        {
            SwitchWeapon();
        }
        if (Input.GetMouseButtonDown(0) && attackType == AttackType.NONE)
        {
            attackType = AttackType.BASIC;
        }
        if (Input.GetMouseButtonDown(1) && attackType == AttackType.NONE)
        {
            attackType = AttackType.HEAVY;
        }
    }

    void SwitchWeapon()
    {
        switch (currentWeapon)
        {
            case Weapon.SCYTHE:
                currentWeapon = Weapon.ORB;
                break;

            case Weapon.ORB:
                currentWeapon = Weapon.STAFF;
                break;

            case Weapon.STAFF:
                currentWeapon = Weapon.SCYTHE;
                break;
        }
    }
}

