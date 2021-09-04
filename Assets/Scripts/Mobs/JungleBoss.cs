using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JungleBoss : Enemy
{
    private enum AnimationBody
    {
        Idle,
        Damage,
        Death
    }

    public enum AnimationShield
    {
        Idle,
        Damage,
        Death
    }

    public enum AnimationSpitter
    {
        IdleBeginning,
        Idle,
        Jump,
        // InAir,
        // Land,
        Death
    }

    private Animator animatorBody;
    [SerializeField] private AnimationBody animationState;
    [SerializeField] private GameObject projectilePrefab;

    private JungleBossShield shield;
    private JungleBossSpitter[] spitters;

    private bool shieldActive;
    private bool spittersActive;
    public bool shieldDamageAnimation;

    private bool bossActive;
    private bool damageAnimation;
    private bool deathAnimation;

    public int spittersDeath;
    private float timeNextAttack;
    private float timeEndAttack;
    private bool isAttacking;

    private float timeBetweenAttacks = 3f;
    private float durationAttack = 3f;
    private Transform coreTransform;

    private new void Awake()
    {
        Debug.Log("Awake Jungle Boss");
        base.Awake();
        animatorBody = GetComponent<Animator>();
        shield = GetComponentInChildren<JungleBossShield>();
        spitters = GetComponentsInChildren<JungleBossSpitter>();
        Debug.Log($"{spitters} {spitters.Length}");
        
        animationState = AnimationBody.Idle;
        shield.animationState = AnimationShield.Idle;
        foreach (var spitter in spitters)
        {
            spitter.animationState = AnimationSpitter.IdleBeginning;
            spitter.animatorSpitter.SetFloat("speed", speed);
        }

        animatorBody.SetFloat("speed", speed);
        shield.animatorShield.SetFloat("speed", speed);
        
        shieldActive = true;
        isBoss = true;
    }

    private new void Start()
    {
        Debug.Log("Start Jungle Boss");
        base.Start();
    }

    private new void Update()
    {
        base.Update();
        if (bossActive)
        {
            if (damageAnimation) return;
            if (isAttacking) return;
            if (Time.time >= timeNextAttack)
                StartCoroutine(Attack());
        }
        else
        {
            if (spittersDeath == 4)
                bossActive = true;
        }
    }

    private IEnumerator Attack()
    {
        timeEndAttack = Time.time + durationAttack;
        var timeNextProjectile = Time.time;
        while (Time.time > timeEndAttack)
        {
            yield return new WaitForFixedUpdate();
            if (Time.time >= timeNextProjectile)
            {
                Instantiate(projectilePrefab, coreTransform.position, Quaternion.identity);
                timeNextProjectile = Time.time;
            }
        }

        timeNextAttack = Time.time + timeBetweenAttacks;
    }

    public override void OnDamageTaken(float damage, bool isCrit)
    {
        base.OnDamageTaken(damage, isCrit);
        if (shieldActive)
        {
            if (hp <= 0)
            {
                shield.animationState = AnimationShield.Death;
                player.position += new Vector3(-20f, 5f);
                StartCoroutine(ActivateSpitters());
                return;
            }

            shieldDamageAnimation = true;
            shield.animationState = AnimationShield.Damage;
        }
        else
            animationState = AnimationBody.Death;
    }

    private IEnumerator ActivateSpitters()
    {
        yield return new WaitForSeconds(1f);
        spittersActive = true;
        foreach (var spitter in spitters)
        {
            spitter.active = true;
        }
    }

    public void OnShieldDestroy()
    {
        hp = initialHP;
        GetComponent<BoxCollider2D>().size = new Vector2(10f, 16f);
    }

    public void EndDamageAnimation()
    {
        damageAnimation = false;
        animationState = AnimationBody.Idle;
    }
}