using UnityEngine;

public class BossLegs : MonoBehaviour
{
    private Resources playerResources;
    
    private IceFinalBoss parent;

    bool isAttacking;
    [SerializeField] private float timeNextAttack;

    private void Start()
    {
        parent = FindObjectOfType<IceFinalBoss>();
        playerResources = FindObjectOfType<Resources>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Debug.Log("Colider");
        if (collision.tag != "Player") return;
        if (isAttacking) return;

        // Debug.Log("Attack");
        isAttacking = true;
        
        if (parent.animType != IceFinalBoss.AnimType.Attack) return;
        // Debug.Log("animType");
        if (Time.time < timeNextAttack) return;
        // Debug.Log("damage");

        playerResources.TakeDamage(1, transform.position);
        timeNextAttack = Time.time + 1.5f;
    }
}
