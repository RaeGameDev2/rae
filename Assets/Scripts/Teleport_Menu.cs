using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport_Menu : MonoBehaviour
{
    public GameObject Player;
    public GameObject IcePortal_1;
    public GameObject IcePortal_2;
    public GameObject IcePortal_3;
    public GameObject IcePortal_4;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Portal1()
    {
        Player.transform.position = new Vector3(IcePortal_1.transform.position.x, IcePortal_1.transform.position.y, IcePortal_1.transform.position.z);
    }

    public void Portal2()
    {
        Player.transform.position = new Vector3(IcePortal_2.transform.position.x, IcePortal_2.transform.position.y, IcePortal_2.transform.position.z);
    }

    public void Portal3()
    {
        Player.transform.position = new Vector3(IcePortal_3.transform.position.x, IcePortal_3.transform.position.y, IcePortal_3.transform.position.z);
    }
    public void Portal4()
    {
        Player.transform.position = new Vector3(IcePortal_4.transform.position.x, IcePortal_4.transform.position.y, IcePortal_4.transform.position.z);
    }
}
