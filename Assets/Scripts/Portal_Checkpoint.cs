using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal_Checkpoint : MonoBehaviour
{   
    public int portal_id;
    public CheckPoint_System.Realm portal_type;
    public GameObject Teleport_Menu;
    // Start is called before the first frame update
    void Start()
    {
        Teleport_Menu.SetActive(false);
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
            if (portal_type == CheckPoint_System.Realm.Ice)
            {
                Teleport_Menu.SetActive(true);
                CheckPoint_System.realms_Portals[CheckPoint_System.Realm.Ice][portal_id] = 1;
            }
            else if (portal_type == CheckPoint_System.Realm.Fire)
            {
                Teleport_Menu.SetActive(true);
                CheckPoint_System.realms_Portals[CheckPoint_System.Realm.Fire][portal_id] = 1;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "Player")
            return;
        else
        {
            if (portal_type == CheckPoint_System.Realm.Ice)
            {
                Teleport_Menu.SetActive(false);
            }
            else if (portal_type == CheckPoint_System.Realm.Fire)
            {
                Teleport_Menu.SetActive(false);
            }
        }
    }
}
