using UnityEngine;

public class FireMob3 : Enemy
{
    private enum AttackType
    {
        Idle,
        Attack,
        Damage,
        Death
    }
    public bool down;
    private float relative_y;
    public float treshholdHeight = 20f;
    public bool up = true;
    private Animator anim;

    private new void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
    }

    private new void Update()
    {
        base.Update();

        anim.SetFloat("speed", speed / 7);
        anim.SetFloat("attackSpeed", attackSpeed / 100);
        if (hp <= 0)
        {
            anim.SetInteger("state", (int)AttackType.Death);
            return;
        }

        if (relative_y >= treshholdHeight)
        {
            up = false;
            down = true;
        }
        else if (relative_y <= -5f)
        {
            down = false;
            up = true;
        }

        if (up)
        {
            relative_y += speed * Time.deltaTime;
            transform.Translate(0, speed * Time.deltaTime, 0);
        }
        else if (down)
        {
            relative_y -= speed * Time.deltaTime;
            transform.Translate(0, -1 * speed * Time.deltaTime, 0);
        }
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;
        if (playerSpells.phaseWalkActive) return;
        playerResources.TakeDamage(1, transform.position);
        anim.SetInteger("state", (int)AttackType.Attack);
    }

    public override void OnDamageTaken(float damage, bool isCritical)
    {
        base.OnDamageTaken(damage, isCritical);
        anim.SetInteger("state", (int)AttackType.Damage);
    }

    public void OnDamageTakenEnd()
    {
        anim.SetInteger("state", (int)AttackType.Idle);
    }

    public void OnAttackEnd()
    {
        anim.SetInteger("state", (int)AttackType.Idle);
    }
}