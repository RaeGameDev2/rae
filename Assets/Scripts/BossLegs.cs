using UnityEngine;

public class BossLegs : MonoBehaviour
{
    private Resources playerResources;
    
    private IceFinalBoss parent;

    bool isAttacking;

    private void Start()
    {
        parent = FindObjectOfType<IceFinalBoss>();
        playerResources = FindObjectOfType<Resources>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;
        if (isAttacking) return;
        
        isAttacking = true;
        if (parent.simpleAttack)
            playerResources.TakeDamage(1, transform.position);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isAttacking = false;
        }
    }
}
