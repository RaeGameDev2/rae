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

        Player.transform.position = new Vector3(IcePortal_3.transform.position.x, IcePortal_3.transform.position.y, IcePortal_3.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
