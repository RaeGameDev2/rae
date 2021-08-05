using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ICE_MOB1 : Enemy
{
    public float speedY = 5.5f;
    public enum Direction
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    };
    public Direction state_mob;


    public float treshHoldUP = 2f;
    public float treshHoldDOWN = -5f;

    void Start()
    {
        //transform.position = new Vector3(3f, 0, 0);
        state_mob = Direction.UP;

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y >= treshHoldUP && state_mob == Direction.UP)
            state_mob = Direction.DOWN;
        else
             if (transform.position.y <= treshHoldDOWN && state_mob == Direction.DOWN)
            state_mob = Direction.UP;


        if (state_mob == Direction.UP)
            transform.position += new Vector3(0, 1, 0) * Time.deltaTime * speedY;
        else if (state_mob == Direction.DOWN)
            transform.position -= new Vector3(0, 1, 0) * Time.deltaTime * speedY;
    }
}