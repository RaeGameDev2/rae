using System.Collections;
using System.Linq;
using UnityEngine;

public class PlayerResources : MonoBehaviour
{
    public int currentHealth;
    [SerializeField] private GameObject damageRae;
    [SerializeField] private float manaRegeneration;
    private const float manaRegenerationRate = 0.1f;

    private bool pause;
    public int skillPoints = 10;
    private PlayerSpells spells;

    private UI_Manager uiManager;
    public int maxHealth;
    public int maxMana;
    public int currentMana;

    private GameManager gameManager;

    private void Awake()
    {
        spells = GetComponent<PlayerSpells>();
    }

    private void Start()
    {
        uiManager = FindObjectOfType<UI_Manager>();

        gameManager = FindObjectsOfType<GameManager>().FirstOrDefault(manager => manager.isDontDestroyOnLoad);
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

    public void Pause()
    {
        pause = !pause;
    }

    // Summary:
    //   daca enemyPosition este egal cu playerPosition nu se face pushBack
    public void TakeDamage(int damage, Vector3 enemyPosition)
    {
        if (spells.phaseWalkActive)
            return;
        if (spells.parryActive)
        {
            spells.Shockwave();
            return;
        }

        if (spells.shieldActive)
        {
            spells.shieldDamage--;
            return;
        }

        var direction = transform.position - enemyPosition;
        direction = new Vector3(direction.x, 0f).normalized;
        if (direction != Vector3.zero)
        {
            StartCoroutine(DamageKnockback(direction));
        }

        StartCoroutine(DamageAnimation());

        if (damage > currentHealth) damage = currentHealth;

        if (damage == currentHealth)
        {
            uiManager.TakeLives(currentHealth);

            for (int i = 0; i < currentMana; i++)
                uiManager.UseMana();

            StartCoroutine(transform.GetComponent<PlayerController>().activateDeath());
        }
        else
        {
            currentHealth -= damage;
            uiManager.TakeLives(damage);
        }
    }

    private IEnumerator DamageKnockback(Vector3 dir)
    {
        var rb = GetComponent<Rigidbody2D>();
        var time = 0.40f;
        while (time > 0)
        {
            rb.AddForce(1000f * dir, ForceMode2D.Force);
            time -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
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

            time -= Time.deltaTime;
            yield return null;
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