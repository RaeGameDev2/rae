using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float hp;
    private float initialHP;
    [SerializeField] protected int damageOnTouch = 1;

    [SerializeField] private float initialTimeLifeDrain = 4f;
    [SerializeField] private float dpsLifeDrain = 50f;
    private bool lifeDrain;
    private float timeLifeDrain;


    [SerializeField] private float initialAttackSpeed = 2f;
    protected float attackSpeed;
    [SerializeField] protected float speed;

    [SerializeField] private GameObject damageText;
    private Transform hpBar;
    protected bool pause;

    protected void Awake()
    {
        var components = gameObject.GetComponentsInChildren<Transform>();
        hpBar = components.FirstOrDefault(component => component.tag == "HP");
        lifeDrain = false;
        pause = false;
    }

    protected void Start()
    {
        attackSpeed = initialAttackSpeed;
        initialHP = hp;
    }

    protected void Update()
    {
        if (!lifeDrain) return;
        timeLifeDrain -= Time.deltaTime;
        if (timeLifeDrain <= 0)
        {
            lifeDrain = false;
            timeLifeDrain = 0;
        }
        else
        {
            hp -= dpsLifeDrain * Time.deltaTime;
        }
    }

    private float timeNextHit;
    public void OnDamageTaken(float damage, bool isCrit)
    {
        if (Time.time < timeNextHit) return;

        timeNextHit = Time.time + 1f;
        Debug.Log($"OnDamageTaken {damage}");
        hp -= damage;
        hp = Mathf.Clamp(hp, 0f, initialHP);
        if (hpBar != null)
            hpBar.localScale = new Vector3(hp / initialHP, hpBar.localScale.y, hpBar.localScale.z);
        DamagePopup.Create(transform.position, (int)damage, isCrit);
        if (hp > 0) return;
        hp = 0;
        Destroy(gameObject, 0.5f);
    }

    public void LifeDrain(int lvl)
    {
        // if (lvl < 1) return;
        lifeDrain = true;
        timeLifeDrain = initialTimeLifeDrain + lvl;
        Debug.Log("Lifedrain: " + lvl);
    }

    public void Debuff(int lvl)
    {
        var factor = 1f - 0.2f * lvl;
        attackSpeed /= factor;
        speed *= factor;
        Debug.Log("debuff: "+ factor);
    }

    private IEnumerator DamageTextAnimation(float damage, bool crit)
    {
        var instances = Instantiate(damageText, transform.position, Quaternion.identity, transform).GetComponent<TextMeshPro>();
        instances.text = damage.ToString("####");
        instances.color = crit ? Color.blue : Color.red;
        var rectTransform = instances.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(1f, 3f);

        var time = 2f;
        var rotation = 0f;
        while (time > 0f)
        {
            yield return new WaitForFixedUpdate();
            time -= Time.fixedDeltaTime;
            rotation -= 0.4f;
            rectTransform.sizeDelta *= 1.003f;
            rectTransform.anchoredPosition += 0.02f * new Vector2(-Mathf.Sin(Mathf.Deg2Rad * rotation), Mathf.Cos(Mathf.Deg2Rad * rotation));
            rectTransform.rotation = Quaternion.Euler(0, 0, rotation);
        }
        Destroy(instances.gameObject);
    }
}