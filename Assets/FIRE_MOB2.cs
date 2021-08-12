using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FIRE_MOB2 : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private SpriteRenderer Mob2_Sprite;
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private int damage = 1;
    [SerializeField] private float thresholdDistance = 10f;
    private float time_until_dissapear = 1;
    private float remaining_time_until_dissapear;
    private float time_until_explosion_dissapear = 1; // mod
    private float remaining_time_until_explosion_dissapear;
    private bool initiate_explosion;
    private bool exploded = false;
    private bool explosion_active;
    private ParticleSystem explosion_instance;
    private bool player_damaged = false;
    private Resources playerResources;
    void Start()
    {
        var components = gameObject.GetComponentsInChildren<Transform>();
        Mob2_Sprite = components.FirstOrDefault(component => component.name == "FireMob2_Temp").GetComponent<SpriteRenderer>();


        remaining_time_until_dissapear = time_until_dissapear;
        remaining_time_until_explosion_dissapear = time_until_explosion_dissapear = 1; // mod
        playerResources = FindObjectOfType<Resources>();
    }

    // Update is called once per frame
    void Update()
    {
        if (initiate_explosion)
        {
            remaining_time_until_dissapear -= Time.deltaTime;

            if (remaining_time_until_dissapear <= 0)
            {
                explosion_instance = Instantiate(explosion, transform.position, Quaternion.identity);
                explosion.transform.localScale *= 1f;
                Mob2_Sprite.enabled = false;
                initiate_explosion = false;
                exploded = true;
                explosion_active = true;
                remaining_time_until_dissapear = time_until_dissapear;
                if ((playerResources.transform.position - transform.position).magnitude < thresholdDistance)
                    playerResources.TakeDamage(damage, playerResources.transform.position);
            }
        }

        if (!explosion_active) return;

        remaining_time_until_explosion_dissapear -= Time.deltaTime;
        if (remaining_time_until_explosion_dissapear > 0) return;

        if ((playerResources.transform.position - transform.position).magnitude < thresholdDistance)
        {
            //Debug.Log("SALUT");
            playerResources.TakeDamage(damage, transform.position);
            Destroy(this.gameObject);
            Destroy(explosion_instance.gameObject);
        }
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.tag != "Player")
            return;
        else
        {
          
            transform.localScale = new Vector3(1.5f, 1.5f, 1f);
            if (exploded == false)
            {
                initiate_explosion = true;
            }
        }
       
    }
}
