using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacles : MonoBehaviour
{
    private enum AnimType
    {
        Grow,
        Attack,
        Decent
    }

    private JungleMob3 parent;

    private Animator anim;
    private AnimType animType;
    private float animSpeed;

    private bool growAnimation;
    private bool attackAnimation;
    private bool decentAnimation;

    private void Awake()
    {
        parent = GetComponentInParent<JungleMob3>();
        animSpeed = parent.GetSpeed();
    }



    public void EndGrowAnimation()
    {
        growAnimation = false;
    }
}
