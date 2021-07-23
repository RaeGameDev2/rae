using UnityEngine;

public class PlayerController : MonoBehaviour
{
<<<<<<< HEAD
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private bool grounded;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        //Flip player when moving left-right
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(5, 5, 5);//pune in paranteza dimensiunile obiectului 
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-5, 5, 5);

        //pentru saritura:
        if (Input.GetKey(KeyCode.Space) && grounded)
            Jump();
=======
        [SerializeField] private float speed;
        private Rigidbody2D body;
        private bool grounded;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        //Flip player when moving left-right
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(5, 5, 5);//pune in paranteza dimensiunile obiectului 
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-5, 5, 5);

        //pentru saritura:
        if (Input.GetKey(KeyCode.Space) && grounded)
            Jump();
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            grounded = true;
>>>>>>> parent of 50beb99 (Merge branch 'master' of https://github.com/SaiTatter133/rae)
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        grounded = false;
    }

}
