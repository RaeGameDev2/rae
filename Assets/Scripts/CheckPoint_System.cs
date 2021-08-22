using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint_System : MonoBehaviour
{
    public static Dictionary<int, int> IceRealm_Portals = new Dictionary<int, int>(); // (portal id - 0 if locked and 1 if unlocked)
    public static Dictionary<int, int> FireRealm_Portals = new Dictionary<int, int>();
    public GameObject Player;
    public GameObject IcePortal_1;
    public GameObject IcePortal_2;
    public GameObject IcePortal_3;
    public GameObject IcePortal_4;
    // Start is called before the first frame update
    void Start()
    {
        IceRealm_Portals.Add(1,0);
        IceRealm_Portals.Add(2,0);
        IceRealm_Portals.Add(3,0);
        IceRealm_Portals.Add(4,0);

        FireRealm_Portals.Add(1,0);
        FireRealm_Portals.Add(2,0);
        FireRealm_Portals.Add(3,0);
        FireRealm_Portals.Add(4,0);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Player.transform.position = new Vector3(IcePortal_1.transform.position.x, IcePortal_1.transform.position.y, IcePortal_1.transform.position.z);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Player.transform.position = new Vector3(IcePortal_2.transform.position.x, IcePortal_2.transform.position.y, IcePortal_2.transform.position.z);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Player.transform.position = new Vector3(IcePortal_3.transform.position.x, IcePortal_3.transform.position.y, IcePortal_3.transform.position.z);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Player.transform.position = new Vector3(IcePortal_4.transform.position.x, IcePortal_4.transform.position.y, IcePortal_4.transform.position.z);
        }
    }
}
