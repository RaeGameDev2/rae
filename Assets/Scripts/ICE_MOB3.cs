using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ICE_MOB3 : MonoBehaviour
{
    [SerializeField] private SpriteRenderer Mob3_Sprite;
    [SerializeField] private Sprite idle;
    [SerializeField] private Sprite active;
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private int damage = 1;
    [SerializeField] private float thresholdDistance = 10f;
    private float time_until_dissapear = 1;
    private float remaining_time_until_dissapear;
    private float time_until_explosion_dissapear = 1;
    private float remaining_time_until_explosion_dissapear;
    private bool initiate_explosion;
    private bool explosion_active;
    private ParticleSystem explosion_instance;
    private bool player_damaged = false;
    private Resources playerResources;
    
    private void Start()
    {
        remaining_time_until_dissapear = time_until_dissapear;
        remaining_time_until_explosion_dissapear = time_until_explosion_dissapear = 1;
        Mob3_Sprite.sprite = idle;
        playerResources = FindObjectOfType<Resources>();
    }
    
    private void Update()
    {
        if (initiate_explosion)
        {
            remaining_time_until_dissapear -= Time.deltaTime;

            if (remaining_time_until_dissapear <= 0)
            {
                explosion_instance = Instantiate(explosion, transform.position, Quaternion.identity);
                explosion.transform.localScale *= 5f;
                Mob3_Sprite.enabled = false;
                initiate_explosion = false;
                explosion_active = true;
                remaining_time_until_dissapear = time_until_dissapear;
                if ((playerResources.transform.position - transform.position).magnitude < thresholdDistance)
                    playerResources.TakeDamage(damage);
            }
        }

        if (!explosion_active) return;
        
        remaining_time_until_explosion_dissapear -= Time.deltaTime;
        if (remaining_time_until_explosion_dissapear > 0) return;

        if ((playerResources.transform.position - transform.position).magnitude < thresholdDistance)
            playerResources.TakeDamage(damage);
        Destroy(explosion_instance.gameObject);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;
        Mob3_Sprite.sprite = active;
        initiate_explosion = true;
    }

    // private void OnTriggerStay2D(Collider2D collision)
    // {
    //     if (collision.tag == "Player")
    //     {
    //         if (explosion_active && player_damaged == false)
    //         {
    //             player_damaged = true;
    //             collision.GetComponent<Resources>().TakeDamage(damage);
    //         }
    //     }
    // }
}
