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
        if(melee_weapon_equiped == true)
        {
            Sword.transform.position = new Vector2(Player.transform.position.x - 3, Player.transform.position.y + 1);
        }
        else
        {
            Bow.transform.position = new Vector2(Player.transform.position.x - 3, Player.transform.position.y + 1);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            weapon_switch();
        } 
    }
}
