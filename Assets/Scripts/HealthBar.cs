using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image[] healthPoints;

    public float health, maxHealth = 100;
    

    private void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        
        if (health > maxHealth) health = maxHealth;


        HealthBarFiller();

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            
                Damage(20);
            


        }
    }

    void HealthBarFiller()
    {

        for (int i = 0; i < healthPoints.Length; i++)
        {
            healthPoints[i].enabled = !DisplayHealthPoint(health, i);
        }
    }

    bool DisplayHealthPoint(float _health, int pointNumber)
    {
        return ((pointNumber * 20) >= _health);
    }

    public void Damage(float damagePoints)
    {
        if (health > 0)
            health -= damagePoints;
    }
    public void Heal(float healingPoints)
    {
        if (health < maxHealth)
            health += healingPoints;
    }
}
