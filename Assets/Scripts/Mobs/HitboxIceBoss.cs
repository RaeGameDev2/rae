using UnityEngine;

public class HitboxIceBoss : MonoBehaviour
{
    private IceBoss parent;
    private PlayerResources playerResources;
    [SerializeField] private float timeNextAttack;

    private void Start()
    {
        parent = FindObjectOfType<IceBoss>();
        playerResources = FindObjectOfType<PlayerResources>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;

        if (parent.animType != IceBoss.AnimType.Attack) return;
        if (Time.time < timeNextAttack) return;

        playerResources.TakeDamage(1, transform.position);
        timeNextAttack = Time.time + 1.5f;
    }
}