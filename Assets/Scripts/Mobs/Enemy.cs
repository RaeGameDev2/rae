using System.Collections;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private GameObject atStaffHeavyAttack;
    [SerializeField] private GameObject atStaffBasicAttack;
    [SerializeField] private GameObject atOrbHeavyAttack;
    [SerializeField] private GameObject atOrbBasicAttack;

    [SerializeField] protected Transform centerPiece;

    [SerializeField] private float dpsLifeDrain = 50f;
    [SerializeField] protected float hp;
    protected Transform hpBar;
    protected Transform squareHpBar;


    protected float initialHP;
    protected float initialScaleX;
    protected float attackCooldown;
    protected float timeSinceAttack;

    private float initialTimeLifeDrain = 4f;
    protected bool isBoss;
    private bool lifeDrain;
    protected bool pause;

    [SerializeField] protected float speed;
    [SerializeField] protected float attackSpeed;

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
        squareHpBar = components.FirstOrDefault(component => component.name == "Square");
        lifeDrain = false;
        pause = false;
    }

    protected void Start()
    {
        spawnPosition = transform.position;
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
        if (centerPiece == null)
            centerPiece = transform;
        if (currWeaponHandler.currWeapon.type == Weapon.WeaponType.Staff &&
            currWeaponHandler.currWeapon.attackType == Weapon.AttackType.Basic)
        {
            var instBasic = Instantiate(atStaffBasicAttack, centerPiece.transform.position, Quaternion.identity, transform);
            instBasic.transform.localScale = (isBoss ? 4f : 2f) * Vector3.one;
            Destroy(instBasic, 1f);
        }
        else if (currWeaponHandler.currWeapon.type == Weapon.WeaponType.Staff &&
            currWeaponHandler.currWeapon.attackType == Weapon.AttackType.Heavy)
        {
            var instHeavy = Instantiate(atStaffHeavyAttack, centerPiece.transform.position, Quaternion.identity, transform);
            instHeavy.transform.localScale = (isBoss ? 4f : 2f) * Vector3.one;
            Destroy(instHeavy, 1f);
        }

        if (currWeaponHandler.currWeapon.type == Weapon.WeaponType.Orb && currWeaponHandler.currWeapon.attackType == Weapon.AttackType.Basic)
        {
            var instBasic = Instantiate(atOrbBasicAttack, centerPiece.transform.position, Quaternion.identity, transform);
            instBasic.transform.localScale = (isBoss ? 4f : 2f) * Vector3.one;
            Destroy(instBasic, 1f);
        }
        else if (currWeaponHandler.currWeapon.type == Weapon.WeaponType.Orb && currWeaponHandler.currWeapon.attackType == Weapon.AttackType.Heavy)
        {
            var instHeavy = Instantiate(atOrbHeavyAttack, centerPiece.transform.position, Quaternion.identity, transform);
            instHeavy.transform.localScale = (isBoss ? 4f : 2f) * Vector3.one;
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