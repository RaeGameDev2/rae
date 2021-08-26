using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private PlayerController pc;
    [SerializeField] private float distance;
    [SerializeField] private LayerMask groundMask;
    ContactFilter2D filter2D;
    List<RaycastHit2D> results;

    private void Start()
    {
        pc = FindObjectOfType<PlayerController>();
        filter2D = filter2D.NoFilter();
        results = new List<RaycastHit2D>();
    }

    private void Update()
    {
        if (pc)
        {
            pc.isGrounded = false;
            foreach (Transform child in transform)
            {
                Physics2D.Raycast(child.position, Vector2.down, filter2D, results, distance);
                foreach (RaycastHit2D hit in results)
                {
                    if (hit.collider.tag == "Ground")
                    {
                        pc.isGrounded = true;
                        break;
                    }
                }
                if (pc.isGrounded)
                    Debug.DrawRay(child.position, Vector2.down * distance, Color.green);
                else
                    Debug.DrawRay(child.position, Vector2.down * distance, Color.red);
            }
        }
        else
            pc = FindObjectOfType<PlayerController>();
    }
}
