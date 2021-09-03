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
        InAir,
        Land,
        Death
    }

    private Animator animatorBody;
    [SerializeField] private AnimationBody animationState;

    private JungleBossShield shield;
    private JungleBossSpitter[] spitters;

    private bool shieldActive;
    private bool spittersActive;

    private bool bossActive;
    private bool damageAnimation;
    private bool deathAnimation;

    public int spittersDeath;
    private float timeNextAttack;
    private float timeEndAttack;
    private bool isAttacking;

    private new void Awake()
    {
        base.Awake();
        animatorBody = GetComponent<Animator>();
        shield = GetComponents<JungleBossShield>()[0];
        spitters = GetComponents<JungleBossSpitter>();
        animationState = AnimationBody.Idle;
        shield.animationState = AnimationShield.Idle;
        foreach (var spitter in spitters)
        {
            spitter.animationState = AnimationSpitter.IdleBeginning;
        }
    }

    private new void Update()
    {
        base.Update();
        if (bossActive)
        {
            if (damageAnimation) return;
            
        }
        else if (spittersDeath == 4)
            bossActive = true;
    }

    public override void OnDamageTaken(float damage, bool isCrit)
    {
        base.OnDamageTaken(damage, isCrit);
        if (shieldActive)
            shield.animationState = AnimationShield.Damage;
        else
            animationState = AnimationBody.Death;
    }

    public void OnShieldDeath()
    {
        player.position += new Vector3(-50f, 5f);
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
    }

    public void EndDamageAnimation()
    {
        damageAnimation = false;
        animationState = AnimationBody.Idle;
    }
}
