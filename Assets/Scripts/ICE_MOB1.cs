using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ICE_MOB1 : MonoBehaviour
{
    public GameObject hpBar;

    public float speedY = 5.5f;
    public float Health = 300;
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
        transform.position = new Vector3(3f, 0, 0);
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

        /*Debug.Log(Health);*/

        if (Health <= 0)
            Destroy(this.gameObject);

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (Weapons_Handler.is_attacking == true)
        {
            if (collision.tag == "Sword")
            {
                Health -= 50;
            }
            if (collision.tag == "Scythe")
            {
                Health -= 50;
            }
            if (collision.tag == "Spear")
            {
                Health -= 50;
            }
            if (collision.tag == "Basic_Staff")
            {
                Health -= 50;
            }
            if (collision.tag == "Ancient_Staff")
            {
                Health -= 50;
            }

            hpBar.transform.localScale = new Vector2(hpBar.transform.localScale.x * ((float) Health / 300.0f), hpBar.transform.localScale.y);
        }
    }
}