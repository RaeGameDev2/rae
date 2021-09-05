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

    private JungleBoss parent;
    private new void Awake()
    {
        base.Awake();
        parent = GetComponentInParent<JungleBoss>();
        animatorSpitter = GetComponent<Animator>();
        hpBar.gameObject.SetActive(false);
        squareHpBar.gameObject.SetActive(false);
        id = int.Parse(transform.name.ToCharArray()[transform.name.Length - 1].ToString());
    }

    private new void Update()
    {
        base.Update();

        if (activated)
        {
            activated = false;
            StartCoroutine(Activate());
        }

        if (active)
        {

        }
    }

    private IEnumerator Activate()
    {
        var filter2D = new ContactFilter2D().NoFilter();
        var results = new List<RaycastHit2D>();
        Physics2D.Raycast(transform.position, Vector2.down, filter2D, results, 40f);

        var distance = results.First(hit => hit.collider.tag == "Ground").point.y - transform.position.y - 2.6f;

        var scale = transform.localScale.x;
        var deltaScale = 1f - scale;
        var time = 1f;
        while (time > 0)
        {
            time -= Time.fixedDeltaTime;
            transform.position -= Vector3.down * distance * Time.fixedDeltaTime;
            scale += deltaScale * Time.fixedDeltaTime;
            transform.localScale = Vector3.one * scale;
            yield return new WaitForFixedUpdate();
        }

        hpBar.gameObject.SetActive(false);
        squareHpBar.gameObject.SetActive(false);

        animationState = JungleBoss.AnimationSpitter.Idle;
        var target = parent.GetComponentsInChildren<Transform>().First(child => child.name == transform.name + "Target").position;
        Debug.Log(target);

        var direction = target - transform.position;
        
        Debug.Log(direction);

        distance = direction.magnitude;
        direction = direction.normalized;

        time = 1f;
        while (time > 0)
        {
            time -= Time.fixedDeltaTime;
            transform.position -= direction * distance * Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        active = true;
    }

    public void OnDeath()
    {
        parent.spittersDeath++;
    }
}
