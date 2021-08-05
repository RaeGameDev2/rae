using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    public Image[] manaPoints;

    float mana, maxMana = 100;


    private void Start()
    {
        mana = maxMana;
    }

    private void Update()
    {

        if (mana > maxMana) mana = maxMana;

        
        ManaBarFiller();

        if (Input.GetKeyDown(KeyCode.S))
        {

            TakeMana(20);



        }

        NormalizeMana();
    }

    void ManaBarFiller()
    {

        for (int i = 0; i < manaPoints.Length; i++)
        {
            manaPoints[i].enabled = !DisplayManaPoint(mana, i);
        }
    }

    bool DisplayManaPoint(float _mana, int pointNumber)
    {
        return ((pointNumber * 20) >= _mana);
    }

    public void TakeMana(float manaPoints)
    {
        if (mana > 0)
            mana -= manaPoints;
    }
    void NormalizeMana()
    {
        if (mana < 100)
        {
            mana += 5f * Time.deltaTime;
        }
        
    }
}
