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

    private void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            if (Time.time >= timeNextMainSpell)
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
            if (Time.time >= timeNextFireBolt)
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
    }

    private void MainSpell()
    {
        mana -= costMainSpell;
    }

    private void FireBolt()
    {
        mana -= costFireBolt;
    }

    private Vector2 GetDirection()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        return new Vector2(x, y).normalized;
    }
}