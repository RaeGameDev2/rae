using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float attackSpeed;

    [SerializeField] private float dpsLifeDrain = 50f;
    [SerializeField] protected float hp;
    protected Transform hpBar;


    [SerializeField] private float initialAttackSpeed = 2f;
    [SerializeField] protected float initialHP;
    [SerializeField] private float initialScaleX;
    [SerializeField] protected float attackCooldown;
    [HideInInspector] protected float timeSinceAttack;

    [SerializeField] private float initialTimeLifeDrain = 4f;
    [SerializeField] protected bool isBoss;
    private bool lifeDrain;
    protected bool pause;
    [SerializeField] protected float speed;
    private float timeLifeDrain;
    private float timeNextHit;

    protected void Awake()
    {
        var components = gameObject.GetComponentsInChildren<Transform>();
        hpBar = components.FirstOrDefault(component => component.tag == "HP");
        lifeDrain = false;
        pause = false;
    }

    protected void Start()
    {
        attackSpeed = initialAttackSpeed;
        initialHP = hp;
        initialScaleX = hpBar.transform.localScale.x;
        timeSinceAttack = attackCooldown;
    }

    protected void Update()
    {
        if (!lifeDrain) return;
        timeLifeDrain -= Time.deltaTime;
        if (timeLifeDrain <= 0)
        {
            lifeDrain = false;
            timeLifeDrain = 0;
        }
        else
        {
            hp -= dpsLifeDrain * Time.deltaTime;
            hp = Mathf.Clamp(hp, 0f, initialHP);
            hpBar.localScale = new Vector3(hp / initialHP * initialScaleX, hpBar.localScale.y, hpBar.localScale.z);

            if (hp > 0) return;
            hp = 0;
            if (!isBoss)
                Destroy(gameObject, 1.5f);
        }
    }

    public virtual void OnDamageTaken(float damage, bool isCrit)
    {
        if (Time.time < timeNextHit) return;

        timeNextHit = Time.time + 1f;
        hp -= damage;
        hp = Mathf.Clamp(hp, 0f, initialHP);
        hpBar.localScale = new Vector3(hp / initialHP * initialScaleX, hpBar.localScale.y, hpBar.localScale.z);

        DamagePopup.Create(transform.position, (int)damage, isCrit);
        if (hp > 0) return;
        hp = 0;
        if (!isBoss)
            Destroy(gameObject, 1.5f);
    }

    public void LifeDrain(int lvl)
    {
        lifeDrain = true;
        timeLifeDrain = initialTimeLifeDrain + lvl;
        Debug.Log("Lifedrain: " + lvl);
    }

    public void Debuff(int lvl)
    {
        var factor = 1f - 0.2f * lvl;
        attackSpeed *= factor;
        speed *= factor;
        Debug.Log("debuff: " + factor);
    }
}