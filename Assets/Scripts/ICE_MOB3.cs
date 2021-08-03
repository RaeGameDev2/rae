using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ICE_MOB3 : MonoBehaviour
{
    public SpriteRenderer Mob3_Sprite;
    public Sprite idle;
    public Sprite active;
    public static int damage = 30;
    private float delay_explosion = 1;
    private float remainingdelay;
    private int initiate_explosion = 0;
    // Start is called before the first frame update
    void Start()
    {
        remainingdelay = delay_explosion;
        Mob3_Sprite.sprite = idle;
    }

    // Update is called once per frame
    void Update()
    {
        if (initiate_explosion == 1)
        {
            remainingdelay -= Time.deltaTime;

            if (remainingdelay <= 0)
            {
                Destroy(this.gameObject);
                initiate_explosion = 0;
                remainingdelay = delay_explosion;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Mob3_Sprite.sprite = active;
            initiate_explosion = 1;
        }
    }
/*    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Mob3_Sprite.sprite = idle;
        }
    }*/
}
