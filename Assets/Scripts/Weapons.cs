using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
<<<<<<< HEAD
    //public GameObject Player;
    public ParticleSystem expl;
    public ParticleSystem expl1;
    public ParticleSystem expl2;
    public GameObject Sword;
    public GameObject Bow;
    public Text Text;
=======
    public GameObject Player;
>>>>>>> e17ca19cfe05f9f6b40e585710cdbb8cc4805572
    public static bool melee_weapon_equiped = true;
    public static bool range_weapon_equiped = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
<<<<<<< HEAD
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
=======
>>>>>>> e17ca19cfe05f9f6b40e585710cdbb8cc4805572
        
    }
}
