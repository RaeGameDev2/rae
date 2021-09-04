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
        
        playerResources.TakeDamage(playerResources.maxHealth, transform.position);
    }
}
