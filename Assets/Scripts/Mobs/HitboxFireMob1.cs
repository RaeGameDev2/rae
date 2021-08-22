using UnityEngine;

public class HitboxFireMob1 : MonoBehaviour
{
    [SerializeField] private float attackDelay;
    [SerializeField] private float attackTimer;

    private bool isAttacking;

    private FireMob1 parent;
    private PlayerResources playerResources;

    // Start is called before the first frame update
    private void Start()
    {
        attackDelay = 2f;
        attackTimer = attackDelay;

        playerResources = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerResources>();
        parent = transform.GetComponentInParent<FireMob1>();

        isAttacking = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (isAttacking)
        {
            attackTimer -= Time.deltaTime;

            if (attackTimer <= 0)
            {
                attackTimer = attackDelay;
                isAttacking = false;

                parent.anim.SetInteger("state", (int) FireMob1.AttackType.Idle);
                parent.isAttacking = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        if (collision.tag == "Player")
            if (!isAttacking)
            {
                isAttacking = true;
                Debug.Log("Player attacked");
                if (parent.isAttacking)
                    playerResources.TakeDamage(1, transform.position);
            }
    }
}