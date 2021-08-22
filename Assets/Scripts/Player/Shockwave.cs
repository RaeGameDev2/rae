using UnityEngine;

public class Shockwave : MonoBehaviour
{
    private float albedo;
    private Color color;
    [SerializeField] private float damagePerLevel;

    [SerializeField] private float duration = 2f;
    private float initialAlbedo = 0.5f;
    [SerializeField] private float initialDamage;
    private float initialPositionY;
    private int levelParry;
    private float maxScale = 15;
    private float scale;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        albedo = initialAlbedo;
        scale = transform.localScale.x;
        initialPositionY = transform.position.y;
        spriteRenderer = GetComponent<SpriteRenderer>();
        color = spriteRenderer.color;
        levelParry = 0;
        initialDamage = 50;
        damagePerLevel = 20;
    }

    private void Update()
    {
        albedo -= initialAlbedo * Time.deltaTime / duration;
        scale += maxScale * Time.deltaTime / duration;
        color.a = albedo;
        spriteRenderer.color = color;
        transform.localScale = scale * Vector3.one;
        transform.position = new Vector3(transform.position.x, initialPositionY + scale / 4);
        if (albedo <= 0)
            Destroy(gameObject, 0.2f);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag != "Enemy") return;
        col.GetComponent<Enemy>()?.OnDamageTaken(initialDamage + levelParry * damagePerLevel, false);
    }

    public void SetLevelParry(int lvl)
    {
        levelParry = lvl;
    }
}