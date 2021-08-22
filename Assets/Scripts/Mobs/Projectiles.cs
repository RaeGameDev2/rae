using UnityEngine;

public class Projectiles : MonoBehaviour
{
    private float albedo;
    private Color color;

    [SerializeField] private bool damagedPlayer;

    [SerializeField] private  float duration = 1.4f;
    private  float initialAlbedo = 0.5f;
    private  float maxScale = 25;
    private float scale;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        albedo = initialAlbedo;
        scale = transform.localScale.x;
        spriteRenderer = GetComponent<SpriteRenderer>();
        color = spriteRenderer.color;
    }

    private void Update()
    {
        albedo -= initialAlbedo * Time.deltaTime / duration;
        scale += maxScale * Time.deltaTime / duration;
        color.a = albedo;
        spriteRenderer.color = color;
        transform.localScale = scale * Vector3.one;
        // transform.position = new Vector3(transform.position.x, initialPositionY + scale / 4);
        if (albedo <= 0)
            Destroy(gameObject, 0.2f);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag != "Player") return;
        if (damagedPlayer) return;
        col.GetComponent<PlayerResources>()?.TakeDamage(1, transform.position);
        damagedPlayer = true;
        var rb = col.GetComponent<Rigidbody2D>();
        rb.AddForce(10000f * Vector2.left, ForceMode2D.Force);
    }
}