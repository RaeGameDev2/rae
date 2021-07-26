using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapons : MonoBehaviour
{
    public GameObject Player;
    public GameObject Sword;
    public GameObject Bow;
    public static bool melee_weapon_equiped = true;
    public static bool range_weapon_equiped = false;
    public static bool is_attacking = false;
    private int attack_timer = 300;
    private string melee_atack_direction;
    public static float melee_damage = 20;
    private string direction = "right";
    private int object_rotated = 1;
    void weapon_switch()
    {
        if (melee_weapon_equiped == true)
        {
            melee_weapon_equiped = false;
            range_weapon_equiped = true;
            Sword.gameObject.SetActive(false);
            Bow.gameObject.SetActive(true);
        }
        else if (range_weapon_equiped == true)
        {
            melee_weapon_equiped = true;
            range_weapon_equiped = false;
            Sword.gameObject.SetActive(true);
            Bow.gameObject.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Sword.gameObject.SetActive(true);
        Bow.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if(is_attacking == true)
        {
            attack_timer--;
            Debug.Log(attack_timer);
            if(attack_timer <= 0)
            {
                is_attacking = false;
                attack_timer = 300;
                Weapons_Enemy.Hit_Detected = false;
            }
        }
        if(melee_weapon_equiped == true)
        {
            if (is_attacking == false)
            {
                if (direction == "right")
                {
                    Sword.transform.position = new Vector2(Player.transform.position.x - 3, Player.transform.position.y + 1);
                    // if(object_rotated == 0)
                    // {
                    //     //Sword.transform.Rotate(new Vector3(0, 0, -60));
                    //     object_rotated = 1;
                    // }
                }
                if (direction == "left")
                {
                    Sword.transform.position = new Vector2(Player.transform.position.x + 3, Player.transform.position.y + 1);
                    // if (object_rotated == 0)
                    // {
                    //     //Sword.transform.Rotate(new Vector3(0, 0, 60));
                    //     object_rotated = 1;
                    // }
                }
            }
            else
            {
                if (melee_atack_direction == "left")
                    Sword.transform.position = new Vector2(Player.transform.position.x - 3, Player.transform.position.y + 1);
                if (melee_atack_direction == "right")
                    Sword.transform.position = new Vector2(Player.transform.position.x + 3, Player.transform.position.y + 1);
                if (melee_atack_direction == "up")
                    Sword.transform.position = new Vector2(Player.transform.position.x, Player.transform.position.y + 4);
            }
        }
        else if (range_weapon_equiped == true && is_attacking == false)
        {
            if (is_attacking == false)
                Bow.transform.position = new Vector2(Player.transform.position.x - 3, Player.transform.position.y + 1);
        }

        if (Input.GetKeyDown(KeyCode.T) && is_attacking == false)
        {
            weapon_switch();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (direction == "left")
                object_rotated = 0;
            direction = "right";
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if(direction == "right")
                object_rotated = 0;
            direction = "left";
        }
        if (Input.GetKey("left") && is_attacking == false && direction == "left")
        {
            Sword.transform.position = new Vector2(Player.transform.position.x - 3, Player.transform.position.y + 1);
            is_attacking = true;
            melee_atack_direction = "left";
        }
        if (Input.GetKey("right") && is_attacking == false && direction == "right")
        {
            Sword.transform.position = new Vector2(Player.transform.position.x + 3, Player.transform.position.y + 1);
            is_attacking = true;
            melee_atack_direction = "right";
        }
        if (Input.GetKey("up") && is_attacking == false)
        {
            Sword.transform.position = new Vector2(Player.transform.position.x, Player.transform.position.y + 3);
            is_attacking = true;
            melee_atack_direction = "up";
        }
    }
}
