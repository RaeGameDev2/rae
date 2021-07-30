using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapons_Handler : MonoBehaviour
{
    public enum Weapons
    {
        SWORD = 1,
        SCYTHE = 2,
        SPEAR = 3,
        ANCIENT_STAFF = 4,
        BASIC_STAFF = 5
    }
    public GameObject Player;
    public GameObject Sword;
    public GameObject Scythe;
    public GameObject Spear;
    public GameObject Ancient_Staff;
    public GameObject Basic_Staff;
    public static bool melee_weapon_equiped = true;
    public static int current_melee_weapon = 1;
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
        if (Input.GetKey("left") && is_attacking == false && direction == "left")
        {
            is_attacking = true;
            melee_atack_direction = "left";
        } 
        else if (Input.GetKey("right") && is_attacking == false && direction == "right")
        {
            is_attacking = true;
            melee_atack_direction = "right";
        } 
        else if (Input.GetKey("up") && is_attacking == false)
        {
            is_attacking = true;
            melee_atack_direction = "up";
        } 
    }
    void weapon_switch()
    {
        if(current_melee_weapon == 5)
        {
            current_melee_weapon = 1;
        }
        else
        {
            current_melee_weapon++;
        }
        if(current_melee_weapon == (int) Weapons.SWORD)
        {
            Sword.gameObject.SetActive(true);
            Scythe.gameObject.SetActive(false);
            Spear.gameObject.SetActive(false);
            Ancient_Staff.gameObject.SetActive(false);
            Basic_Staff.gameObject.SetActive(false);
        }
        else if(current_melee_weapon == (int) Weapons.SCYTHE)
        {
            Sword.gameObject.SetActive(false);
            Scythe.gameObject.SetActive(true);
            Spear.gameObject.SetActive(false);
            Ancient_Staff.gameObject.SetActive(false);
            Basic_Staff.gameObject.SetActive(false);
        }
        else if(current_melee_weapon == (int) Weapons.SPEAR)
        {
            Sword.gameObject.SetActive(false);
            Scythe.gameObject.SetActive(false);
            Spear.gameObject.SetActive(true);
            Ancient_Staff.gameObject.SetActive(false);
            Basic_Staff.gameObject.SetActive(false);
        }
        else if(current_melee_weapon == (int) Weapons.ANCIENT_STAFF)
        {
            Sword.gameObject.SetActive(false);
            Scythe.gameObject.SetActive(false);
            Spear.gameObject.SetActive(false);
            Ancient_Staff.gameObject.SetActive(true);
            Basic_Staff.gameObject.SetActive(false);
        }
        else if(current_melee_weapon == (int) Weapons.BASIC_STAFF)
        {
            Sword.gameObject.SetActive(false);
            Scythe.gameObject.SetActive(false);
            Spear.gameObject.SetActive(false);
            Ancient_Staff.gameObject.SetActive(false);
            Basic_Staff.gameObject.SetActive(true);
        }
    }
}

