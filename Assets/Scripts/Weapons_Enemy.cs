using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Weapons_Enemy : MonoBehaviour
{
    private int HP = 100;
    public static bool Hit_Detected = false;
    public GameObject Player;
    public TMP_Text HP_Text;
    // Start is called before the first frame update
    void Start()
    {
        HP_Text.text = "100";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Sword" && Weapons.is_attacking == true && Hit_Detected == false)
        {
            Hit_Detected = true;
            HP -= 20;
            HP_Text.text = HP + "";
            if (HP <= 0)
            {
                Destroy(HP_Text.gameObject);
                Destroy(this.gameObject);
            }
        }
    }
}
