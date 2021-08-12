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
    private float time_until_explosion_dissapear = 1; // mod
    private float remaining_time_until_explosion_dissapear;
    private bool initiate_explosion;
    private bool exploded = false;
    private bool explosion_active;
    private ParticleSystem explosion_instance;
    private bool player_damaged = false;
    private Resources playerResources;
    
    private void Start()
    {
        remaining_time_until_dissapear = time_until_dissapear;
        remaining_time_until_explosion_dissapear = time_until_explosion_dissapear = 1; // mod
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
                explosion.transform.localScale *= 1f;
                Mob3_Sprite.enabled = false;
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
            return;
        else
        {
            Mob3_Sprite.sprite = active;
            if (exploded == false)
            {
                initiate_explosion = true;
            }
        }

    }
}
