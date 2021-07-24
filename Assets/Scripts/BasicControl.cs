using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BasicControl : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = 10;
        jumpForce = 10;
    }

    void Update()
    {
        var x = Input.GetAxis("Horizontal");

        transform.position += new Vector3(x, 0) * speed * Time.deltaTime;
        if (Input.GetButtonDown("Jump"))
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}