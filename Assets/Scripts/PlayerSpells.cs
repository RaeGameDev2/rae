using UnityEngine;

public class PlayerSpells : MonoBehaviour
{
    public float coolDownFireBolt = 10f; // in seconds
    public float coolDownMainSpell = 5f; // in seconds
    public float costFireBolt = 50f;
    public float costMainSpell = 30f;
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
                Debug.Log("Cooldown: next: " + timeNextMainSpell + " time: " + Time.time);
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
                Debug.Log("Cooldown: next: " + timeNextFireBolt + " time: " + Time.time);
            }
        }

        mana += manaRegenerateSpeed * Time.deltaTime;
        if (mana > 100f)
            mana = 100f;

        GetDirection();
    }

    private void MainSpell()
    {

        mana -= costMainSpell;
        Debug.DrawRay(transform.position, Quaternion.Euler(0, 0, 30) * attackDirection * 3, Color.cyan, 2f);
        Debug.DrawRay(transform.position, Quaternion.Euler(0, 0, -30) * attackDirection * 3, Color.cyan, 2f);
    }

    private void FireBolt()
    {
        mana -= costFireBolt;
        Debug.DrawRay(transform.position, Quaternion.Euler(0, 0, 30) * attackDirection * 3, Color.red, 2f);
        Debug.DrawRay(transform.position, Quaternion.Euler(0, 0, -30) * attackDirection * 3, Color.red, 2f);
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
