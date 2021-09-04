using UnityEngine;

public class JungleBossSpitter : MonoBehaviour
{
    public Animator animatorSpitter;
    public JungleBoss.AnimationSpitter animationState;
    public bool active;
    public bool activated;
    public bool isDefending;
    public bool isAttacking;

    private JungleBoss parent;
    private void Awake()
    {
        parent = GetComponentInParent<JungleBoss>();
        animatorSpitter = GetComponent<Animator>();
    }
    public void OnDeath()
    {
        parent.spittersDeath++;
    }
}
