using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDamage : MonoBehaviour
{

    private PlayerResources playerResources;


    // Start is called before the first frame updat
    void Start()
    {
        playerResources = FindObjectOfType<PlayerResources>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }




    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Lava da damage!!!");
        if (!col.CompareTag("Player")) return;

        Debug.Log("Lava da damage!!!1");
        if (FindObjectOfType<PlayerSpells>().phaseWalkActive) return;


        Debug.Log("Lava da damage!!!2");
        playerResources.TakeDamage(playerResources.maxHealth, transform.position);

        Debug.Log("Lava da damage!!!3");
    }
}
