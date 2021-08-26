using System;
using UnityEngine;

internal enum Direction
{
    LEFT,
    RIGHT
}

public class IceMob2 : Enemy
{ 
    private enum State
    {
        Idle,
        Attack,
        Damage,
        Death
    }
    private State animState = State.Idle;

    private Animator animator;
    private float attackTimer;
    [SerializeField] private bool damageAnimation;
    [SerializeField] private float oldHp;
    private Direction patrolDirection;
    [SerializeField] private float patrolRange;
    private PlayerResources playerResources;

    private Vector3 spawnPosition;

    [SerializeField] private float thresholdDistance;

    private new void Start()
    {
        base.Start();

        spawnPosition = transform.position;
        patrolDirection = Direction.LEFT;
        
        attackTimer = attackSpeed;
        playerResources = FindObjectOfType<PlayerResources>();
        animator = GetComponent<Animator>();
        oldHp = hp;
    }

    private new void Update()
    {
        UpdateAnimation();

        if (hp <= 0)
        {
            animState = State.Death;
            return;
        }

        if (damageAnimation) return;
        base.Update();
        if (Math.Abs(oldHp - hp) > 0.1f)
        {
            DamageAnimation();
            return;
        }

        oldHp = hp;
        if (animState == State.Idle)
            Patrol();
        else
            Attack();
    }

    private void DamageAnimation()
    {
        damageAnimation = true;
        animState = State.Damage;
    }

    public void DamageEnd()
    {
        damageAnimation = false;
        oldHp = hp;
        if (hp > 0)
            animState = State.Idle;
    }

    private void UpdateAnimation()
    {
        animator.SetInteger("state", (int)animState);
        animator.SetFloat("speed", attackSpeed / 3.5f);
    }

    private void Patrol()
    {
        transform.position += patrolDirection switch
        {
            Direction.RIGHT => Vector3.right * speed * Time.deltaTime,
            Direction.LEFT => Vector3.left * speed * Time.deltaTime,
            _ => throw new ArgumentOutOfRangeException()
        };

        if (Mathf.Abs(transform.position.x - spawnPosition.x) < patrolRange) return;
        patrolDirection = 1 - patrolDirection;
        transform.localScale =
            new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z); // flip x
    }

    private void Attack()
    {
        attackTimer -= Time.deltaTime;

        if (attackTimer > 0) return;
        if ((playerResources.transform.position - transform.position).magnitude < thresholdDistance)
            playerResources.TakeDamage(1, transform.position);
        attackTimer = attackSpeed;
        animState = State.Idle;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;
        if (col.GetComponent<PlayerSpells>().phaseWalkActive) return;
        animState = State.Attack;
    }
}