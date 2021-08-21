using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal_Checkpoint : MonoBehaviour
{
    public int portal_id;
    public string portal_type;
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
        if (collision.tag != "Player")
            return;
        else
        {
            if (portal_type == "Ice") 
                CheckPoint_System.IceRealm_Portals[portal_id] = 1;
            else if (portal_type == "Fire")
                CheckPoint_System.FireRealm_Portals[portal_id] = 1;
        }
    }
}
