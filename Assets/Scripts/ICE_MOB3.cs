using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ICE_MOB3 : MonoBehaviour
{
    public SpriteRenderer Mob3_Sprite;
    public Sprite idle;
    public Sprite active;
    public ParticleSystem explosion;
    public static int damage = 30;
    private float time_until_dissapear = 1;
    private float remaining_time_until_dissapear;
    private float time_until_explosion_dissapear = 1;
    private float remaining_time_until_explosion_dissapear;
    private int initiate_explosion = 0;
    private int explosion_active = 0;
    private ParticleSystem explosion_instance;
    private bool player_damaged = false;
    // Start is called before the first frame update
    void Start()
    {
        remaining_time_until_dissapear = time_until_dissapear;
        remaining_time_until_explosion_dissapear = time_until_explosion_dissapear = 1;
        Mob3_Sprite.sprite = idle;
    }

    // Update is called once per frame
    void Update()
    {
        if (initiate_explosion == 1)
        {
            remaining_time_until_dissapear -= Time.deltaTime;

            if (remaining_time_until_dissapear <= 0)
            {
                explosion_instance = Instantiate(explosion, this.transform.position, Quaternion.identity);
                Mob3_Sprite.enabled = false;
                initiate_explosion = 0;
                explosion_active = 1;
                remaining_time_until_dissapear = time_until_dissapear;
            }
        }
        if (explosion_active == 1)
        {
            remaining_time_until_explosion_dissapear -= Time.deltaTime;
            if (remaining_time_until_explosion_dissapear <= 0)
            {
                Destroy(this.gameObject);
                Destroy(explosion_instance.gameObject);
                initiate_explosion = 0;
                explosion_active = 1;
                remaining_time_until_explosion_dissapear = time_until_explosion_dissapear;
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
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //Debug.Log("DAAAA");
            if (explosion_active == 1 && player_damaged == false)
            {
                player_damaged = true;
                Debug.Log("ai luat damage!");
            }
        }
    }
}
