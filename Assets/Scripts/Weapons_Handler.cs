using UnityEngine;

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
    private bool pause;
    private PlayerSpells playerSpells;
    private PlayerController playerController;
    private PlayerSkills playerSkills;

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
        if ((Input.GetKeyDown(KeyCode.T) || Input.GetAxis("Mouse ScrollWheel") != 0f) && currWeapon.attackType == Weapon.AttackType.NONE)
        {
            SwitchWeapon();
        }

        if (playerSpells.phaseWalkActive || playerSpells.orbDropped) return;

        if (Input.GetMouseButtonDown(0) && currWeapon.attackType == Weapon.AttackType.NONE)
        {
            currWeapon.attackType = Weapon.AttackType.BASIC;
            // playerController.timeNextAttack = Time.time + currWeapon.attackSpeed + currWeapon.bonusAttackSpeed * playerSkills.GetLevelAttackSpeed();

            SoundManagerScript.playAttackSound = true;
        }
        if (Input.GetMouseButtonDown(1) && currWeapon.attackType == Weapon.AttackType.NONE)
        {
            currWeapon.attackType = Weapon.AttackType.HEAVY;
            // playerController.timeNextAttack = Time.time + 2 * currWeapon.attackSpeed + currWeapon.bonusAttackSpeed * playerSkills.GetLevelAttackSpeed();

            SoundManagerScript.playAttackSound = true;
        }
    }

    private void SwitchWeapon()
    {
        switch (currWeapon.type)
        {
            case Weapon.WeaponType.SCYTHE:
                currWeapon = weapons[(int)Weapon.WeaponType.ORB];
                break;

            case Weapon.WeaponType.ORB:
                currWeapon = weapons[(int)Weapon.WeaponType.STAFF];
                break;

            case Weapon.WeaponType.STAFF:
                currWeapon = weapons[(int)Weapon.WeaponType.SCYTHE];
                break;
        }
    }
}

