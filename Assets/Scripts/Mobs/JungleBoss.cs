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

    private bool ShieldActive;


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
    }


}
