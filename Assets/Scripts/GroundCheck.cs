using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private PlayerController pc;
    [SerializeField] private float distance;
    ContactFilter2D filter2D;
    List<RaycastHit2D> results;
    private float threshold = 0.1f;

    private void Start()
    {
        pc = FindObjectOfType<PlayerController>();
        filter2D = filter2D.NoFilter();
        results = new List<RaycastHit2D>();
    }

    private void Update()
    {
        if (pc == null)
            pc = FindObjectOfType<PlayerController>();
        
        pc.isGrounded = CheckGrounded();
        if (pc.isGrounded)
        {
            while (CheckGrounded())
                pc.transform.position += Vector3.up * threshold;
            pc.transform.position += Vector3.down * threshold;
        }

        foreach (Transform child in transform)
               Debug.DrawRay(child.position, Vector2.down * distance, pc.isGrounded ? Color.green : Color.red);
    }

    private bool CheckGrounded()
    {
        foreach (Transform child in transform)
        {
            Physics2D.Raycast(child.position, Vector2.down, filter2D, results, distance);
            if (results.Any(hit => hit.collider.tag == "Ground"))
                return true;
        }

        return false;
    }
}
