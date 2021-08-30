using UnityEngine;

public class HitboxIceBoss : MonoBehaviour
{
    private IceFinalBoss parent;
    private PlayerResources playerResources;
    [SerializeField] private float timeNextAttack;

    private void Start()
    {
        parent = FindObjectOfType<IceFinalBoss>();
        playerResources = FindObjectOfType<PlayerResources>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;

        if (parent.animType != IceFinalBoss.AnimType.Attack) return;
        if (Time.time < timeNextAttack) return;

        playerResources.TakeDamage(1, transform.position);
        timeNextAttack = Time.time + 1.5f;
    }
}