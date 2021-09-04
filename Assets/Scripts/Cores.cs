using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cores : MonoBehaviour
{
    private void Start()
    {

        if (transform.tag == "FireCore")
            StartCoroutine(RiseCore());
    }
    
    private IEnumerator RiseCore()
    {
        var dist = 8f;
        while (dist > 0f)
        {
            dist -= 0.2f;
            transform.position += new Vector3(0f, 0.2f);
            yield return new WaitForFixedUpdate();
        }
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
