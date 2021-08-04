using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Direction
{
    LEFT,
    RIGHT
}

public class ICE_MOB2 : MonoBehaviour
{
    [SerializeField] GameObject Player;

    [SerializeField] private float attackRadius;
    [SerializeField] private float attackSpeed;

    [SerializeField] private float patrolRange;
    [SerializeField] private float patrolSpeed;

    private Vector3 spawnPosition;
    private Direction patrolDirection;
    private Vector3 movementAxis;

    void Start()
    {
        spawnPosition = transform.position;
        patrolDirection = Direction.LEFT;
        // It moves only on X axis
        movementAxis = new Vector3(1, 0, 0);
    }

    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        if (patrolDirection == Direction.RIGHT)
            transform.position += movementAxis * patrolSpeed * Time.deltaTime;
        else if (patrolDirection == Direction.LEFT)
            transform.position -= movementAxis * patrolSpeed * Time.deltaTime;

        if (Mathf.Abs(transform.position.x - spawnPosition.x) >= patrolRange)
        {
            // Switch direction
            patrolDirection = 1 - patrolDirection;
        }
    }
}
