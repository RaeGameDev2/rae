using System;
using UnityEngine;

enum Direction
{
    LEFT,
    RIGHT
}

public class ICE_MOB2 : Enemy
{
    [SerializeField] private float patrolRange;

    [SerializeField] private float thresholdDistance = 5f;

    private Vector3 spawnPosition;
    private Direction patrolDirection;
    private Resources playerResources;
    private Animator animator;
    // Attack rate: seconds till next hit
    private float attackTimer;

    public enum State
    {
        IDLE,
        ATTACK,
        DEATH
    }

    private State animState = State.IDLE;

    private new void Start()
    {
        base.Start();

        spawnPosition = transform.position;
        patrolDirection = Direction.LEFT;

        // 1 hit per 2 seconds
        attackTimer = attackSpeed;
        playerResources = FindObjectOfType<Resources>();
        animator = GetComponent<Animator>();
    }

    private new void Update()
    {
        if (hp <= 0)
            animState = State.DEATH;
        base.Update();
        if (animState == State.IDLE)
            Patrol();
        else
            Attack();
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        animator.SetInteger("state", (int)animState);
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
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);  // flip x
    }

    private void Attack()
    {
        attackTimer -= Time.deltaTime;

        if (attackTimer > 0) return;
        Debug.Log("Mob2: I attacked!");
        if ((playerResources.transform.position - transform.position).magnitude < thresholdDistance)
            playerResources.TakeDamage(damageOnTouch, transform.position);
        attackTimer = attackSpeed;
        animState = State.IDLE;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;
        animState = State.ATTACK;
    }
}
