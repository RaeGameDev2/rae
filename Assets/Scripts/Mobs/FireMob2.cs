using UnityEngine;

public class FireMob2 : Enemy
{
    private enum AttackType
    {
        Idle,
        Attack,
        Damage,
        Death
    }

    private enum Direction
    {
        Left,
        Right
    }
    [SerializeField] private Direction patrolDirection;
    [SerializeField] private float patrolRange;
    [SerializeField] private float attackDistance;
    private bool isAttacking = false;
    private bool isGrounded;
    private bool canDamage;
    private Rigidbody2D rb;
    private Animator anim;

    private new void Start()
    {
        base.Start();
        attackCooldown = 1f;
        transform.localScale = new Vector3(patrolDirection == Direction.Left ? transform.localScale.x : -transform.localScale.x, transform.localScale.y, transform.localScale.z);

        isGrounded = true;
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        canDamage = false;
        anim = GetComponent<Animator>();
    }

    private new void Update()
    {
        if (hp <= 0)
        {
            anim.SetInteger("state", (int)AttackType.Death);
            return;
        }
        base.Update();
        anim.SetFloat("speed", speed / 7);
        anim.SetFloat("attackSpeed", attackSpeed / 2);

        if (anim.GetInteger("state") == (int)AttackType.Damage) return;
        if (anim.GetInteger("state") == (int)AttackType.Attack) return;
        if (GetDistanceFromPlayer() <= attackDistance && !playerSpells.phaseWalkActive && timeSinceAttack <= 0 && !isAttacking)
        {
            anim.SetInteger("state", (int)AttackType.Attack);
            if (player.transform.position.x < transform.position.x && patrolDirection == Direction.Right)
            {
                transform.localScale =
                    new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                patrolDirection = Direction.Left;
            }
            else if (player.transform.position.x > transform.position.x && patrolDirection == Direction.Left)
            {
                transform.localScale =
                    new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                patrolDirection = Direction.Right;
            }

            rb.AddForce(10f * Vector2.up, ForceMode2D.Impulse);
            isAttacking = true;
            canDamage = true;
        }
        else
        {
            if (!canDamage)
            {
                Patrol();
                timeSinceAttack -= Time.deltaTime;
                if (timeSinceAttack < 0)
                    timeSinceAttack = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        if (isGrounded)
        {
            rb.gravityScale = 0;
            if (rb.velocity.y < 0)
                rb.velocity = Vector2.zero;
        }
        else
        {
            rb.gravityScale = 1;
        }

        if (isAttacking)
        {
            var direction = player.transform.position - transform.position;
            direction = new Vector3(direction.x, 0f).normalized;
            rb.AddForce(5f * direction, ForceMode2D.Impulse);
            isAttacking = false;
            timeSinceAttack = attackCooldown;
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Ground"))
        {
            isGrounded = true;
            canDamage = false;
            timeSinceAttack = attackCooldown;
        }
        if (col.CompareTag("Player") && canDamage)
        {
            playerResources.TakeDamage(1, transform.position);
            canDamage = false;
        }

    }

    public void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void Patrol()
    {
        transform.position += (patrolDirection == Direction.Right ? Vector3.right : Vector3.left) * speed * Time.deltaTime;

        if (Mathf.Abs(transform.position.x - spawnPosition.x) >= patrolRange)
        {
            patrolDirection = 1 - patrolDirection;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    public override void OnDamageTaken(float damage, bool isCritical)
    {
        base.OnDamageTaken(damage, isCritical);
        anim.SetInteger("state", (int)AttackType.Damage);
    }

    public void OnDamageTakenEnd()
    {
        anim.SetInteger("state", (int)AttackType.Idle);
    }

    public void OnAttackEnd()
    {
        anim.SetInteger("state", (int)AttackType.Idle);
        timeSinceAttack = attackCooldown;
        spawnPosition = transform.position;
    }
}