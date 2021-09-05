using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Cinemachine;
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
    [SerializeField] private GameObject corePrefab;

    private new void Awake()
    {
        base.Awake();
        animatorBody = GetComponent<Animator>();
        shield = GetComponentInChildren<JungleBossShield>();
        spitters = GetComponentsInChildren<JungleBossSpitter>();

        animationState = AnimationBody.Idle;
        shield.animationState = AnimationShield.Idle;

        shieldActive = true;
        isBoss = true;
    }

    private new void Start()
    {
        base.Start();
        animatorBody.SetFloat("speed", speed);
        shield.animatorShield.SetFloat("speed", speed);

        foreach (var spitter in spitters)
        {
            spitter.animationState = AnimationSpitter.IdleBeginning;
            spitter.animatorSpitter.SetFloat("speed", speed);
        }
    }

    private new void Update()
    {
        base.Update();
        CheckCamera();

        animatorBody.SetInteger("state", (int)animationState);

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

    private void CheckCamera()
    {
        var cam = GameObject.Find("CameraMachine").GetComponent<CinemachineVirtualCamera>();

        if (GetDistanceFromPlayer() < 70f)
        {
            if (Math.Abs(cam.m_Lens.OrthographicSize - 21.6f) < 0.1f)
                return;
            cam.m_Lens.OrthographicSize = 21.6f;
            GameObject.Find("Layer1").transform.localScale = new Vector3(2, 2);
            SoundManagerScript.instance.PlayBossMusic();
        }

        if (GetDistanceFromPlayer() > 100f)
        {
            if (Math.Abs(cam.m_Lens.OrthographicSize - 10.8f) < 0.1f)
                return;
            cam.m_Lens.OrthographicSize = 10.8f;
            GameObject.Find("Layer1").transform.localScale = new Vector3(1, 1);
            SoundManagerScript.instance.PlayMusic();
        }
    }

    private IEnumerator Attack()
    {
        isAttacking = true;
        timeEndAttack = Time.time + durationAttack;
        for (var i = 0; i < 7; i++)
        {
            Instantiate(projectilePrefab, centerPiece.position, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }

        isAttacking = false;
        timeNextAttack = Time.time + timeBetweenAttacks;
    }

    public override void OnDamageTaken(float damage, bool isCrit)
    {
        if (hp <= 0) return;

        base.OnDamageTaken(damage, isCrit);
        if (shieldActive)
        {
            if (hp <= 0)
            {
                shield.animationState = AnimationShield.Death;
                player.position += new Vector3(-20f, 5f);
                return;
            }

            shieldDamageAnimation = true;
            shield.animationState = AnimationShield.Damage;
        }
        else
        {
            if (hp <= 0)
            {
                animationState = AnimationBody.Death;
                StopAllCoroutines();
                return;
            }
            animationState = AnimationBody.Damage;
            damageAnimation = true;
        }
    }

    public void OnShieldDestroy()
    {
        shieldActive = false;
        hp = initialHP;
        hp = Mathf.Clamp(hp, 0f, initialHP);
        hpBar.localScale = new Vector3(hp / initialHP * initialScaleX, hpBar.localScale.y, hpBar.localScale.z);
        GetComponent<BoxCollider2D>().size = new Vector2(10f, 16f);
        bossActive = true;
        // spittersActive = true;
        // foreach (var spitter in spitters)
        // {
        //     spitter.activated = true;
        // }

        // Destroy(GetComponentsInChildren<Transform>().First(child => child.name == "SupportSpitters").gameObject, 0.1f);
    }

    public void EndDamageAnimation()
    {
        damageAnimation = false;
        animationState = AnimationBody.Idle;
    }
    public void EndDeathAnimation()
    {
        Instantiate(corePrefab, transform.position + new Vector3(-4.45f, 10f), Quaternion.identity);
        Destroy(gameObject);
    }
}