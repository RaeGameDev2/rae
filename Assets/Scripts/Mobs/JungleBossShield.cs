using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JungleBossShield : MonoBehaviour
{
    public Animator animatorShield;
    public JungleBoss.AnimationShield animationState;
    private JungleBoss parent;

    private void Awake()
    {
        parent = GetComponentInParent<JungleBoss>();
        animatorShield = GetComponent<Animator>();
        
    }

    private void Update()
    {
        animatorShield.SetInteger("state", (int)animationState);
    }

    public void EndDamageAnimation()
    {
        animationState = JungleBoss.AnimationShield.Idle;
    }

    public void EndDeathAnimation()
    {
        Destroy(gameObject);
    }
}
