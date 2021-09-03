using UnityEngine;

public class ProjectileJungleBoss : MonoBehaviour
{
    private Vector3 target;

    private const float speed = 20f;

    private void Start()
    {
        target = FindObjectOfType<PlayerController>().transform.position;
        target += (transform.position - target).normalized * 10f;
    }

    private void FixedUpdate()
    {
        transform.position += (transform.position - target).normalized * speed * Time.timeScale;
        if ((transform.position - target).magnitude < 0.1f) 
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.tag != "Player")
        {
            Destroy(gameObject, 0.2f);
            return;
        }
        collider2D.GetComponent<PlayerResources>().TakeDamage(1, transform.position);
    }
}
