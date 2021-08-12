using System;
using UnityEngine;



public class Test_mob_1 : Enemy
{
    private Transform target;
    private Resources playerResources;

    [SerializeField] private float patrol;
    [SerializeField] private int attackDamage = 1;

    [SerializeField] private float patrolRange;
    [SerializeField] private Sprite idleSprite;
    [SerializeField] private Sprite attackSprite;
    [SerializeField] private float thresholdDistance = 5f;

    private float canAttack;
    private SpriteRenderer spriteRenderer;

    private enum Directions
    {
        LEFT,
        RIGHT
    }

    private enum AttackType
    {
        Idle,
        Attack,
        Damage,
        Death
    }
    private Vector3 spawnPosition;
    private Directions patrolDirection;
    private Vector3 movementAxis;

    // Attack rate: seconds till next hit
    private bool isAttacking;
    private float attackTimer;
    private Animator anim;

    void Start()
    {
        patrol = 5;
        
        spawnPosition = transform.position;
        patrolDirection = Directions.LEFT;
        movementAxis = new Vector3(1, 0, 0);

        playerResources = FindObjectOfType<Resources>();

        // 1 hit per 2 seconds
        isAttacking = false;
        attackTimer = attackSpeed;
        playerResources = FindObjectOfType<Resources>();

        anim = GetComponent<Animator>();
        anim.SetInteger("state", (int)AttackType.Idle);
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        if (!isAttacking)
            Patrol();
        else
            Attack();
    }

    void Patrol()
    {
        if (patrolDirection == Directions.RIGHT)
            transform.position += movementAxis * speed * Time.deltaTime;
        else
            if(patrolDirection == Directions.LEFT)
                transform.position -= movementAxis * speed * Time.deltaTime;
        
        if(Mathf.Abs(transform.position.x - spawnPosition.x) >= patrol)
        {
            patrolDirection = 1 - patrolDirection;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

        anim.SetFloat("Speed", speed);
    }

    
    private void OnTriggerExit1D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            target = null;
        }
    }


    private void Attack()
    {
        attackTimer -= Time.deltaTime;

        if (attackTimer > 0) return;
        Debug.Log("Fire Mob 1: I attacked!");
        if ((playerResources.transform.position - transform.position).magnitude < thresholdDistance)
            playerResources.TakeDamage(damageOnTouch, transform.position);
        attackTimer = attackSpeed;
        isAttacking = false;

        anim.SetTrigger("Attack");
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;
        isAttacking = true;
    }

}