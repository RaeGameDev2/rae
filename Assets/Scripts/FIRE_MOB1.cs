using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FIRE_MOB1 : Enemy
{
    private Transform target;
    private Resources playerResources;

    [SerializeField] private float patrolRange;
    [SerializeField] private float thresholdDistance = 5f;

    private enum Directions
    {
        LEFT,
        RIGHT
    }

    public enum AttackType
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
    public bool isAttacking;
    public Animator anim;

    private float damageFinish;

    void Start()
    {
        base.Start();

        patrolRange = 5;
        speed = 3;

        spawnPosition = transform.position;
        patrolDirection = Directions.LEFT;
        movementAxis = new Vector3(1, 0, 0);

        playerResources = FindObjectOfType<Resources>();

        isAttacking = false;

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();

        if (damageFinish > Time.time) return;

        if (GetDistanceFromPlayer() < thresholdDistance)
        {
            isAttacking = true;

            if (playerResources.transform.position.x < transform.position.x && patrolDirection == Directions.RIGHT)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                patrolDirection = Directions.LEFT;
            } else if (playerResources.transform.position.x > transform.position.x && patrolDirection == Directions.LEFT)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                patrolDirection = Directions.RIGHT;
            }
        }
        else
            isAttacking = false;

        if (!isAttacking)
            Patrol();
        else
            Attack();
    }

    void Patrol()
    {
        anim.SetInteger("state", (int) AttackType.Idle);

        if (patrolDirection == Directions.RIGHT)
            transform.position += movementAxis * speed * Time.deltaTime;
        else if (patrolDirection == Directions.LEFT)
            transform.position -= movementAxis * speed * Time.deltaTime;

        if (Mathf.Abs(transform.position.x - spawnPosition.x) >= patrolRange)
        {
            patrolDirection = 1 - patrolDirection;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    private void Attack()
    {
        anim.SetInteger("state", (int) AttackType.Attack);
    }

    private float GetDistanceFromPlayer()
    {
        return (playerResources.transform.position - transform.position).magnitude;
    }

    public new void OnDamageTaken(float damage, bool isCritical)
    {
        base.OnDamageTaken(damage, isCritical);

        anim.SetInteger("state", (int)AttackType.Damage);
        damageFinish = Time.time + 1;
    }
}
