using UnityEngine;

public class Resources : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public float maxMana = 100;
    public float currentMana;
    public float manaAmount = 1f;
    public int skillPoints = 10;

    public HealthBar healthBar;
    public ManaBar manaBar;
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        currentMana = maxMana;
        manaBar.SetMaxMana(maxMana);
    }

    // Update is called once per frame
    void Update()
    {
        NormalizeMana();

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if (currentHealth > 0)
            {
                TakeDamage(20);
            }
            else currentHealth = 0;


        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (currentMana > 0)
            {
                TakeMana(20);
            }
            else currentMana = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.attachedRigidbody.tag == "SkillPoint")
        {
            skillPoints++;
        }

        if (collision.attachedRigidbody.tag == "HealthPoints")
        {

        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    void TakeMana(float mana)
    {
        currentMana -= mana;
        manaBar.SetMana(currentMana);
    }
    void NormalizeMana()
    {
        if (currentMana < 100)
        {
            currentMana += manaAmount * Time.deltaTime;
            manaBar.SetMana(currentMana);
        }
        else currentMana = 100;
    }
}
