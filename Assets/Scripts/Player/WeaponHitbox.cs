using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHitbox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.CompareTo("Enemy") == 0)
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().OnAttackHit(col);
    }
}
