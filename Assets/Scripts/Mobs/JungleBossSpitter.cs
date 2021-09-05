using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class JungleBossSpitter : Enemy
{
    public Animator animatorSpitter;
    public JungleBoss.AnimationSpitter animationState;
    public bool active;
    public bool activated;
    public bool isDefending;
    public bool isAttacking;

    public int id;

    private float timeNextAttack;

    [SerializeField] private GameObject projectile;

    private JungleBoss parent;
    private new void Awake()
    {
        base.Awake();
        parent = GetComponentInParent<JungleBoss>();
        animatorSpitter = GetComponent<Animator>();
        hpBar.gameObject.SetActive(false);
        squareHpBar.gameObject.SetActive(false);
        id = int.Parse(transform.name.ToCharArray()[transform.name.Length - 1].ToString());
        attackCooldown = 3f;
        isBoss = true;
    }

    private new void Update()
    {
        base.Update();

        animatorSpitter.SetInteger("state", (int)animationState);

        if (activated)
        {
            activated = false;
            StartCoroutine(Activate());
        }

        if (active)
        {
            isDefending = id == parent.spittersDeath + 1;

            if (isDefending)
            {
                if (timeNextAttack <= Time.time)
                {
                    StartCoroutine(Attack());
                }

                if (hp <= 0f  &!isAttacking)
                    animationState = JungleBoss.AnimationSpitter.Death;
            }
        }
    }

    private IEnumerator Attack()
    {
        timeNextAttack = Time.time + 5f;
        isAttacking = true;
        animationState = JungleBoss.AnimationSpitter.Jump;
        yield return new WaitForSeconds(1f);

        StartCoroutine(Projectiles());

        var time = 1f;
        while (time > 0)
        {
            time -= Time.fixedDeltaTime;
            transform.position += Vector3.up  * 10f * Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        
        time = 1f;
        while (time > 0)
        {
            time -= Time.fixedDeltaTime;
            transform.position += Vector3.down * 10f * Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        isAttacking = false;
        animationState = JungleBoss.AnimationSpitter.IdleBeginning;
        timeNextAttack = Time.time + attackCooldown;
    }

    private IEnumerator Projectiles()
    {
        for (var i = 0; i < 7; i++)
        {
            Instantiate(projectile, centerPiece.position, Quaternion.identity).transform.localScale = Vector3.one * 0.25f;
            yield return new WaitForSeconds(0.3f);
        }
    }

    private IEnumerator Activate()
    {
        var filter2D = new ContactFilter2D().NoFilter();
        var results = new List<RaycastHit2D>();
        Physics2D.Raycast(transform.position, Vector2.down, filter2D, results, 40f);

        var distance = transform.position.y - results.First(hit => hit.collider.tag == "Ground").point.y - 5f;

        var scale = transform.localScale.x;
        var deltaScale = 1f - scale;
        var time = 1f;
        while (time > 0)
        {
            time -= Time.fixedDeltaTime;
            transform.position += Vector3.down * distance * Time.fixedDeltaTime;
            scale += deltaScale * Time.fixedDeltaTime;
            transform.localScale = Vector3.one * scale;
            yield return new WaitForFixedUpdate();
        }

        hpBar.gameObject.SetActive(true);
        squareHpBar.gameObject.SetActive(true);
        
        var target = parent.GetComponentsInChildren<Transform>().First(child => child.name == transform.name + "Target").position;
        var direction = target - transform.position;
        distance = direction.magnitude;
        direction = direction.normalized;

        time = 1f;
        while (time > 0)
        {
            time -= Time.fixedDeltaTime;
            transform.position += direction * distance * Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        active = true;
    }

    public void OnDeath()
    {
        parent.spittersDeath++;
    }

    public void EndDeath()
    {
        Destroy(gameObject);
    }
}
