using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x + offset.x, target.position.y + offset.y, transform.position.z);
    }
}
