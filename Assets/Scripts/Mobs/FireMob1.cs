using UnityEngine;

public class FireMob1 : Enemy
{
    private enum AnimType
    {
        Idle,
        Attack,
        Damage,
        Death
    }

    private enum Direction
    {
        Left,
        Right
    }
    [SerializeField] private Direction patrolDirection;
    [SerializeField] private float patrolRange;
    [SerializeField] private float attackDistance;

    private Animator anim; 
    public bool isAttacking;

    private new void Start()
    {
        base.Start();

        attackCooldown = 1f;

        anim = GetComponent<Animator>();
        transform.localScale = new Vector3(patrolDirection == Direction.Left ? transform.localScale.x : -transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    private new void Update()
    {
        if (hp <= 0)
        {
            anim.SetInteger("state", (int)AnimType.Death);
            return;
        }
        base.Update();

        anim.SetFloat("speed", speed / 3);
        anim.SetFloat("attackSpeed", attackSpeed / 100);
        
        if (anim.GetInteger("state") == (int)AnimType.Damage) return;
        if (anim.GetInteger("state") == (int)AnimType.Attack) return;

        if (GetDistanceFromPlayer() <= attackDistance && !playerSpells.phaseWalkActive && timeSinceAttack <= 0)
        {
            anim.SetInteger("state", (int)AnimType.Attack);
            if (playerSpells.transform.position.x < transform.position.x && patrolDirection == Direction.Right)
            {
                transform.localScale =
                    new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                patrolDirection = Direction.Left;
            }
            else if (playerSpells.transform.position.x > transform.position.x && patrolDirection == Direction.Left)
            {
                transform.localScale =
                    new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                patrolDirection = Direction.Right;
            }
            isAttacking = true;
        }
        else
        {
            Patrol();
            timeSinceAttack -= Time.deltaTime;
            if (timeSinceAttack < 0)
                timeSinceAttack = 0;
            isAttacking = false;
        }
    }

    private void Patrol()
    {
        anim.SetInteger("state", (int)AnimType.Idle);

        if (patrolDirection == Direction.Right)
            transform.position += Vector3.right * speed * Time.deltaTime;
        else if (patrolDirection == Direction.Left)
            transform.position += Vector3.left * speed * Time.deltaTime;
        if (Mathf.Abs(transform.position.x - spawnPosition.x) >= patrolRange)
        {
            if (patrolDirection == Direction.Right)
                transform.position = new Vector3(spawnPosition.x + patrolRange - 0.1f, transform.position.y, transform.position.z);
            else
                transform.position = new Vector3(spawnPosition.x - patrolRange + 0.1f, transform.position.y, transform.position.z);
            patrolDirection = 1 - patrolDirection;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
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

    public void OnAttackEnd()
    {
        anim.SetInteger("state", (int)AnimType.Idle);
        timeSinceAttack = attackCooldown;
        isAttacking = false;
    }
}