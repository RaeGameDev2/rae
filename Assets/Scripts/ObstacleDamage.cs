using UnityEngine;

public class ObstacleDamage : MonoBehaviour
{

    private PlayerResources playerResources;
    private PlayerSpells playerSpells;

    private void Start()
    {
        playerResources = FindObjectOfType<PlayerResources>();
        playerSpells = FindObjectOfType<PlayerSpells>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;

        if (FindObjectOfType<PlayerSpells>().phaseWalkActive) return;
        if (playerSpells.shieldActive)
        {
            playerSpells.shieldDamage = 0;
            col.GetComponent<PlayerController>().activateDeath();
        }
        playerResources.TakeDamage(playerResources.maxHealth, transform.position);
    }
}
