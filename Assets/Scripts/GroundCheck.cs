using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private PlayerController pc;

    private void Awake()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        // Debug.Log("enter");
        if (!col.CompareTag("Ground")) return;
        pc.isGrounded = true;
        pc.canDoubleJump = false;
        pc.diagonalJump = false;
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        // Debug.Log("stay");
        if (!col.CompareTag("Ground")) return;
        pc.isGrounded = true;
        pc.canDoubleJump = false;
        pc.diagonalJump = false;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        // Debug.Log("exit");
        if (col.CompareTag("Ground"))
            pc.isGrounded = false;
    }
}
