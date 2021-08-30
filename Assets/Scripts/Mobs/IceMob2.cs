using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class IceMob2 : Enemy
{ 
    private enum Direction
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }

    private enum AnimType
    {
        Idle,
        Attack,
        Damage,
        Death
    }
    [SerializeField] private Animator anim;
    private AnimType animType;
    [SerializeField] private bool damageAnimation;
    private Vector3 initialPosition;

    private bool isAttacking;
    [SerializeField] private float oldHp;

    private PlayerResources playerResources;

    [SerializeField] private Direction state_mob;
    [SerializeField] private float thresholdDistance = 10f;
    [SerializeField] private float thresholdDown = -5f;
    [SerializeField] private float thresholdLeft = -5f;
    [SerializeField] private float thresholdRight = 5f;

    [SerializeField] private float thresholdUp = 5f;
    private float timeAttack;
    private float timeNextAttack;

    private new void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
        isAttacking = false;
        state_mob = Direction.UP;
        speed = 5;
        initialPosition = transform.position;
    }

    private new void Start()
    {
        base.Start();
        playerResources = FindObjectOfType<PlayerResources>();
        playerSpells = FindObjectOfType<PlayerSpells>();
        animType = AnimType.Idle;
        oldHp = hp;
    }

    private new void Update()
    {
        if (hp <= 0) return;

        base.Update();

        if (damageAnimation) return;

        if (isAttacking)
        {
            if (timeAttack <= Time.time)
            {
                if (GetDistanceFromPlayer() < 2 * thresholdDistance) playerResources.TakeDamage(1, transform.position);

                isAttacking = false;
            }

            if (pause)
                timeAttack += Time.deltaTime;
        }
        else
        {
            transform.position += state_mob switch
            {
                Direction.UP => Vector3.up * Time.deltaTime * speed,
                Direction.DOWN => Vector3.down * Time.deltaTime * speed,
                Direction.LEFT => Vector3.left * Time.deltaTime * speed,
                Direction.RIGHT => Vector3.right * Time.deltaTime * speed,
                _ => throw new ArgumentOutOfRangeException()
            };

            if (timeNextAttack > Time.time) return;
            if (GetDistanceFromPlayer() < thresholdDistance && !playerSpells.phaseWalkActive)
                Attack();
        }
    }

    private void FixedUpdate()
    {
        anim.SetFloat("speed", attackSpeed / 3.5f);
        anim.SetInteger("state", (int)animType);
        if (hp <= 0)
        {
            animType = AnimType.Death;
            return;
        }

        if (damageAnimation) return;

        if (Math.Abs(oldHp - hp) > 0.1f)
        {
            DamageAnimation();
            return;
        }

        oldHp = hp;

        transform.localScale = new Vector3(Mathf.Sign(transform.position.x - playerResources.transform.position.x),
            transform.localScale.y, transform.localScale.z);

        if (Random.Range(0f, 1f / Time.fixedDeltaTime) < 0.3f) state_mob = (Direction)Random.Range(0f, 3.99f);

        var i = 0;
        while (!CheckDirection())
        {
            i++;
            if (i > 10)
                break;
            state_mob = (Direction)Random.Range(0f, 3.99f);
        }
    }

    private void DamageAnimation()
    {
        damageAnimation = true;
        animType = AnimType.Damage;
    }

    public void DamageEnd()
    {
        Debug.Log("DamageEnd");
        damageAnimation = false;
        oldHp = hp;
        if (hp > 0)
            animType = AnimType.Idle;
    }

    private bool CheckDirection()
    {
        if (transform.position.y >= initialPosition.y + thresholdUp && state_mob == Direction.UP)
            return false;
        if (transform.position.y <= initialPosition.y + thresholdDown && state_mob == Direction.DOWN)
            return false;
        if (transform.position.x <= initialPosition.x + thresholdLeft && state_mob == Direction.LEFT)
            return false;
        if (transform.position.x >= initialPosition.x + thresholdRight && state_mob == Direction.RIGHT)
            return false;

        return true;
    }

    private void Attack()
    {
        anim.SetFloat("speed", attackSpeed / 2.5f);
        animType = AnimType.Attack;
        isAttacking = true;
        timeAttack = Time.time + attackSpeed;
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

        time = attackSpeed / 4f;
        while (time > 0)
        {
            transform.localScale *= 1.01f;
            transform.position = Vector2.MoveTowards(transform.position, initialPos,
                thresholdDistance * attackSpeed * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
            time -= Time.deltaTime;
        }

        anim.SetFloat("speed", attackSpeed / 3.5f);
        animType = AnimType.Idle;
        transform.position = initialPos;
        transform.localScale = initialLocalScale;
    }

    private float GetDistanceFromPlayer()
    {
        return (playerResources.transform.position - transform.position).magnitude;
    }
}