using System.Linq;
using UnityEngine;

public class FireMob2 : Enemy
{
    public enum AttackType
    {
        Idle,
        Attack,
        Damage,
        Death
    }

    public enum Direction
    {
        Left,
        Right
    }
    [SerializeField] private Direction patrolDirection;
    [SerializeField] private float patrolRange;
    [SerializeField] private float attackDistance;

    private Vector3 spawnPosition;

    private new void Start()
    {
        base.Start();

        spawnPosition = transform.position;

        if (patrolDirection == Direction.Left)
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        else
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    private new void Update()
    {
        base.Update();
        if (GetDistanceFromPlayer() < attackDistance)
            player.GetComponent<PlayerResources>().TakeDamage(1, transform.position);
        else
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        //anim.SetInteger("state", (int)AttackType.Idle);

        if (patrolDirection == Direction.Right)
            transform.position += Vector3.right * speed * Time.deltaTime;
        else if (patrolDirection == Direction.Left)
            transform.position += Vector3.left * speed * Time.deltaTime;

        if (Mathf.Abs(transform.position.x - spawnPosition.x) >= patrolRange)
        {
            patrolDirection = 1 - patrolDirection;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;
        if (collision.GetComponent<PlayerSpells>().phaseWalkActive) return;
    }
}