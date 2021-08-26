using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected float attackSpeed;

    [SerializeField] GameObject atStaffHeavyAttack;
    [SerializeField] GameObject atStaffBasicAttack;
    GameObject atHeavyAttack;

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
    protected GameObject player;
    protected PlayerSpells playerSpells;

    protected void Awake()
    {
        var components = gameObject.GetComponentsInChildren<Transform>();
        hpBar = components.FirstOrDefault(component => component.tag == "HP");
        lifeDrain = false;
        pause = false;
        player = GameObject.FindGameObjectWithTag("Player");
        playerSpells = FindObjectOfType<PlayerSpells>().GetComponent<PlayerSpells>();
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
        // check in Weapon , two enums weapons type, attack type 
        // if weapon_type=staff si attack_type = basic
        // instatiem obiect 
        // else if weapon_type=staff si attack_type = heavy
        // instatiem alt obiect 
        // retinem instanta ca sa le distug dupa un delay

        if (Time.time < timeNextHit) return;

        WeaponsHandler currWeaponHandler = (WeaponsHandler)FindObjectOfType(typeof(WeaponsHandler));

        if (currWeaponHandler.currWeapon.type == Weapon.WeaponType.Staff &&
            currWeaponHandler.currWeapon.attackType == Weapon.AttackType.Basic)
        {
            GameObject instBasic = Instantiate(atStaffBasicAttack, transform.position, Quaternion.identity, transform);
            Destroy(instBasic, 1f);
        }
        else if (currWeaponHandler.currWeapon.type == Weapon.WeaponType.Staff &&
            currWeaponHandler.currWeapon.attackType == Weapon.AttackType.Heavy)
        {
            GameObject instHeavy = Instantiate(atStaffHeavyAttack, transform.position, Quaternion.identity, transform);
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
        Debug.Log("Lifedrain: " + lvl);
    }

    public void Debuff(int lvl)
    {
        var factor = 1f - 0.2f * lvl;
        attackSpeed *= factor;
        speed *= factor;
        Debug.Log("debuff: " + factor);
    }

    protected float GetDistanceFromPlayer()
    {
        return (player.transform.position - transform.position).magnitude;
    }
}