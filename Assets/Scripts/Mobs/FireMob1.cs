using UnityEngine;

public class FireMob1 : Enemy
{
    public enum AttackType
    {
        Idle,
        Attack,
        Damage,
        Death
    }


    public bool isAttacking;
    private Directions patrolDirection;

    [SerializeField] private float patrolRange;
    private PlayerResources playerResources;

    private Vector3 spawnPosition;
    [SerializeField] private float thresholdDistance = 5f;

    private PlayerSpells playerSpells;
    private Animator anim;

    private new void Start()
    {
        base.Start();

        patrolRange = 5;
        speed = 3;

        spawnPosition = transform.position;
        patrolDirection = Directions.Left;

        playerResources = FindObjectOfType<PlayerResources>();

        isAttacking = false;

        anim = GetComponent<Animator>();
        playerSpells = FindObjectOfType<PlayerSpells>().GetComponent<PlayerSpells>();
    }

    // Update is called once per frame
    private new void Update()
    {
        base.Update();

        if (hp <= 0)
        {
            anim.SetInteger("state", (int)AttackType.Death);
            return;
        }
        if (anim.GetInteger("state") == (int)AttackType.Damage) return;
        if (anim.GetInteger("state") == (int)AttackType.Attack) return;

        if (GetDistanceFromPlayer() < thresholdDistance && !playerSpells.phaseWalkActive)
        {
            isAttacking = true;

            if (playerResources.transform.position.x < transform.position.x && patrolDirection == Directions.Right)
            {
                transform.localScale =
                    new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                patrolDirection = Directions.Left;
            }
            else if (playerResources.transform.position.x > transform.position.x && patrolDirection == Directions.Left)
            {
                transform.localScale =
                    new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                patrolDirection = Directions.Right;
            }
        }
        else
        {
            isAttacking = false;
        }

        if (!isAttacking)
            Patrol();
        else
            Attack();
    }

    private void Patrol()
    {
        anim.SetInteger("state", (int)AttackType.Idle);

        if (patrolDirection == Directions.Right)
            transform.position += Vector3.right * speed * Time.deltaTime;
        else if (patrolDirection == Directions.Left)
            transform.position += Vector3.left * speed * Time.deltaTime;

        if (Mathf.Abs(transform.position.x - spawnPosition.x) >= patrolRange)
        {
            patrolDirection = 1 - patrolDirection;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    private void Attack()
    {
        anim.SetInteger("state", (int)AttackType.Attack);
    }

    private float GetDistanceFromPlayer()
    {
        return (playerResources.transform.position - transform.position).magnitude;
    }

    public override void OnDamageTaken(float damage, bool isCritical)
    {
        base.OnDamageTaken(damage, isCritical);
        //Debug.Log("sex");
        anim.SetInteger("state", (int)AttackType.Damage);
    }

    public void OnDamageTakenEnd()
    {
        anim.SetInteger("state", (int)AttackType.Idle);
    }

    public void OnAttackEnd()
    {
        anim.SetInteger("state", (int)AttackType.Idle);
        Debug.Log("se termina atacul");
    }

    private enum Directions
    {
        Left,
        Right
    }
}