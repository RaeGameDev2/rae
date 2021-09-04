using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class JungleMob2 : Enemy
{
    private enum Direction
    {
        Up,
        Down,
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
    private bool isAttacking;

    [SerializeField] private Direction state_mob;
    [SerializeField] private float thresholdDistance = 10f;

    [SerializeField] private float thresholdDown = -5f;
    [SerializeField] private float thresholdLeft = -5f;
    [SerializeField] private float thresholdRight = 5f;
    [SerializeField] private float thresholdUp = 5f;
    private float timeAttack;
    private float timeNextAttack;
    private Animator anim;

    private new void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
        isAttacking = false;
        state_mob = Direction.Up;
    }

    private new void Start()
    {
        base.Start();
    }

    private new void Update()
    {
        base.Update();

        anim.SetFloat("speed", speed / 3);
        anim.SetFloat("attackSpeed", attackSpeed / 100);

        if (hp <= 0)
        {
            anim.SetInteger("state", (int)AnimType.Death);
            return;
        }
        if (anim.GetInteger("state") == (int)AnimType.Damage) return;
        if (anim.GetInteger("state") == (int)AnimType.Attack) return;

        if (isAttacking)
        {
            if (timeAttack <= Time.time)
            {
                isAttacking = false;
            }

            if (pause)
                timeAttack += Time.deltaTime;
        }
        else
        {
            transform.position += state_mob switch
            {
                Direction.Up => Vector3.up * Time.deltaTime * speed,
                Direction.Down => Vector3.down * Time.deltaTime * speed,
                Direction.Left => Vector3.left * Time.deltaTime * speed,
                Direction.Right => Vector3.right * Time.deltaTime * speed,
                _ => throw new ArgumentOutOfRangeException()
            };

            if (timeNextAttack > Time.time) return;
            if (GetDistanceFromPlayer() < thresholdDistance && !playerSpells.phaseWalkActive)
                Attack();
        }
    }

    private void FixedUpdate()
    {
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

    private bool CheckDirection()
    {
        if (transform.position.y >= spawnPosition.y + thresholdUp && state_mob == Direction.Up)
            return false;
        if (transform.position.y <= spawnPosition.y + thresholdDown && state_mob == Direction.Down)
            return false;
        if (transform.position.x <= spawnPosition.x + thresholdLeft && state_mob == Direction.Left)
            return false;
        if (transform.position.x >= spawnPosition.x + thresholdRight && state_mob == Direction.Right)
            return false;

        return true;
    }

    private void Attack()
    {
        anim.SetInteger("state", (int)AnimType.Attack);
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
            transform.position = Vector2.MoveTowards(transform.position, playerInitialPosition,
                thresholdDistance * attackSpeed * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
            time -= Time.fixedDeltaTime;
        }

        time = attackSpeed / 4f;
        while (time > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, initialPos,
                thresholdDistance * attackSpeed * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
            time -= Time.deltaTime;
        }

        anim.SetInteger("state", (int)AnimType.Idle);
        transform.position = initialPos;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;
        if (playerSpells.phaseWalkActive) return;
        playerResources.TakeDamage(1, transform.position);
        anim.SetInteger("state", (int)AnimType.Attack);
    }

    public override void OnDamageTaken(float damage, bool isCritical)
    {
        base.OnDamageTaken(damage, isCritical);
        anim.SetInteger("state", (int)AnimType.Damage);
    }

    public void OnDamageTakenEnd()
    {
        anim.SetInteger("state", (int)AnimType.Idle);
    }
}
