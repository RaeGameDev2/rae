using UnityEngine;

public class IceMob3 : MonoBehaviour
{
    [SerializeField] private  int damage = 1;
    [SerializeField] private  float thresholdDistance = 10f;
    private  float time_until_dissapear = 1;
    [SerializeField] private Sprite active;
    private bool exploded;
    [SerializeField] private ParticleSystem explosion;
    private bool explosion_active;
    private ParticleSystem explosion_instance;
    [SerializeField] private Sprite idle;
    private bool initiate_explosion;
    [SerializeField] private SpriteRenderer Mob3_Sprite;
    private PlayerResources playerResources;
    private float remaining_time_until_dissapear;
    private float remaining_time_until_explosion_dissapear;

    private void Start()
    {
        remaining_time_until_dissapear = time_until_dissapear;
        remaining_time_until_explosion_dissapear = 1;
        Mob3_Sprite.sprite = idle;
        playerResources = FindObjectOfType<PlayerResources>();
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

        if ((playerResources.transform.position - transform.position).magnitude > thresholdDistance) return;
        playerResources.TakeDamage(damage, transform.position);
        Destroy(gameObject);
        Destroy(explosion_instance.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;
        if (FindObjectOfType<PlayerSpells>().phaseWalkActive) return;

        Mob3_Sprite.sprite = active;
        if (exploded == false) initiate_explosion = true;
    }
}