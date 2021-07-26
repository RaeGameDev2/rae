using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons_Arrow_Move_Script : MonoBehaviour
{
    private string arrow_direction;
    public int arrow_speed = 20;
    // Start is called before the first frame update
    void Start()
    {
        if(Weapons.direction == "left")
            arrow_speed = -20;
        else
            arrow_speed = 20;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x + arrow_speed * Time.deltaTime,transform.position.y,transform.position.z);
    }
}
