using UnityEngine;

public class TeleportDamageEnemy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Enemy")) return;
        col.GetComponent<Enemy>().OnDamageTaken(1000f, true);
    }
}
