using UnityEngine;

public class HitboxFireMob1 : MonoBehaviour
{
    private bool isAttacking;

    private FireMob1 parent;
    private PlayerResources playerResources;

    private void Start()
    {
        playerResources = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerResources>();
        parent = transform.GetComponentInParent<FireMob1>();

        isAttacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (parent.isAttacking)
            {
                playerResources.TakeDamage(1, transform.position);
                parent.isAttacking = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (parent.isAttacking)
            {
                playerResources.TakeDamage(1, transform.position);
                parent.isAttacking = false;
            }
        }
    }
}