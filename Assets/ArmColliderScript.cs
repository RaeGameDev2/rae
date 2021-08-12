using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmColliderScript : MonoBehaviour
{
    private Resources playerResources;
    [SerializeField] private float attackDelay;
    [SerializeField] private float attackTimer;

    bool isAttacking;

    // Start is called before the first frame update
    void Start()
    {
        attackDelay = 1f;
        attackTimer = attackDelay;

        playerResources = GameObject.FindGameObjectWithTag("Player").GetComponent<Resources>();

        isAttacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttacking)
        {
            attackTimer -= Time.deltaTime;

            if (attackTimer <= 0)
            {
                attackTimer = attackDelay;
                isAttacking = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        if (collision.tag == "Player")
        {
            if (!isAttacking)
            {
                isAttacking = true;
                Debug.Log("Player attacked");
                playerResources.TakeDamage(1, transform.position);
            }
        }
    }
}
