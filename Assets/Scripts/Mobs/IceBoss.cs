using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class IceBoss : Enemy
{
    public enum AnimType
    {
        Idle,
        Attack,
        Projectile,
        Death
    }

    private Animator anim;
    [SerializeField] public AnimType animType;
    [SerializeField] private AudioClip bossFightClip;
    [SerializeField] private AudioClip iceRealmClip;
    private float initialLocalScaleX;
    [SerializeField] private bool isDying;
    [SerializeField] private GameObject projectile;
    [SerializeField] private bool projectileAttack;

    [SerializeField] private bool simpleAttack;
    [SerializeField] private float thresholdDistance = 10f;

    private new void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
        animType = AnimType.Idle;
        initialLocalScaleX = transform.localScale.x;
    }

    private new void Start()
    {
        base.Start();
        playerResources = FindObjectOfType<PlayerResources>();
        playerSpells = FindObjectOfType<PlayerSpells>();
        anim.SetInteger("state", (int) animType);
        initialHP = hp;
        isBoss = true;
    }

    private new void Update()
    {
        base.Update();
        CheckCamera();
        CheckOrientation();

        anim.SetInteger("state", (int) animType);
        if (isDying)
        {
            animType = AnimType.Death;
            return;
        }

        if (simpleAttack) return;
        if (projectileAttack) return;

        if (hp <= 0)
            Die();

        if (GetDistanceToPlayer() < thresholdDistance && !playerSpells.phaseWalkActive)
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

    private void CheckOrientation()
    {
        var deltaX = transform.position.x - playerResources.transform.position.x;
        const float threshold = 6f;
        if (deltaX > threshold)
            transform.localScale = new Vector3(initialLocalScaleX, transform.localScale.y, transform.localScale.z);
        if (deltaX < -threshold)
            transform.localScale = new Vector3(-initialLocalScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void CheckCamera()
    {
        if (GetDistanceFromPlayer() < 100f)
        {
            var cam = Camera.main;
            if (Math.Abs(cam.orthographicSize - 18f) < 0.1f)
                return;
            cam.orthographicSize = 21.6f;
            GameObject.Find("Layer1").transform.localScale = new Vector3(2, 2);
            cam.GetComponent<AudioSource>().clip = bossFightClip;
            cam.GetComponent<AudioSource>().Play();
        }

        if (GetDistanceFromPlayer() > 150f)
        {
            var cam = Camera.main;
            if (Math.Abs(cam.orthographicSize - 10.8f) < 0.1f)
                return;
            cam.orthographicSize = 10.8f;
            GameObject.Find("Layer1").transform.localScale = new Vector3(1, 1);
            cam.GetComponent<AudioSource>().clip = iceRealmClip;
            cam.GetComponent<AudioSource>().Play();
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
        anim.SetInteger("state", (int) animType);

        Destroy(hpBar.gameObject, 1f);
        Destroy(
            gameObject.GetComponentsInChildren<Transform>()?.FirstOrDefault(component => component.name == "Square")
                ?.gameObject, 1f);
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
        Instantiate(projectile, transform.position, Quaternion.identity, transform);
    }

    public void ProjectileEnd()
    {
        projectileAttack = false;
        if (!isDying)
            animType = AnimType.Idle;
    }

    [SerializeField] private GameObject corePrefab;

    public void EndDeathAnimation()
    {
        Instantiate(corePrefab, transform.position + new Vector3(-15.02349f, 2.910582f), Quaternion.identity);
        Destroy(gameObject);
    }

    public new void OnDamageTaken(float damage, bool isCrit)
    {
        if (isDying) return;
        base.OnDamageTaken(damage, isCrit);
    }
}