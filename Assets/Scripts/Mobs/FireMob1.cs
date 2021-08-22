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

    public Animator anim;

    private float damageFinish;

    // Attack rate: seconds till next hit
    public bool isAttacking;
    private Vector3 movementAxis;
    private Directions patrolDirection;

    [SerializeField] private float patrolRange;
    private PlayerResources playerResources;

    private Vector3 spawnPosition;
    [SerializeField] private float thresholdDistance = 5f;

    private new void Start()
    {
        base.Start();

        patrolRange = 5;
        speed = 3;

        spawnPosition = transform.position;
        patrolDirection = Directions.Left;
        movementAxis = new Vector3(1, 0, 0);

        playerResources = FindObjectOfType<PlayerResources>();

        isAttacking = false;

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private new void Update()
    {
        base.Update();

        if (damageFinish > Time.time) return;

        if (GetDistanceFromPlayer() < thresholdDistance)
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
        anim.SetInteger("state", (int) AttackType.Idle);

        if (patrolDirection == Directions.Right)
            transform.position += movementAxis * speed * Time.deltaTime;
        else if (patrolDirection == Directions.Left)
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

        anim.SetInteger("state", (int) AttackType.Damage);
        damageFinish = Time.time + 1;
    }

    private enum Directions
    {
        Left,
        Right
    }
}