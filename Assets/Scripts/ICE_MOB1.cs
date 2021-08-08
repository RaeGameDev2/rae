using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;


// Summary:
//      Se muta semi random intre thresholdUp si thresholdDown pe Oy si intre thresholdLeft si thresholdRight pe Ox
//      Cand se aproie la thresholdDistance (de setat din inspector) de player se opreste din miscre si dupa face o animatie de atac
//      Player ul ia damage doar daca pana trece attackSpeed secunde este la mai putin de 2 * thresholdDistance de enemy
public class ICE_MOB1 : Enemy
{
    private enum Direction
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }

    [SerializeField] private Direction state_mob;
    private Vector3 initialPosition;

    [SerializeField] private float thresholdUp = 5f;
    [SerializeField] private float thresholdDown = -5f;
    [SerializeField] private float thresholdLeft = -5f;
    [SerializeField] private float thresholdRight = 5f;
    [SerializeField] private float thresholdDistance = 10f;
    
    private Resources playerResources;

    private bool isAttacking;
    private float timeAttack;
    private float timeNextAttack;

    private new void Awake()
    {
        base.Awake();
        isAttacking = false;
        state_mob = Direction.UP;
        speed = 5;
        initialPosition = transform.position;
    }

    private new void Start()
    {
        base.Start();
        playerResources = GameObject.FindGameObjectWithTag("Player").GetComponent<Resources>();
    }

    private new void Update()
    {
        base.Update();

        if (isAttacking)
        {
            if (timeAttack <= Time.time)
            {
                if (GetDistanceFromPlayer() < 2 * thresholdDistance)
                {
                    playerResources.TakeDamage(1, transform.position);
                }

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
            if (GetDistanceFromPlayer() < thresholdDistance)
                Attack();
        }
    }

    private void FixedUpdate()
    {
        if (Random.Range(0f, 1f / Time.fixedDeltaTime) < 0.3f) {
            state_mob = (Direction)Random.Range(0f, 3.99f);
        }

        var i = 0;
        while (!CheckDirection())
        {
            i++;
            if (i > 10)
            {
                Debug.LogError("CheckDirection");
                break;
            }
            state_mob = (Direction) Random.Range(0f, 3.99f);
        }
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

        transform.position = initialPos;
        transform.localScale = initialLocalScale;
    }

    private float GetDistanceFromPlayer()
    {
        return (playerResources.transform.position - transform.position).magnitude;
    }
}