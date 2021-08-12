using System.Collections;
using System.Linq;
using UnityEngine;

public class IceFinalBoss : Enemy
{
    private enum AnimType
    {
        Idle,
        Attack,
        Projectile,
        Death
    }
    
    private Animator anim;
    private Resources playerResources;
    [SerializeField] private AnimType animType;
    [SerializeField] private float thresholdDistance = 10f;

    [SerializeField] private bool simpleAttack;
    [SerializeField] private bool projectileAttack;
    [SerializeField] private bool isDying;
    [SerializeField] private float timeOfAttack;
    [SerializeField] private float timeOfProjectile;

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
        hp = 5000f;
        isBoss = true;
    }

    private new void Update()
    {
        base.Update();

        // anim.SetInteger("state", (int)animType);
        if (isDying) {
            animType = AnimType.Death; 
            return;
        }
        if (simpleAttack) return;
        if (projectileAttack) return;

        if (hp <= 0)
            Die();

        if (GetDistanceToPlayer() < thresholdDistance)
        {
            animType = Random.Range(0f, 2f) < 1f ? AnimType.Attack : AnimType.Projectile;
            switch (animType)
            {
                case AnimType.Attack:
                    StartCoroutine(StopAttacking());
                    break;
                case AnimType.Projectile:
                    StartCoroutine(StopProjectile());
                    break;
            }
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

    private IEnumerator StopAttacking()
    {
        simpleAttack = true;
        yield return new WaitForSeconds(1f);
        if (!isDying)
            animType = AnimType.Attack;
        yield return new WaitForSeconds(2f);
        simpleAttack = false;
        if (!isDying)
            animType = AnimType.Idle;
    }

    private IEnumerator StopProjectile()
    {
        projectileAttack = true;
        yield return new WaitForSeconds(1f);
        if (!isDying)
            animType = AnimType.Projectile;
        yield return new WaitForSeconds(2f);
        projectileAttack = false;
        if (!isDying)
            animType = AnimType.Idle;
    }

    public new void OnDamageTaken(float damage, bool isCrit)
    {
        if (isDying) return;
        base.OnDamageTaken(damage, isCrit);
    }
}
