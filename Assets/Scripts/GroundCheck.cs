using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private PlayerController pc;
    [SerializeField] private float distance;
    [SerializeField] private LayerMask groundMask;

    private void Start()
    {
        pc = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if (pc)
        {
            pc.isGrounded = false;
            foreach (Transform child in transform)
            {
                pc.isGrounded |= Physics2D.Raycast(child.position, Vector2.down, distance, groundMask);
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
