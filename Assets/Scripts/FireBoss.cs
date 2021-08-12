using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class FireBoss : Enemy
{
    public enum AnimType
    {
        Idle,
        Projectile,
        Attack,
        Damage,
        Death
    }

    private Animator anim;
    private Resources playerResources;
    [SerializeField] public AnimType animType;
    [SerializeField] private float thresholdDistance;
    [SerializeField] private float oldHp;

    [SerializeField] private bool simpleAttack;
    [SerializeField] private bool projectileAttack;
    [SerializeField] private bool damageAnimation;
    [SerializeField] private bool isDying;

    [SerializeField] private AudioClip fireRealm;
    [SerializeField] private AudioClip bossFight;

    private new void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
        animType = AnimType.Idle;
    }

    private new void Start()
    {
        base.Start();
        playerResources = FindObjectOfType<Resources>();
        anim.SetInteger("state", (int)animType);
        hp = 1000f;
        initialHP = 1000f;
        isBoss = true;
        oldHp = 1000f;
    }

    private new void Update()
    {
        base.Update();
        CheckCamera();

        anim.SetInteger("state", (int)animType);
        if (isDying)
        {
            animType = AnimType.Death;
            return;
        }
        if (simpleAttack) return;
        if (projectileAttack) return;
        if (damageAnimation) return;

        if (Math.Abs(oldHp - hp) > 0.1f)
        {
            DamageAnimation();
            return;
        }
        oldHp = hp;

        if (hp <= 0)
            Die();
        if (GetDistanceToPlayer() < thresholdDistance)
        {
            animType = Random.Range(0f, 2f) < 1f ? AnimType.Attack : AnimType.Projectile;
            switch (animType)
            {
                case AnimType.Attack:
                    StartCoroutine(ActivateAttacking());
                    break;
                case AnimType.Projectile:
                    StartCoroutine(ActivateProjectile());
                    break;
            }
        }
    }

    private void CheckCamera()
    {
        if (GetDistanceToPlayer() < 40f)
        {
            var cam = Camera.main;
            cam.orthographicSize = 18f;
            cam.GetComponent<AudioSource>().clip = bossFight;
        }

        if (GetDistanceToPlayer() > 50f)
        {
            var cam = Camera.main;
            cam.orthographicSize = 10.8f;
            cam.GetComponent<AudioSource>().clip = fireRealm;
        }
    }
    private float GetDistanceToPlayer()
    {
        return (playerResources.transform.position - transform.position).magnitude;
    }

    private void Die()
    {
        isDying = true;
        animType = AnimType.Death;
        anim.SetInteger("state", (int)animType);

        Destroy(hpBar.gameObject, 1f);
        Destroy(gameObject.GetComponentsInChildren<Transform>()?.FirstOrDefault(component => component.name == "Square")?.gameObject, 1f);
        Destroy(gameObject, 2f);
    }

    private IEnumerator ActivateAttacking()
    {
        simpleAttack = true;
        yield return new WaitForSeconds(2f);
        if (!isDying)
            animType = AnimType.Attack;
    }

    public void AttackEnd()
    {
        simpleAttack = false;
        if (!isDying)
            animType = AnimType.Idle;
    }
    private IEnumerator ActivateProjectile()
    {
        projectileAttack = true;
        yield return new WaitForSeconds(2f);

        if (isDying) yield break;
        animType = AnimType.Projectile;
    }

    public void ProjectileEnd()
    {
        projectileAttack = false;
        if (!isDying)
            animType = AnimType.Idle;
    }

    private void DamageAnimation()
    {
        damageAnimation = true;
        animType = AnimType.Damage;
    }

    public void DamageEnd()
    {
        damageAnimation = false;
        oldHp = hp;
        if (!isDying)
            animType = AnimType.Idle;
    }

    public new void OnDamageTaken(float damage, bool isCrit)
    {
        if (isDying) return;
        base.OnDamageTaken(damage, isCrit);
    }
}
