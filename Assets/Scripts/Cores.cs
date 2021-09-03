using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cores : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (transform.tag == "FireCore")
            {
                GameManager.spawn_fire_core = true;
            }
            else if(transform.tag == "IceCore")
            {
                GameManager.spawn_ice_core = true;
            }
            else if(transform.tag == "NatureCore")
            {
                GameManager.spawn_nature_core = true;
            }
            Destroy(this.gameObject);
        }
    }
}
