using System;
using UnityEngine;

[Serializable]
public class Weapon
{
    public enum AttackType
    {
        None,
        Basic,
        Heavy
    }

    public enum WeaponType
    {
        Scythe,
        Orb,
        Staff
    }

    public float attackSpeed;

    [HideInInspector] public AttackType attackType;

    public float bonusAttackDmg;
    public float bonusAttackSpeed;
    public float bonusCritDmg;
    public float bonusCritRate;
    public float critDmg;
    public float critRate;
    public float mainDamage;
    public float secondaryDamage;

    public WeaponType type;
}

public class WeaponsHandler : MonoBehaviour
{
    public Weapon currWeapon;
    private bool pause;
    private PlayerController playerController;
    private PlayerSkills playerSkills;
    private PlayerSpells playerSpells;

    public Weapon[] weapons;

    public void Pause()
    {
        pause = !pause;
    }

    private void Start()
    {
        currWeapon = weapons[0];
        playerSpells = FindObjectOfType<PlayerSpells>();
        playerController = FindObjectOfType<PlayerController>();
        playerSkills = FindObjectOfType<PlayerSkills>();
        pause = false;
    }

    private void Update()
    {
        if (pause) return;
        if ((Input.GetKeyDown(KeyCode.T) || Input.GetAxis("Mouse ScrollWheel") != 0f) &&
            currWeapon.attackType == Weapon.AttackType.None) SwitchWeapon();

        if (playerSpells.phaseWalkActive || playerSpells.orbDropped) return;

        if (Input.GetMouseButtonDown(0) && currWeapon.attackType == Weapon.AttackType.None)
        {
            currWeapon.attackType = Weapon.AttackType.Basic;
            // playerController.timeNextAttack = Time.time + currWeapon.attackSpeed + currWeapon.bonusAttackSpeed * playerSkills.GetLevelAttackSpeed();

            SoundManagerScript.playAttackSound = true;
        }

        if (Input.GetMouseButtonDown(1) && currWeapon.attackType == Weapon.AttackType.None)
        {
            currWeapon.attackType = Weapon.AttackType.Heavy;
            // playerController.timeNextAttack = Time.time + 2 * currWeapon.attackSpeed + currWeapon.bonusAttackSpeed * playerSkills.GetLevelAttackSpeed();

            SoundManagerScript.playAttackSound = true;
        }
    }

    private void SwitchWeapon()
    {
        switch (currWeapon.type)
        {
            case Weapon.WeaponType.Scythe:
                currWeapon = weapons[(int) Weapon.WeaponType.Orb];
                break;

            case Weapon.WeaponType.Orb:
                currWeapon = weapons[(int) Weapon.WeaponType.Staff];
                break;

            case Weapon.WeaponType.Staff:
                currWeapon = weapons[(int) Weapon.WeaponType.Scythe];
                break;
        }
    }
}