using System.Collections;
using UnityEngine;

public class ProjectileJungleBoss : MonoBehaviour
{
    private Vector3 target;

    private const float speed = 20f;
    private Vector3 initialPosition;
    private void Start()
    {
        target = FindObjectOfType<PlayerController>().transform.position;
        target += (target - transform.position).normalized * 10f;
        StartCoroutine(Die());
        initialPosition = transform.position;
    }

    private void FixedUpdate()
    {
        transform.position += (target - initialPosition).normalized * speed * Time.fixedDeltaTime;
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

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
