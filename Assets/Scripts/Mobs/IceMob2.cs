using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class IceMob2 : Enemy
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

    [SerializeField] private Animator anim;
    private AnimType animType;
    private float animSpeed;
    [SerializeField] private bool damageAnimation;

    private bool isAttacking;

    [SerializeField] private Direction direction;
    [SerializeField] private float thresholdDistance = 10f;

    [SerializeField] private float thresholdDown = -5f;
    [SerializeField] private float thresholdLeft = -5f;
    [SerializeField] private float thresholdRight = 5f;
    [SerializeField] private float thresholdUp = 5f;

    private float timeNextAttack;
    private bool hasDamagedPlayer;
    private bool attackStarted;

    private new void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
        isAttacking = false;
        direction = Direction.Up;
        animType = AnimType.Idle;
        animSpeed = speed / 5f;
    }

    private void FixedUpdate()
    {
        if (hp <= 0)
        {
            animType = AnimType.Death;
            return;
        }

        anim.SetFloat("speed", animSpeed);
        anim.SetInteger("state", (int)animType);

        if (damageAnimation) return;
        if (attackStarted) return;

        transform.position += direction switch
        {
            Direction.Up => Vector3.up * Time.deltaTime * speed,
            Direction.Down => Vector3.down * Time.deltaTime * speed,
            Direction.Left => Vector3.left * Time.deltaTime * speed,
            Direction.Right => Vector3.right * Time.deltaTime * speed,
            _ => throw new ArgumentOutOfRangeException()
        };

        if (Random.Range(0f, 1f / Time.fixedDeltaTime) < 0.3f) direction = (Direction) Random.Range(0f, 3.99f);

        for (var i = 0; i < 10; i++)
        {
            if (CheckDirection())
                break;
            direction = (Direction) Random.Range(0f, 3.99f);
        }

        if (GetDistanceFromPlayer() < thresholdDistance && !playerSpells.phaseWalkActive && Time.time >= timeNextAttack)
            StartCoroutine(Attack());
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

    private bool CheckDirection()
    {
        if (transform.position.y >= spawnPosition.y + thresholdUp && direction == Direction.Up)
            return false;
        if (transform.position.y <= spawnPosition.y + thresholdDown && direction == Direction.Down)
            return false;
        if (transform.position.x <= spawnPosition.x + thresholdLeft && direction == Direction.Left)
            return false;
        if (transform.position.x >= spawnPosition.x + thresholdRight && direction == Direction.Right)
            return false;

        return true;
    }

    private IEnumerator Attack()
    {
        var initialLocalScale = transform.localScale;
        var initialPos = transform.position;
        var playerInitialPosition = player.position;
        var attackDuration = 4f / attackSpeed;


        timeNextAttack = Time.time + 3f * attackDuration / 2f;
        attackStarted = true;
        transform.localScale = new Vector3(Mathf.Sign(transform.position.x - player.position.x),
            transform.localScale.y, transform.localScale.z);

        yield return new WaitForSeconds(3f * attackDuration / 4f);

        transform.localScale = new Vector3(Mathf.Sign(transform.position.x - player.position.x),
            transform.localScale.y, transform.localScale.z);
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

        Time.timeScale = 0.3f;
        animSpeed = attackSpeed;
        animType = AnimType.Attack;
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

        animSpeed = speed / 5f;
        animType = AnimType.Idle;
        isAttacking = false;
        attackStarted = false;
        hasDamagedPlayer = false;
        transform.position = initialPos;
        transform.localScale = initialLocalScale;
    }

    public override void OnDamageTaken(float damage, bool isCritical)
    {
        if (animType == AnimType.Death) return;
        base.OnDamageTaken(damage, isCritical);
        if (attackStarted) return;
        animType = AnimType.Damage;
        damageAnimation = true;
    }
    public void DamageEnd()
    {
        damageAnimation = false;
        if (hp > 0)
            animType = AnimType.Idle;
    }
}