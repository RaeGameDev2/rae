using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lamp : MonoBehaviour
{
    public Image[] light;
    public HealthBar healthbar;

    private void Start()
    {
       
    }

    private void Update()
    {
        DisplayLight();
        
    }



    void DisplayLight()
    {
        if (healthbar.health == 0)
        {

            for (int i = 0; i < light.Length; i++)
            {
                light[i].enabled = false;
            }
        }
        
    }

    
}
