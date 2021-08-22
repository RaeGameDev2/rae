using UnityEngine;

public class BasicController : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    [SerializeField] private float speed;

    private enum AnimState
    {
        Idle,
        Run
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        anim.SetInteger("weapon", 2);
        anim.SetInteger("type", 0);
        anim.SetInteger("state", (int)AnimState.Idle);
        anim.SetFloat("speed", 1f);
    }

    private void Update()
    {
        var x = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(x * speed, rb.velocity.y);
        if (x == 0f)
        {
            anim.SetInteger("state", (int)AnimState.Idle);
        }
        else
        {
            transform.localScale = new Vector3(x < 0f ? -1f : 1f, transform.localScale.y, transform.localScale.z);
            anim.SetInteger("state", (int) AnimState.Run);
            anim.SetFloat("speed", rb.velocity.x / 4f);
        }
    }
}
