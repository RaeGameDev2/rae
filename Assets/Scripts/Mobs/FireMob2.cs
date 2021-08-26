using System.Linq;
using UnityEngine;

public class FireMob2 : Enemy
{
    public enum AttackType
    {
        Idle,
        Attack,
        Damage,
        Death
    }

    public enum Direction
    {
        Left,
        Right
    }
    [SerializeField] private Direction patrolDirection;
    [SerializeField] private float patrolRange;
    [SerializeField] private float attackDistance;
    [HideInInspector] public bool isAttacking = false;
    public bool isGrounded;
    public bool canDamage;
    public Rigidbody2D rb;

    private Vector3 spawnPosition;
    private PlayerResources playerResources;

    private new void Start()
    {
        base.Start();

        spawnPosition = transform.position;

        if (patrolDirection == Direction.Left)
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        else
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        isGrounded = true;
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        playerResources = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerResources>();
        canDamage = false;
    }

    private new void Update()
    {
        base.Update();
        // anim.SetFloat("speed", speed / 3);
        // anim.SetFloat("attackSpeed", attackSpeed / 100);

        if (hp <= 0)
        {
            //anim.SetInteger("state", (int)AttackType.Death);
            return;
        }
        // if (anim.GetInteger("state") == (int)AttackType.Damage) return;
        // if (anim.GetInteger("state") == (int)AttackType.Attack) return;
        if (GetDistanceFromPlayer() <= attackDistance && !playerSpells.phaseWalkActive && timeSinceAttack <= 0 && !isAttacking)
        {
            // anim.SetInteger("state", (int)AttackType.Attack);
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
            if (!isAttacking)
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

    private void Patrol()
    {
        //anim.SetInteger("state", (int)AttackType.Idle);

        if (patrolDirection == Direction.Right)
            transform.position += Vector3.right * speed * Time.deltaTime;
        else if (patrolDirection == Direction.Left)
            transform.position += Vector3.left * speed * Time.deltaTime;
        if (Mathf.Abs(transform.position.x - spawnPosition.x) >= patrolRange)
        {
            patrolDirection = 1 - patrolDirection;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
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

    public override void OnDamageTaken(float damage, bool isCritical)
    {
        base.OnDamageTaken(damage, isCritical);
        // anim.SetInteger("state", (int)AttackType.Damage);
    }

    public void OnDamageTakenEnd()
    {
        //anim.SetInteger("state", (int)AttackType.Idle);
    }

    public void OnAttackEnd()
    {
        // anim.SetInteger("state", (int)AttackType.Idle);
        timeSinceAttack = attackCooldown;
    }

}