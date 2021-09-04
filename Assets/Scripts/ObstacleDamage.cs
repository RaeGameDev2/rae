using UnityEngine;

public class ObstacleDamage : MonoBehaviour
{

    private PlayerResources playerResources;

    private void Start()
    {
        playerResources = FindObjectOfType<PlayerResources>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;

        if (FindObjectOfType<PlayerSpells>().phaseWalkActive) return;

        Debug.Log("Lava da damage!!!2");
        playerResources.TakeDamage(playerResources.maxHealth, transform.position);

        Debug.Log("Lava da damage!!!3");
    }
}
