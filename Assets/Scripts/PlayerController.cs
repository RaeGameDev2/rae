using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpHeight;

    private Rigidbody2D rb;
    private float hInput;

    [HideInInspector] public bool grounded;
    [HideInInspector] public bool isJumping;
    [HideInInspector] public bool canDoubleJump;

    public enum Direction
    {
        left,
        right
    }
    private Direction direction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = Direction.right;
        isJumping = false;
        grounded = true;
    }

    //Polls input every frame and updates flags accordingly
    private void HandleInput()
    {
        hInput = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.Space) && grounded)
            isJumping = true;
    }

    //Flip player when changing direction
    private void ChangeDirection()
    {
        if (hInput > 0.01f)
        {
            if (direction == Direction.left)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                direction = Direction.right;
            }

        }
        else if (hInput < -0.01f)
        {
            if (direction == Direction.right)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                direction = Direction.left;
            }
        }
    }

    private void Update()
    {
        HandleInput();
        ChangeDirection();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(hInput * speed, rb.velocity.y);

        if (isJumping && grounded)
        {
            rb.AddForce(new Vector2(rb.velocity.x, jumpHeight), ForceMode2D.Impulse);

        }
    }
}
