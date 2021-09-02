using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class JungleMob3 : Enemy
{
    private enum AnimType
    {
        Idle,
        Damage,
        Death
    }

    private Tentacles tentaclesLeft;
    private Tentacles tentaclesRight;
    private Tentacles[] tentaclesArray;
    private Animator anim;

    private AnimType animType;
    private float animSpeed;

    public bool playerInRange;
    private bool damageAnimation;
    private bool isAttacking;

    [SerializeField] private float thresholdDistance = 10f;

    private new void Awake()
    {
        base.Awake();

        animType = AnimType.Idle;
        animSpeed = speed / 5;

        tentaclesArray = GetComponentsInChildren<Tentacles>(true);
        tentaclesLeft = tentaclesArray.FirstOrDefault(child => child.name == "Tentacles Left");
        tentaclesRight = tentaclesArray.FirstOrDefault(child => child.name == "Tentacles Right");

        anim = GetComponent<Animator>();
    }

    [SerializeField] private float dist;
    private  new void Update()
    {
        base.Update();
        dist = GetDistanceFromPlayer();
        if (hp <= 0) return;
        if (damageAnimation) return;

        anim.SetInteger("state", (int)animType);
        anim.SetFloat("speed", animSpeed);

        if (GetDistanceFromPlayer() < thresholdDistance)
        {
            Debug.Log("in range");
            if (!playerInRange)
                foreach (var tentacles in tentaclesArray)
                    tentacles.Attack();
            Debug.Log("in rage true");
            playerInRange = true;
        }

        if (GetDistanceFromPlayer() > 2f * thresholdDistance)
            playerInRange = false;
    }

    public override void OnDamageTaken(float damage, bool isCrit)
    {
        base.OnDamageTaken(damage, isCrit);
        if (hp > 0)
        {
            damageAnimation = true;
            animType = AnimType.Damage;
        }
        else animType = AnimType.Death;
    }

    public float GetSpeed()
    {
        return attackSpeed;
    }

    public void EndDamageAnimation()
    {
        damageAnimation = false;
        animType = AnimType.Idle;
    }
}
