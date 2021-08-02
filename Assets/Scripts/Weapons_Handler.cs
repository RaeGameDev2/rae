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
        RIGHT
    }
    public enum Attack_Directions
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
    public static Attack_Directions melee_atack_direction;


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
        if (Input.GetKeyDown(KeyCode.LeftArrow) && is_attacking == false && direction == Directions.LEFT)
        {
            is_attacking = true;
            melee_atack_direction = Attack_Directions.LEFT;
        } 
        else if (Input.GetKey(KeyCode.RightArrow) && is_attacking == false && direction == Directions.RIGHT)
        {
            is_attacking = true;
            melee_atack_direction = Attack_Directions.RIGHT;
        } 
        else if (Input.GetKey(KeyCode.UpArrow) && is_attacking == false)
        {
            is_attacking = true;
            melee_atack_direction = Attack_Directions.UP;
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
    }
}

