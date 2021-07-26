using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Weapons_Enemy : MonoBehaviour
{
    private float HP;
    public static bool Hit_Detected = false;
    public GameObject Player;
    public TMP_Text HP_Text;
    // public Transform parent;
    // public GameObject FloatingText;
    // private GameObject newInstance;
    // Start is called before the first frame update
    void Start()
    {
        HP = 100;
        HP_Text.text = "100";
        // newInstance = Instantiate(FloatingText, new Vector3(0,0,0), Quaternion.identity);
        // newInstance.transform.SetParent(parent);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Sword" && Weapons.is_attacking == true && Hit_Detected == false)
        {
            
            Hit_Detected = true;
            HP -= Weapons.melee_damage;
            HP_Text.text = HP + "";
            if (HP <= 0)
            {
                Destroy(HP_Text.gameObject);
                Destroy(this.gameObject);
            }
        }
        else if(collision.tag == "Arrow")
        {
            HP -= Weapons.melee_damage;
            HP_Text.text = HP + "";
            Destroy(collision.gameObject);
            if (HP <= 0)
            {
                Destroy(HP_Text.gameObject);
                Destroy(this.gameObject);
            }
        }
    }
}
