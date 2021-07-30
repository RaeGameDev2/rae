using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapons_Handler : MonoBehaviour
{
    public GameObject Player;
    public GameObject Sword;

    public GameObject Bow;

    public GameObject Scythe;
    public static bool melee_weapon_equiped = true;
    public static string current_melee_weapon = "Sword";
    public static bool range_weapon_equiped = false;
    public static string direction = "right";
    public static int weapon_rotate = 1;
    public static bool is_attacking = false;
    public static string melee_atack_direction;

    // Start is called before the first frame update
    void Start()
    {
        Sword.gameObject.SetActive(true);
        Bow.gameObject.SetActive(false);
        Scythe.gameObject.SetActive(false);
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
        } else if (Input.GetKey("right") && is_attacking == false && direction == "right")
        {
            is_attacking = true;
            melee_atack_direction = "right";
        } else if (Input.GetKey("up") && is_attacking == false)
        {
            is_attacking = true;
            melee_atack_direction = "up";
        } 
    }
    void weapon_switch()
    {
        if(current_melee_weapon == "Sword")
        {
            current_melee_weapon = "Scythe";
            Sword.gameObject.SetActive(false);
            Scythe.gameObject.SetActive(true);
        }
        else if(current_melee_weapon == "Scythe")
        {
            current_melee_weapon = "Sword";
            Sword.gameObject.SetActive(true);
            Scythe.gameObject.SetActive(false);
        }
    }
}

