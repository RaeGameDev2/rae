using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float hp = 100f;
    [SerializeField] protected int damageOnTouch = 1;
    
    private bool lifeDrain;
    private float timeLifeDrain;
    [SerializeField] private float initialTimeLifeDrain = 4;
    [SerializeField] private float dpsLifeDrain = 30;
    private Transform hpBar;

    protected bool pause;

    private float initialAttackSpeed = 2f;
    [SerializeField] protected float attackSpeed;

    private void Awake()
    {
        var components = gameObject.GetComponentsInChildren<Transform>();
        hpBar = components.FirstOrDefault(component => component.tag == "HP");
    }

    private void Start()
    {
        attackSpeed = initialAttackSpeed;
    }

    private void Update()
    {
        if (lifeDrain)
        {
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
    }

    public void OnDamageTaken(float damage)
    {
        hp -= damage;
        if (hpBar != null)
            hpBar.localScale = new Vector3(hp / 100, hpBar.localScale.y, hpBar.localScale.z);
        if (hp > 0) return;
        hp = 0;
        Destroy(gameObject, 0.5f);
    }

    public void LifeDrain(int lvl)
    {
        if (lvl < 1) return;
        lifeDrain = true;
        timeLifeDrain = initialTimeLifeDrain + lvl;
    }

    public void Debuff(int lvl)
    {

    }
}