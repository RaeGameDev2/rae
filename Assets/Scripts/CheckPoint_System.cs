using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint_System : MonoBehaviour
{
    public enum Realm
    {
        Ice,
        Fire,
        Jungle
    }
    public static Dictionary<Realm, Dictionary<int, int>> realms_Portals = new Dictionary<Realm, Dictionary<int, int>>();
    public GameObject Player;
    public GameObject IcePortal_1;
    public GameObject IcePortal_2;
    public GameObject IcePortal_3;
    public GameObject IcePortal_4;
    // Start is called before the first frame update
    void Start()
    {
        realms_Portals.Add(Realm.Ice, new Dictionary<int, int>());
        realms_Portals[Realm.Ice].Add(1, 0);
        realms_Portals[Realm.Ice].Add(2, 0);
        realms_Portals[Realm.Ice].Add(3, 0);
        realms_Portals[Realm.Ice].Add(4, 0);

        realms_Portals.Add(Realm.Fire, new Dictionary<int, int>());
        realms_Portals[Realm.Fire].Add(1, 0);
        realms_Portals[Realm.Fire].Add(2, 0);
        realms_Portals[Realm.Fire].Add(3, 0);
        realms_Portals[Realm.Fire].Add(4, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
