using System;
using System.Collections;
using UnityEngine;

public class IceMob1 : Enemy
{
    private enum Direction
    {
        Left,
        Right
    }
    private enum AnimType
    {
        Idle,
        Attack,
        Damage,
        Death
    }
    private AnimType animType = AnimType.Idle;
    private float animSpeed;
    private Animator anim;
    
    private Direction patrolDirection;

    [SerializeField] private float patrolRange;
    [SerializeField] private float thresholdDistance;
    private bool isAttacking;
    private bool attackStarted;
    private bool hasDamagedPlayer;
    private bool damageAnimationActive;
    private float timeNextAttack;

    private new void Start()
    {
        base.Start();
        
        patrolDirection = Direction.Left;
        playerResources = FindObjectOfType<PlayerResources>();
        anim = GetComponent<Animator>();

        animSpeed = speed / 9f;
    }

    private new void Update()
    {
        base.Update();
        UpdateAnimation();

        if (hp <= 0)
        {
            animType = AnimType.Death;
            return;
        }
        if (attackStarted) return;
        if (damageAnimationActive) return;

        if (animType == AnimType.Idle)
            Patrol();
        if (GetDistanceFromPlayer() < thresholdDistance && !playerSpells.phaseWalkActive && Time.time >= timeNextAttack)
            Attack();
    }

    private void UpdateAnimation()
    {
        anim.SetInteger("state", (int)animType);
        anim.SetFloat("speed", animSpeed);
    }

    private void Patrol()
    {
        transform.position += patrolDirection switch
        {
            Direction.Right => Vector3.right * speed * Time.deltaTime,
            Direction.Left => Vector3.left * speed * Time.deltaTime,
            _ => throw new ArgumentOutOfRangeException()
        };

        if (Mathf.Abs(transform.position.x - spawnPosition.x) < patrolRange) return;

        patrolDirection = patrolDirection == Direction.Left ? Direction.Right : Direction.Left;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    private void Attack()
    {
        animSpeed = attackSpeed / 2.5f;
        animType = AnimType.Attack;
        attackStarted = true;
        timeNextAttack = Time.time + 3f * attackSpeed / 2f;
        StartCoroutine(AttackAnimation());
    }


    private IEnumerator AttackAnimation()
    {
        var initialLocalScale = transform.localScale;
        var initialPos = transform.position;
        var playerInitialPosition = playerResources.transform.position;
        yield return new WaitForSeconds(3f * attackSpeed / 4f);
        if (GetDistanceFromPlayer() < 2 * thresholdDistance)
            playerInitialPosition = playerResources.transform.position;

        var time = attackSpeed / 4f;
        while (time > 0)
        {
            transform.localScale *= 0.99f;
            transform.position = Vector2.MoveTowards(transform.position, playerInitialPosition,
                thresholdDistance * attackSpeed * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
            time -= Time.fixedDeltaTime;
        }

        isAttacking = true;

        time = attackSpeed / 4f;
        while (time > 0)
        {
            transform.localScale *= 1.01f;
            transform.position = Vector2.MoveTowards(transform.position, initialPos,
                thresholdDistance * attackSpeed * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
            time -= Time.deltaTime;
        }

        animSpeed = speed / 9f;
        animType = AnimType.Idle;
        isAttacking = false;
        attackStarted = false;
        hasDamagedPlayer = false;
        transform.position = initialPos;
        transform.localScale = initialLocalScale;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;
        if (!isAttacking) return;
        if (playerSpells.phaseWalkActive) return;
        if (hasDamagedPlayer) return;

        hasDamagedPlayer = true;
        playerResources.TakeDamage(1, transform.position);
    }

    public void DamageEnd()
    {
        damageAnimationActive = false;
        if (hp > 0)
            animType = AnimType.Idle;
    }

    public override void OnDamageTaken(float damage, bool isCritical)
    {
        base.OnDamageTaken(damage, isCritical);
        animType = AnimType.Damage;
        damageAnimationActive = true;
    }
}