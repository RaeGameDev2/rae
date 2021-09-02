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
    private Animator anim;

    private AnimType animType;
    private float animSpeed;

    public bool playerInRange;

    [SerializeField] private float thresholdDistance = 10f;

    private new void Awake()
    {
        base.Awake();

        animType = AnimType.Idle;
        animSpeed = speed;

        var tentaclesArray = GetComponentsInChildren<Tentacles>(true);
        tentaclesLeft = tentaclesArray.FirstOrDefault(child => child.name == "Tentacles Left");
        tentaclesRight = tentaclesArray.FirstOrDefault(child => child.name == "Tentacles Right");
        // tentaclesLeft.gameObject.SetActive(false);
        // tentaclesRight.gameObject.SetActive(false);

        anim = GetComponent<Animator>();
    }

    private  new void Update()
    {
        base.Update();

        if (GetDistanceFromPlayer() < thresholdDistance)
            playerInRange = true;

        if (GetDistanceFromPlayer() > 2f * thresholdDistance)
            playerInRange = false;
    }

    public float GetSpeed()
    {
        return speed;
    }
}
