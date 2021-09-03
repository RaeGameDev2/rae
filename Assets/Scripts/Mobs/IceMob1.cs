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

        animSpeed = speed / 15f;
    }

    private new void Update()
    {
        if (hp <= 0)
        {
            animType = AnimType.Death;
            return;
        }

        base.Update();
        UpdateAnimation();

        transform.localScale = new Vector3(Mathf.Sign(transform.position.x - player.position.x),
            transform.localScale.y, transform.localScale.z);

        if (attackStarted) return;
        if (damageAnimationActive) return;

        if (GetDistanceFromPlayer() < thresholdDistance && !playerSpells.phaseWalkActive && Time.time >= timeNextAttack)
            StartCoroutine(Attack());
        else 
            Patrol();
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
    }

    private IEnumerator Attack()
    {
        var initialLocalScale = transform.localScale;
        var initialPos = transform.position;
        var playerInitialPosition = player.position;
        var attackDuration = 4f / attackSpeed;
        
        animSpeed = attackSpeed / 4f;
        animType = AnimType.Attack;
        attackStarted = true;
        timeNextAttack = Time.time + 3f * attackDuration / 2f;
        
        yield return new WaitForSeconds(3f * attackDuration / 4f);
        
        if (GetDistanceFromPlayer() < 2 * thresholdDistance)
            playerInitialPosition = player.position;

        var time = attackDuration / 4f;
        while (time > 0)
        {
            transform.localScale *= 0.99f;
            transform.position = Vector2.MoveTowards(transform.position, playerInitialPosition,
                thresholdDistance * attackSpeed * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
            time -= Time.fixedDeltaTime;
        }

        isAttacking = true;

        time = attackDuration / 4f;
        while (time > 0)
        {
            transform.localScale *= 1.01f;
            transform.position = Vector2.MoveTowards(transform.position, initialPos,
                thresholdDistance * attackSpeed * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
            time -= Time.deltaTime;
        }

        animSpeed = speed / 15f;
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
        if (animType == AnimType.Death) return;
        base.OnDamageTaken(damage, isCritical);
        if (attackStarted) return;
        animType = AnimType.Damage;
        damageAnimationActive = true;
    }
}