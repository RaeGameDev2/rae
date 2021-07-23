using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    //public GameObject Player;
    public GameObject Sword;
    public GameObject Bow;
    public static bool melee_weapon_equiped = true;
    public static bool range_weapon_equiped = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if(melee_weapon_equiped == true)
            {
                melee_weapon_equiped = false;
                range_weapon_equiped = true;
                Sword.gameObject.SetActive(false);
                Bow.gameObject.SetActive(true);
            }
            else if(range_weapon_equiped == true)
            {
                melee_weapon_equiped = true;
                range_weapon_equiped = false;
                Sword.gameObject.SetActive(true);
                Bow.gameObject.SetActive(false);
            }
        }
    }
}
