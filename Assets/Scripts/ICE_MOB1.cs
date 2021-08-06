using System.Collections;
using UnityEngine;

public class ICE_MOB1 : Enemy
{
    private enum Direction
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }
    private Direction state_mob;
    
    [SerializeField] private float speedY = 5.5f;
    [SerializeField] private float thresholdUP = 2f;
    [SerializeField] private float thresholdDOWN = -5f;
    [SerializeField] private float thresholdDistance = 10f;
    
    private Resources playerResources;

    private bool isAttacking;
    private float timeAttack;

    private new void Awake()
    {
        base.Awake();
        isAttacking = false;
        state_mob = Direction.UP;
    }

    private new void Start()
    {
        base.Start();
        playerResources = GameObject.FindGameObjectWithTag("Player").GetComponent<Resources>();
    }

    private new void Update()
    {
        base.Update();
        if (transform.position.y >= thresholdUP && state_mob == Direction.UP)
            state_mob = Direction.DOWN;
        else
             if (transform.position.y <= thresholdDOWN && state_mob == Direction.DOWN)
            state_mob = Direction.UP;

        if (isAttacking)
        {
            if (timeAttack <= Time.time)
            {
                if (GetDistanceFromPlayer() < 2 * thresholdDistance)
                {
                    playerResources.TakeDamage(1);
                }

                isAttacking = false;
            }

            if (pause)
                timeAttack += Time.deltaTime;
        }
        else
        {
            if (state_mob == Direction.UP)
                transform.position += Vector3.up * Time.deltaTime * speedY;
            else if (state_mob == Direction.DOWN)
                transform.position += Vector3.down * Time.deltaTime * speedY;

            if (GetDistanceFromPlayer() < thresholdDistance)
                Attack();
        }
    }
    // private void OnTriggerEnter2D(Collider2D col)
    // {
    //     if (col.CompareTag("Player"))
    //         playerResources.TakeDamage(damageOnTouch);
    // }

    private void Attack()
    {
        isAttacking = true;
        timeAttack = Time.time + attackSpeed;
        Debug.Log($"Attack {timeAttack} , {Time.time} , {attackSpeed}");
        StartCoroutine(AttackEnumerator());
    }
    
    private IEnumerator AttackEnumerator()
    {
        var time = attackSpeed;
        var initialLocalScale = transform.localScale;
        while (time > 0)
        {
            transform.localScale *= 1.001f;
            yield return new WaitForFixedUpdate();
            time -= Time.fixedDeltaTime;
        }

        transform.localScale = initialLocalScale;
    }

    private float GetDistanceFromPlayer()
    {
        return (playerResources.transform.position - transform.position).magnitude;
    }
}