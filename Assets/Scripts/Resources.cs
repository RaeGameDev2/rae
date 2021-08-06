using UnityEngine;
using UnityEngine.UI;

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
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            TakeDamage(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            AddLife();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            UseMana();
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            currentMana++;
            uiManager.AddMana();
        }

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

    public void IncreaseMaxMana()
    {
        maxMana++;
    }

    public void IncreaseMaxHealth()
    {
        maxHealth++;
    }

    public void TakeDamage(int damage)
    {
        if (spells.ParryActive)
        {
            spells.Shockwave();
            return;
        }

        if (damage > currentHealth) damage = currentHealth;
        currentHealth -= damage;
        uiManager.TakeLives(damage);
    }

    public void AddLife()
    {
        currentHealth++;
        uiManager.AddLife();
    }
    public void UseMana() 
    {
        if (currentMana == 0)
            return;
        currentMana--;
        uiManager.UseMana();
    }
}
