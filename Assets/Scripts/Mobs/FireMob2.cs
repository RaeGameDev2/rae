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

    private Vector3 spawnPosition;

    private new void Start()
    {
        base.Start();

        spawnPosition = transform.position;

        if (patrolDirection == Direction.Left)
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        else
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
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
        if (GetDistanceFromPlayer() <= attackDistance && !playerSpells.phaseWalkActive && timeSinceAttack <= 0)
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
            isAttacking = true;
        }
        else
        {
            Patrol();
            timeSinceAttack -= Time.deltaTime;
            if (timeSinceAttack < 0)
                timeSinceAttack = 0;
            isAttacking = false;
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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;
        if (collision.GetComponent<PlayerSpells>().phaseWalkActive) return;
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