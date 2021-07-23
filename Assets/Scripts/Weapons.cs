using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapons : MonoBehaviour
{
    //public GameObject Player;
    public ParticleSystem expl;
    public ParticleSystem expl1;
    public ParticleSystem expl2;
    public GameObject Sword;
    public GameObject Bow;
    public Text Text;
    public GameObject Player;
    public static bool melee_weapon_equiped = true;
    public static bool range_weapon_equiped = false;
    // Start is called before the first frame update
    void Start()
    {
        Sword.gameObject.SetActive(true);
        Bow.gameObject.SetActive(false);
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if(melee_weapon_equiped == true)
            {
                expl1 = Instantiate(expl, new Vector3(0,0,0), Quaternion.identity);
                expl2 = Instantiate(expl, new Vector3(0, 0, 0), Quaternion.identity);
                Text.text = "Bow equipped!";
                melee_weapon_equiped = false;
                range_weapon_equiped = true;
                Sword.gameObject.SetActive(false);
                Bow.gameObject.SetActive(true);
                Destroy(expl1, expl1.duration + expl1.startLifetime);
                Destroy(expl2, expl2.duration + expl2.startLifetime);
            }
            else if(range_weapon_equiped == true)
            {
                expl1 = Instantiate(expl, new Vector3(0, 0, 0), Quaternion.identity);
                expl2 = Instantiate(expl, new Vector3(0, 0, 0), Quaternion.identity);
                Text.text = "Sword equipped!";
                melee_weapon_equiped = true;
                range_weapon_equiped = false;
                Sword.gameObject.SetActive(true);
                Bow.gameObject.SetActive(false);
                Destroy(expl1, expl1.duration + expl1.startLifetime);
                Destroy(expl2, expl2.duration + expl2.startLifetime);
            }
        } 
    }
}
