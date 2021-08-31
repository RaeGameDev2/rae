using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TestJungleMob3 : Enemy
{
    private Transform tentacles;
    private Transform tentaclesFlip;
    private Animator anim;

    private void Start()
    {
        tentacles = GetComponentsInChildren<Transform>(true).FirstOrDefault(child => child.name == "tentacles");
        tentaclesFlip = GetComponentsInChildren<Transform>(true).FirstOrDefault(child => child.name == "tentaclesFlip");
        tentacles.gameObject.SetActive(false);
        tentaclesFlip.gameObject.SetActive(false);
        tentaclesFlip.localScale =
            new Vector3(-tentaclesFlip.localScale.x, tentaclesFlip.localScale.y, tentacles.localScale.z);
        anim = tentacles.GetComponent<Animator>();
    }

    private void Update()
    {
        if (GetDistanceFromPlayer() < 10f)
        {
            tentacles.gameObject.SetActive(true);
            tentaclesFlip.gameObject.SetActive(true);
        }

        if (tentacles.gameObject.activeInHierarchy && GetDistanceFromPlayer() > 20f)
        {
            tentacles.gameObject.SetActive(false);
            tentaclesFlip.gameObject.SetActive(false);
        }
    }
}
