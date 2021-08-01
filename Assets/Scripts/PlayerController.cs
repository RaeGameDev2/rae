using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float groundSpeed;
    [SerializeField] private float airSpeed;

    [SerializeField] private float jumpHeight;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float fallSpeed;
    [SerializeField] private float gravity;

    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDistance;
    [SerializeField] private float dashCooldown;

    private float currGravity;
    private float prevVelocityY;


    private Rigidbody2D rb;
    private float hInput;

    [HideInInspector] public bool isGrounded;
    [HideInInspector] public bool canJump;
    [HideInInspector] public bool canDoubleJump;
    [HideInInspector] public bool diagonalJump;
    [HideInInspector] public bool onIce;

    private bool jumpPressed;
    private bool dashPressed;
    private float distanceTraveled;
    private float timeSinceDash;

    private bool isDashing;

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
        canJump = false;
        diagonalJump = false;
        isGrounded = true;
        currGravity = gravity;
        canDoubleJump = false;
        prevVelocityY = 0;
        isDashing = false;
        distanceTraveled = 0;
        timeSinceDash = dashCooldown;
    }

    //Polls input every frame and updates flags accordingly
    private void HandleInput()
    {
        hInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
            jumpPressed = true;
        if (Input.GetKeyDown(KeyCode.LeftShift))
            dashPressed = true;
    }

    //Flip player when changing direction
    private void ChangeDirection()
    {
        if (hInput > 0.01f && rb.velocity.x > 0)
        {
            if (direction == Direction.left)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                direction = Direction.right;
            }

        }
        else if (hInput < -0.01f && rb.velocity.x < 0)
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
        if (isDashing == false)
        {
            timeSinceDash += Time.deltaTime;
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        currGravity = jumpSpeed * jumpSpeed / (2 * jumpHeight);
        if (rb.velocity.x != 0)
            diagonalJump = true;
    }

    private IEnumerator Dash()
    {
        Vector2 initPos = rb.position;
        distanceTraveled = 0;

        while (distanceTraveled < dashDistance)
        {
            distanceTraveled += dashSpeed * Time.fixedDeltaTime;
            Debug.Log(distanceTraveled);
            yield return new WaitForFixedUpdate();
        }
        if (direction == Direction.right)
            rb.position = new Vector2(initPos.x + dashDistance, initPos.y);
        else
            rb.position = new Vector2(initPos.x - dashDistance, initPos.y);
        isDashing = false;
        timeSinceDash = 0;
    }

    private void CheckJump()
    {
        if (jumpPressed)
        {
            jumpPressed = false;

            if (isGrounded)
            {
                Jump();
                canDoubleJump = true;

            }
            else if (canDoubleJump)
            {
                Jump();
                canDoubleJump = false;
            }
        }
    }

    private void FixedUpdate()
    {
        //Reset gravity to default values
        if (isGrounded || rb.velocity.y < 0)
            currGravity = gravity;

        //If not in dash, handles jump
        if (!isDashing)
        {
            CheckJump();

            if (isGrounded)
                rb.velocity = new Vector2(hInput * groundSpeed, rb.velocity.y);
            else
            {
                float speed = hInput * airSpeed + rb.velocity.x;
                if (diagonalJump)
                    speed = Mathf.Clamp(speed, -groundSpeed, groundSpeed);
                else
                    speed = Mathf.Clamp(speed, -airSpeed, airSpeed);
                if (rb.velocity.y == 0 || (rb.velocity.y < 0 && prevVelocityY > 0))
                {
                    rb.velocity = new Vector2(speed, -fallSpeed);

                }
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
            //Add simulated gravity
            rb.AddForce(currGravity * Vector2.down, ForceMode2D.Force);
        }

        if (dashPressed && !isDashing && timeSinceDash >= dashCooldown)
        {
            dashPressed = false;
            isDashing = true;
            StartCoroutine("Dash");
            if (direction == Direction.right)
            {
                rb.velocity = new Vector2(dashSpeed, 0);
            }
            else
            {
                rb.velocity = new Vector2(-dashSpeed, 0);
            }
        }

        //Save previous y-velocity for adding fallSpeed
        prevVelocityY = rb.velocity.y;
        //  Debug.Log(rb.velocity);
    }
}
