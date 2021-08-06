using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private float initialAlbedo = 0.5f;
    private float maxScale = 15;
    private float initialPositionY;
    private float scale;
    private float albedo;
    private Color color;
    private int levelParry;
    
    [SerializeField] private float duration = 2f;
    [SerializeField] private float initialDamage;
    [SerializeField] private float damagePerLevel;

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

    void Update()
    {
        albedo -= initialAlbedo * Time.deltaTime / duration;
        scale += maxScale * Time.deltaTime / duration;
        color.a = albedo;
        spriteRenderer.color = color;
        transform.localScale = scale * Vector3.one;
        transform.position = new Vector3(transform.position.x, initialPositionY + scale / 4);
        if (albedo <= 0)
            Destroy(gameObject, 2f);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            col.GetComponent<Enemy>().OnDamageTaken(initialDamage + levelParry * damagePerLevel);
        }
    }

    public void SetLevelParry(int lvl)
    {
        levelParry = lvl;
    }
}
