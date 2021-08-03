using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ICE_MOB3 : MonoBehaviour
{
    public SpriteRenderer Mob3_Sprite;
    public Sprite idle;
    public Sprite active;
    public static int damage = 30;
    private static float delay_explosion = 2;
    private float remainingdelay;
    public static int initiate_explosion = 0;
    // Start is called before the first frame update
    void Start()
    {
        Mob3_Sprite.sprite = idle;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Mob3_Sprite.sprite = active;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Mob3_Sprite.sprite = idle;
        }
    }
}
