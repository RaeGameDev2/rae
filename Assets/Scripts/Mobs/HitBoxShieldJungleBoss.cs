using UnityEngine;

public class HitBoxShieldJungleBoss : MonoBehaviour
{
    private JungleBoss parent;
    private PlayerResources playerResources;
    [SerializeField] private float timeNextAttack;

    private void Start()
    {
        parent = FindObjectOfType<JungleBoss>();
        playerResources = FindObjectOfType<PlayerResources>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;

        if (!parent.shieldDamageAnimation) return;
        if (Time.time < timeNextAttack) return;

        playerResources.TakeDamage(1, transform.position);
        timeNextAttack = Time.time + 1.5f;
    }
}
