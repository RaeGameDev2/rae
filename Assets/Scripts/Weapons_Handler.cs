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
    public GameObject Player;
    public GameObject Sword;
    public GameObject Scythe;
    public GameObject Spear;
    public GameObject Ancient_Staff;
    public GameObject Basic_Staff;
    public static bool melee_weapon_equiped = true;
    public static Weapons current_melee_weapon = Weapons.SWORD;
    public static bool range_weapon_equiped = false;
    public static string direction = "right";
    public static int weapon_rotate = 1;
    public static bool is_attacking = false;
    public static string melee_atack_direction;


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
        if ((Input.GetKeyDown(KeyCode.T) || Input.GetAxis("Mouse ScrollWheel") > 0f  || (Input.GetAxis("Mouse ScrollWheel") < 0f ))  && is_attacking == false)
        {
            weapon_switch();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (direction == "left")
                weapon_rotate = 0;
            direction = "right";
        } 
        else if (Input.GetKeyDown(KeyCode.A)) 
        {
            if (direction == "right")
                weapon_rotate = 0;
            direction = "left";
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && is_attacking == false && direction == "left")
        {
            is_attacking = true;
            melee_atack_direction = "left";
        } 
        else if (Input.GetKey(KeyCode.RightArrow) && is_attacking == false && direction == "right")
        {
            is_attacking = true;
            melee_atack_direction = "right";
        } 
        else if (Input.GetKey(KeyCode.UpArrow) && is_attacking == false)
        {
            is_attacking = true;
            melee_atack_direction = "up";
        } 
    }
    void weapon_switch()
    {
        switch (current_melee_weapon)
        {
            case Weapons.SWORD: current_melee_weapon = Weapons.SCYTHE; break;
            case Weapons.SCYTHE: current_melee_weapon = Weapons.SPEAR; break;
            case Weapons.SPEAR: current_melee_weapon = Weapons.ANCIENT_STAFF; break;
            case Weapons.ANCIENT_STAFF: current_melee_weapon = Weapons.BASIC_STAFF; break;
            case Weapons.BASIC_STAFF: current_melee_weapon = Weapons.SWORD; break;
        }
        switch (current_melee_weapon)
        {
            case Weapons.SWORD: Sword.gameObject.SetActive(true);
            Scythe.gameObject.SetActive(false);
            Spear.gameObject.SetActive(false);
            Ancient_Staff.gameObject.SetActive(false);
            Basic_Staff.gameObject.SetActive(false);
            break;

            case Weapons.SCYTHE: Sword.gameObject.SetActive(false);
            Scythe.gameObject.SetActive(true);
            Spear.gameObject.SetActive(false);
            Ancient_Staff.gameObject.SetActive(false);
            Basic_Staff.gameObject.SetActive(false);
            break;

            case Weapons.SPEAR: Sword.gameObject.SetActive(false);
            Scythe.gameObject.SetActive(false);
            Spear.gameObject.SetActive(true);
            Ancient_Staff.gameObject.SetActive(false);
            Basic_Staff.gameObject.SetActive(false);
            break;

            case Weapons.ANCIENT_STAFF: Sword.gameObject.SetActive(false);
            Scythe.gameObject.SetActive(false);
            Spear.gameObject.SetActive(false);
            Ancient_Staff.gameObject.SetActive(true);
            Basic_Staff.gameObject.SetActive(false);
            break;

            case Weapons.BASIC_STAFF: Sword.gameObject.SetActive(false);
            Scythe.gameObject.SetActive(false);
            Spear.gameObject.SetActive(false);
            Ancient_Staff.gameObject.SetActive(false);
            Basic_Staff.gameObject.SetActive(true);
            break;
        }
        // if(current_melee_weapon == (int) Weapons.SWORD)
        // {
        //     Sword.gameObject.SetActive(true);
        //     Scythe.gameObject.SetActive(false);
        //     Spear.gameObject.SetActive(false);
        //     Ancient_Staff.gameObject.SetActive(false);
        //     Basic_Staff.gameObject.SetActive(false);
        // }
        // else if(current_melee_weapon == (int) Weapons.SCYTHE)
        // {
        //     Sword.gameObject.SetActive(false);
        //     Scythe.gameObject.SetActive(true);
        //     Spear.gameObject.SetActive(false);
        //     Ancient_Staff.gameObject.SetActive(false);
        //     Basic_Staff.gameObject.SetActive(false);
        // }
        // else if(current_melee_weapon == (int) Weapons.SPEAR)
        // {
        //     Sword.gameObject.SetActive(false);
        //     Scythe.gameObject.SetActive(false);
        //     Spear.gameObject.SetActive(true);
        //     Ancient_Staff.gameObject.SetActive(false);
        //     Basic_Staff.gameObject.SetActive(false);
        // }
        // else if(current_melee_weapon == (int) Weapons.ANCIENT_STAFF)
        // {
        //     Sword.gameObject.SetActive(false);
        //     Scythe.gameObject.SetActive(false);
        //     Spear.gameObject.SetActive(false);
        //     Ancient_Staff.gameObject.SetActive(true);
        //     Basic_Staff.gameObject.SetActive(false);
        // }
        // else if(current_melee_weapon == (int) Weapons.BASIC_STAFF)
        // {
        //     Sword.gameObject.SetActive(false);
        //     Scythe.gameObject.SetActive(false);
        //     Spear.gameObject.SetActive(false);
        //     Ancient_Staff.gameObject.SetActive(false);
        //     Basic_Staff.gameObject.SetActive(true);
        // }
    }
}

