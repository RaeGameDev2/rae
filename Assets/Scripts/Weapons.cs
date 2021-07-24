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
    public int is_attacking = 0;
    public int attack_timer = 700;
    public string melee_atack_direction;
    public int test = 0;
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
        if(is_attacking == 1)
        {
            attack_timer--;
            Debug.Log(attack_timer);
            if(attack_timer <= 0)
            {
                is_attacking = 0;
                attack_timer = 700;
            }
        }
        if(melee_weapon_equiped == true)
        {
            if(is_attacking == 0)
                Sword.transform.position = new Vector2(Player.transform.position.x - 3, Player.transform.position.y + 1);
            else
            {
                if(melee_atack_direction == "right")
                    Sword.transform.position = new Vector2(Player.transform.position.x + 3, Player.transform.position.y + 1);
                if (melee_atack_direction == "up")
                    Sword.transform.position = new Vector2(Player.transform.position.x, Player.transform.position.y + 3);
            }
        }
        else if (range_weapon_equiped == true && is_attacking == 0)
        {
            if (is_attacking == 0)
                Bow.transform.position = new Vector2(Player.transform.position.x - 3, Player.transform.position.y + 1);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            weapon_switch();
        }
        if (Input.GetKey("left"))
        {
            Sword.transform.position = new Vector2(Player.transform.position.x - 3, Player.transform.position.y + 1);
            is_attacking = 1;
            melee_atack_direction = "left";
        }
        if (Input.GetKey("right"))
        {
            Sword.transform.position = new Vector2(Player.transform.position.x + 3, Player.transform.position.y + 1);
            is_attacking = 1;
            melee_atack_direction = "right";
        }
        if (Input.GetKey("up"))
        {
            Sword.transform.position = new Vector2(Player.transform.position.x, Player.transform.position.y + 3);
            is_attacking = 1;
            melee_atack_direction = "up";
        }
    }
}
