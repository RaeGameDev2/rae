using UnityEngine;

public class HitboxFireBoss : MonoBehaviour
{
    private bool isAttacking;

    private FireBoss parent;
    private PlayerResources playerResources;
    [SerializeField] private float timeNextAttack;
    [SerializeField] private FireBoss.AnimType type;

    private void Start()
    {
        isAttacking = false;
        parent = FindObjectOfType<FireBoss>();
        playerResources = FindObjectOfType<PlayerResources>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;

        if (parent.animType != type) return;
        if (Time.time < timeNextAttack) return;

        playerResources.TakeDamage(1, transform.position);
        timeNextAttack = Time.time + 1.5f;
    }
}