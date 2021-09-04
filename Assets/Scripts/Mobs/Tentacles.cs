using UnityEngine;

public class Tentacles : MonoBehaviour
{
    private enum AnimType
    {
        Grow,
        Attack,
        Decent
    }

    private JungleMob3 parent;

    private Animator anim;
    [SerializeField] private AnimType animType;
    
    private bool growAnimation;
    private bool attackAnimation;
    private bool decentAnimation;

    private Vector3 offsetAttack;
    private Vector3 initialPosition;
    private void Awake()
    {
        parent = GetComponentInParent<JungleMob3>();
        animType = AnimType.Grow;
        anim = GetComponent<Animator>();
        offsetAttack = new Vector3(transform.name.Contains("Left") ? -2.84f : 2.84f, -0.57f) * parent.transform.localScale.x;
        initialPosition = transform.position;
    }

    private void Update()
    {
        anim.SetInteger("state", (int)animType);
        anim.SetFloat("speed", parent.GetSpeed());

    }

    private float timeNextAttack;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;

        if (animType != AnimType.Attack) return;
        if (Time.time < timeNextAttack) return;

        FindObjectOfType<PlayerResources>().TakeDamage(1, transform.position);
        timeNextAttack = Time.time + 1.5f;
    }

    public void Attack()
    {
        growAnimation = true;
        animType = AnimType.Grow;
        transform.position = initialPosition;
    }

    public void EndGrowAnimation()
    {
        growAnimation = false;
        if (parent.playerInRange)
        {
            animType = AnimType.Attack;
            attackAnimation = true;

            // changePosition = true;
        }
        else
        {
            animType = AnimType.Decent;
            decentAnimation = true;
        }
    }

    private void BeginAttackAnimation()
    {
        changePosition = true;
        // transform.position = initialPosition + offsetAttack;
    }

    public void EndAttackAnimation()
    {
        if (parent.playerInRange) return;
        
        attackAnimation = false;
        animType = AnimType.Decent;
        decentAnimation = true;

        // changePositionBack = true;
    }

    [SerializeField] private bool changePosition;
    [SerializeField] private bool changePositionBack;
    private void BeginDecentAnimation()
    {
        changePositionBack = true;
        // transform.position = initialPosition;
    }
    private void BeginGrowAnimation()
    {
        changePositionBack = true;
        // transform.position = initialPosition;
    }
    public void EndDecentAnimation()
    {
        decentAnimation = false;
    }

    private void OnWillRenderObject()
    {

        if (changePosition)
        {
            changePosition = false;
            transform.position = initialPosition + offsetAttack;
        }

        if (changePositionBack)
        {
            changePositionBack = false;
            transform.position = initialPosition;
        }
    }
}
