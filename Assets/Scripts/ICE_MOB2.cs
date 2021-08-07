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

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite idleSprite;
    [SerializeField] private Sprite attackSprite;
    [SerializeField] private float thresholdDistance = 5f;

    private Vector3 spawnPosition;
    private Direction patrolDirection;
    private Resources playerResources;
    private bool isAttacking;
    // Attack rate: seconds till next hit
    private float attackTimer;

    private new void Start()
    {
        base.Start();

        spriteRenderer = GetComponent<SpriteRenderer>();
        spawnPosition = transform.position;
        patrolDirection = Direction.LEFT;

        // 1 hit per 2 seconds
        isAttacking = false;
        attackTimer = attackSpeed;
        playerResources = FindObjectOfType<Resources>();
    }

    private new void Update()
    {
        base.Update();
        if (!isAttacking)
            Patrol();
        else
            Attack();
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
        spriteRenderer.flipX = patrolDirection == Direction.RIGHT;
    }

    private void Attack()
    {
        attackTimer -= Time.deltaTime;

        if (attackTimer > 0) return;
        Debug.Log("Mob2: I attacked!");
        if ((playerResources.transform.position - transform.position).magnitude < thresholdDistance)
            playerResources.TakeDamage(damageOnTouch, transform.position);
        attackTimer = attackSpeed;
        isAttacking = false;
        transform.localScale /= 1.5f;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;
        isAttacking = true;
        transform.localScale *= 1.5f;
    }
}
