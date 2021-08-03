using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapons_Handler : MonoBehaviour
{
    public enum Weapons
    {
        SWORD,
        SCYTHE,
        SPEAR,
        ANCIENT_STAFF,
        BASIC_STAFF
    }

    public enum Directions
    {
        LEFT,
        RIGHT,
        UP
    }
    public GameObject Player;
    public GameObject Sword;
    public GameObject Scythe;
    public GameObject Spear;
    public GameObject Ancient_Staff;
    public GameObject Basic_Staff;
    public static bool melee_weapon_equiped = true;
    public static Weapons current_melee_weapon = Weapons.SWORD;
    public static bool range_weapon_equiped = false;
    public static Directions direction = Directions.RIGHT;
    public static int weapon_rotate = 1;
    public static bool is_attacking = false;
    public static Directions melee_atack_direction;


    // Start is called before the first frame update
    void Start()
    {
        Sword.gameObject.SetActive(true);
        Scythe.gameObject.SetActive(false);
        Spear.gameObject.SetActive(false);
        Ancient_Staff.gameObject.SetActive(false);
        Basic_Staff.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.T) || Input.GetAxis("Mouse ScrollWheel") > 0f || (Input.GetAxis("Mouse ScrollWheel") < 0f)) && is_attacking == false)
        {
            weapon_switch();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (direction == Directions.LEFT)
                weapon_rotate = 0;
            direction = Directions.RIGHT;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (direction == Directions.RIGHT)
                weapon_rotate = 0;
            direction = Directions.LEFT;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || (Input.GetKey(KeyCode.RightArrow)) || Input.GetKey(KeyCode.UpArrow))
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                melee_atack_direction = Directions.LEFT;

            else if (Input.GetKey(KeyCode.RightArrow))
                melee_atack_direction = Directions.RIGHT;
            else if (Input.GetKey(KeyCode.UpArrow))
                melee_atack_direction = Directions.UP;


            if ((direction == melee_atack_direction || melee_atack_direction == Directions.UP) && is_attacking == false)
                is_attacking = true;
        }
    }
    void weapon_switch()
    {
        switch (current_melee_weapon)
        {
            case Weapons.SWORD:
                current_melee_weapon = Weapons.SCYTHE;
                EquipWeapon(Weapons.SCYTHE);
                break;

            case Weapons.SCYTHE:
                current_melee_weapon = Weapons.SPEAR;
                EquipWeapon(Weapons.SPEAR);
                break;

            case Weapons.SPEAR:
                current_melee_weapon = Weapons.ANCIENT_STAFF;
                EquipWeapon(Weapons.ANCIENT_STAFF);
                break;

            case Weapons.ANCIENT_STAFF:
                current_melee_weapon = Weapons.BASIC_STAFF;
                EquipWeapon(Weapons.BASIC_STAFF);
                break;

            case Weapons.BASIC_STAFF:
                current_melee_weapon = Weapons.SWORD;
                EquipWeapon(Weapons.SWORD);
                break;
        }
    }

    void EquipWeapon(Weapons Weapon)
    {
       if(Weapon == Weapons.SWORD)
       {
            Sword.gameObject.SetActive(true);
            Scythe.gameObject.SetActive(false);
            Spear.gameObject.SetActive(false);
            Ancient_Staff.gameObject.SetActive(false);
            Basic_Staff.gameObject.SetActive(false);
       }
       else if(Weapon == Weapons.SCYTHE)
       {
            Sword.gameObject.SetActive(false);
            Scythe.gameObject.SetActive(true);
            Spear.gameObject.SetActive(false);
            Ancient_Staff.gameObject.SetActive(false);
            Basic_Staff.gameObject.SetActive(false);
        }
        else if (Weapon == Weapons.SPEAR)
        {
            Sword.gameObject.SetActive(false);
            Scythe.gameObject.SetActive(false);
            Spear.gameObject.SetActive(true);
            Ancient_Staff.gameObject.SetActive(false);
            Basic_Staff.gameObject.SetActive(false);
        }
        else if (Weapon == Weapons.BASIC_STAFF)
        {
            Sword.gameObject.SetActive(false);
            Scythe.gameObject.SetActive(false);
            Spear.gameObject.SetActive(false);
            Ancient_Staff.gameObject.SetActive(false);
            Basic_Staff.gameObject.SetActive(true);
        }
        else if (Weapon == Weapons.ANCIENT_STAFF)
        {
            Sword.gameObject.SetActive(false);
            Scythe.gameObject.SetActive(false);
            Spear.gameObject.SetActive(false);
            Ancient_Staff.gameObject.SetActive(true);
            Basic_Staff.gameObject.SetActive(false);
        }
        /* Weapon.gameObject.SetActive(true);
        Debug.Log(Weapon.name);

        foreach (Transform t in Player.transform)
        {
            if (!Weapon.name.Equals(t.name))
            {
                Debug.Log(t.name);
                t.gameObject.SetActive(false);
            }
        }*/
    }
}

