using System.Collections;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected float attackSpeed;

    [SerializeField] GameObject atStaffHeavyAttack;
    [SerializeField] GameObject atStaffBasicAttack;

    [SerializeField] private float dpsLifeDrain = 50f;
    [SerializeField] protected float hp;
    protected Transform hpBar;


    protected float initialAttackSpeed = 2f;
    protected float initialHP;
    private float initialScaleX;
    protected float attackCooldown;
    protected float timeSinceAttack;

    private float initialTimeLifeDrain = 4f;
    protected bool isBoss;
    private bool lifeDrain;
    protected bool pause;

    [SerializeField] protected float speed;
    private float timeLifeDrain;
    private float timeNextHit;
    protected Transform player;
    protected PlayerSpells playerSpells;
    protected PlayerResources playerResources;

    protected Vector3 spawnPosition;

    protected void Awake()
    {
        var components = gameObject.GetComponentsInChildren<Transform>();
        hpBar = components.FirstOrDefault(component => component.tag == "HP");
        lifeDrain = false;
        pause = false;
    }

    protected void Start()
    {
        spawnPosition = transform.position;
        attackSpeed = initialAttackSpeed;
        initialHP = hp;
        initialScaleX = hpBar.transform.localScale.x;
        timeSinceAttack = attackCooldown;
        playerSpells = FindObjectOfType<PlayerSpells>();
        playerResources = playerSpells.GetComponent<PlayerResources>();
        player = playerSpells.transform;
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

        var currWeaponHandler = FindObjectOfType<WeaponsHandler>();

        if (currWeaponHandler.currWeapon.type == Weapon.WeaponType.Staff &&
            currWeaponHandler.currWeapon.attackType == Weapon.AttackType.Basic)
        {
            var instBasic = Instantiate(atStaffBasicAttack, transform.position, Quaternion.identity, transform);
            Destroy(instBasic, 1f);
        }
        else if (currWeaponHandler.currWeapon.type == Weapon.WeaponType.Staff &&
            currWeaponHandler.currWeapon.attackType == Weapon.AttackType.Heavy)
        {
            var instHeavy = Instantiate(atStaffHeavyAttack, transform.position, Quaternion.identity, transform);
            Destroy(instHeavy, 1f);
        }

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
    }

    public void Debuff(int lvl)
    {
        var factor = 1f - 0.2f * lvl;
        attackSpeed *= factor;
        speed *= factor;
    }

    protected float GetDistanceFromPlayer()
    {
        return (player.position - transform.position).magnitude;
    }
}