using System.Linq;
using UnityEngine;

//
// Summary:
//     tasta 0 pentru MainSpell; tasta 1 pentru FireBolt
//     Atacul se face pe o singura directie (ultima directie indicata de jucator (dintre dreapta, drapta sus, sus, stanga sus, stanga) defaultul este dreapta)
//     Doar primul inamic este afectat pe o distanta de X unitati (3 si 3 momentan)
//     Fiecare spell are un cooldown si un manaCost asociat
//     Mana se regenereaza singura in acest script, ulterior poate fi modificat
//     trebuie activat Gizmos in Game Tab in Unity ca sa se vada liniile desenate
public class PlayerSpells : MonoBehaviour
{
    public float coolDownFireBolt = 10f; // in seconds
    public float coolDownMainSpell = 5f; // in seconds
    public float costFireBolt = 50f;
    public float costMainSpell = 30f;
    public float damageMainSpell = 30f;
    public float damageFireBolt = 50f;
    public float distanceMainSpell = 3f;
    public float distanceFireBolt = 3f;
    public float mana = 100f;
    public float manaRegenerateSpeed = 10f; // 10 unitati din 100 pe secunda
    private float timeNextFireBolt;
    private float timeNextMainSpell;
    private Vector2 attackDirection;

    private void Start()
    {
        attackDirection = Vector2.right;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            if (Time.time >= timeNextMainSpell && mana >= costMainSpell)
            {
                timeNextMainSpell = Time.time + coolDownMainSpell;
                MainSpell();
                Debug.Log($"MainSpell: mana left: {mana}, time: {Time.time}, next: {timeNextMainSpell}");
            }
            else
            {
                Debug.Log($"Cooldown: next: {timeNextMainSpell}, time: {Time.time} OR mana to low: mana: {mana}");
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (Time.time >= timeNextFireBolt && mana >= costFireBolt)
            {
                timeNextFireBolt = Time.time + coolDownFireBolt;
                FireBolt();
                Debug.Log($"FireBolt: mana left: {mana}, time: {Time.time}, next: {timeNextFireBolt}");
            }
            else
            {
                Debug.Log($"Cooldown: next: {timeNextFireBolt}, time: {Time.time} OR mana to low: mana: {mana}");
            }
        }

        // auto regenerate mana: Subject to change
        mana += manaRegenerateSpeed * Time.deltaTime;
        if (mana > 100f)
            mana = 100f;

        GetDirection();
    }

    private void MainSpell()
    {
        mana -= costMainSpell;
        DrawLines(Color.cyan, distanceMainSpell);
        HitEnemy(damageMainSpell, distanceMainSpell);
    }

    private void FireBolt()
    {
        mana -= costFireBolt;
        DrawLines(Color.red, distanceFireBolt);  // TODO: mecanicile si animatiile
        HitEnemy(damageFireBolt, distanceFireBolt);
    }

    private void HitEnemy(float damage, float distance)
    {
        var start = new Vector2(transform.position.x, transform.position.y);

        // needs the player to be in Layer IgnoreRayCast and Enemy to be in any other layer. 
        var hit = Physics2D.Raycast(start, attackDirection, distance);

        var enemy = hit.transform?.GetComponent<Enemy>();
        if (enemy != null)
        {
            Debug.Log("Hit enemy");
            enemy.hitPoints -= damage;
            if (enemy.hitPoints <= 0)
            {
                // TODO: disable enemy not to be hit after death before it is destroyed
                enemy.hitPoints = 0;
                Destroy(enemy.gameObject, 1f);
            }

            var components = enemy.gameObject.GetComponentsInChildren<Transform>();

            var hpBar = components.FirstOrDefault(component => component.tag == "HP");
            if (hpBar != null)
                hpBar.localScale = new Vector3(enemy.hitPoints / 100, hpBar.localScale.y, hpBar.localScale.z);
        }
        else
        {
            Debug.Log("Miss");
        }
    }

    private void DrawLines(Color color, float distance)
    {
        for (var angle = -30; angle <= 30; angle += 15)
            Debug.DrawRay(transform.position, Quaternion.Euler(0, 0, angle) * attackDirection * distance, color, 2f);
    }

    private void GetDirection()
    {
        var x = Input.GetAxisRaw("Horizontal");
        var y = Input.GetAxisRaw("Vertical");
        if (y < 0)
            y = 0;

        if (x != 0 || y != 0)
            attackDirection = new Vector2(x, y).normalized;
    }
}
