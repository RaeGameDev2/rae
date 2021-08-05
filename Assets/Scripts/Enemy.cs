using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float hp = 100f;  // poate fi modificat din inspector dupa nevoie

    public void OnDamageTaken(float damage)
    {
        Debug.Log("scade " + damage);
        hp -= damage;
        if (hp <= 0)
            Destroy(this.gameObject);
    }
}
