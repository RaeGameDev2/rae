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

    public enum AnimatorShield
    {
        Idle,
        Damage,
        Death
    }

    public enum AnimatorSpitter
    {
        IdleBeginning,
        Idle,
        Jump,
        InAir,
        Land,
        Death
    }

    private Animator animatorBody;
    private JungleBossShield shield;
    private JungleBossSpitter[] spitters;


    private new void Awake()
    {
        animatorBody = GetComponent<Animator>();
    }
}
