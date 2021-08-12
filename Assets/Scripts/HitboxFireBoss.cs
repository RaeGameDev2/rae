using UnityEngine;

public class HitboxFireBoss : MonoBehaviour
{
    private Resources playerResources;

    private FireBoss parent;

    bool isAttacking;
    [SerializeField] private float timeNextAttack;
    [SerializeField] private FireBoss.AnimType type;

    private void Start()
    {
        isAttacking = false;
        parent = FindObjectOfType<FireBoss>();
        playerResources = FindObjectOfType<Resources>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Debug.Log(collision.tag);
        if (collision.tag != "Player") return;
        
        // if (isAttacking) return;

        // Debug.Log("Attack");
        // isAttacking = true;

        if (parent.animType != type) return;
        // Debug.Log("animType");
        if (Time.time < timeNextAttack) return;
        // Debug.Log("damage");

        playerResources.TakeDamage(1, transform.position);
        timeNextAttack = Time.time + 1.5f;
    }
}