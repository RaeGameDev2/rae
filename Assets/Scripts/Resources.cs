using System.Collections;
using UnityEngine;

public class Resources : MonoBehaviour
{
    public int maxHealth { get; private set; }
    public int currentHealth;
    public int maxMana { get; private set; }
    public int currentMana { get; private set; }
    [SerializeField] private float manaRegenerationRate = 0.25f;
    [SerializeField] private float manaRegeneration;
    public int skillPoints = 10;

    private UI_Manager uiManager;
    private PlayerSpells spells;
    [SerializeField] private GameObject damageRae;

    private void Awake()
    {
        maxHealth = 3;
        maxMana = 3;
        spells = GetComponent<PlayerSpells>();
    }

    private void Start()
    {
        uiManager = FindObjectOfType<UI_Manager>();
        currentHealth = maxHealth;
        currentMana = maxMana;
    }

    private void Update()
    {
        if (currentMana >= maxMana) return;
        RegenerateMana();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        switch (collider.tag)
        {
            case "SkillPoint":
                skillPoints++;
                Destroy(collider.gameObject);
                break;
            case "HealthPoint" when currentHealth == maxHealth:
                return;
            case "HealthPoint":
                AddLife();
                Destroy(collider.gameObject);
                break;
        }
    }

    private void RegenerateMana()
    {
        manaRegeneration += manaRegenerationRate * Time.deltaTime;
        if (manaRegeneration < 1) return;
        manaRegeneration = 0;
        currentMana++;
        uiManager.AddMana();
    }

    public void TakeDamage(int damage, Vector3 enemyPosition)
    {
        if (spells.ParryActive)
        {
            spells.Shockwave();
            return;
        }

        var direction = transform.position - enemyPosition;
        direction = new Vector3(direction.x, 0f).normalized;
        if (direction != Vector3.zero)
        {
            var rb = GetComponent<Rigidbody2D>();
            rb.AddForce(2500f * direction, ForceMode2D.Force);
        }

        StartCoroutine(DamageAnimation());
        
        if (damage > currentHealth) damage = currentHealth;
        currentHealth -= damage;
        uiManager.TakeLives(damage);
    }

    private IEnumerator DamageAnimation()
    {
        var instance = Instantiate(damageRae, transform.position + new Vector3(0, 1f), Quaternion.identity, transform);
        var spriteRenderer = instance.GetComponent<SpriteRenderer>();
        var color = spriteRenderer.color;
        color.a = 0.5f;
        spriteRenderer.color = color;
        instance.transform.localScale *= 1.2f;

        var time = 1f;
        while (time > 0)
        {
            color.a -= Time.deltaTime / 2f;
            spriteRenderer.color = color;
            instance.transform.localScale *= 1.02f;

            time -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        Destroy(instance);
    }
    public void IncreaseMaxMana()
    {
        maxMana++;
    }

    public void IncreaseMaxHealth()
    {
        maxHealth++;
    }


    public void AddLife()
    {
        currentHealth++;
        uiManager.AddLife();
    }

    public void AddMana()
    {
        currentMana++;
        uiManager.AddMana();
    }

    public void UseMana() 
    {
        if (currentMana == 0)
            return;
        currentMana--;
        uiManager.UseMana();
    }

    public void AddSkillPoint()
    {
        skillPoints++;
    }
}
