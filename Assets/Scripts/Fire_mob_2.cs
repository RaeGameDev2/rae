using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Dir
    {
        LEFT,
        RIGHT
    }

public class Fire_mob_2 : Enemy
{
    [SerializeField] private float patrol;
    [SerializeField] private float speedfmob2;
    private SpriteRenderer spriteRenderer;

    private Vector3 spawnPosition;
    private Dir patrolDirection;
    private Vector3 movementAxis;

    void Start()
    {
        patrol = 5;
        speedfmob2 = 3f;

        spriteRenderer = GetComponent<SpriteRenderer>();
        spawnPosition = transform.position;
        patrolDirection = Dir.LEFT;
        movementAxis = new Vector3(1, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        if(patrolDirection == Dir.RIGHT)
            transform.position += movementAxis * speedfmob2 * Time.deltaTime;
        else
            if(patrolDirection == Dir.LEFT)
                transform.position -= movementAxis * speedfmob2 * Time.deltaTime;

        //switch Dir
        if(Mathf.Abs(transform.position.x - spawnPosition.x) >= patrol)
        {
            patrolDirection = 1 - patrolDirection;
            spriteRenderer.flipX = patrolDirection == Dir.RIGHT;
        }
    }
}
