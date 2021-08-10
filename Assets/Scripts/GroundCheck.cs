using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private PlayerController pc;

    private void Start()
    {
        pc = FindObjectOfType<PlayerController>();
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Ground"))
        {
            pc.isGrounded = true;
            pc.canDoubleJump = false;
            pc.diagonalJump = false;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Ground"))
            pc.isGrounded = false;
    }
}
