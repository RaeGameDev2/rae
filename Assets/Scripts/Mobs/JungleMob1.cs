using UnityEngine;
using Random = UnityEngine.Random;

public class JungleMob1 : Enemy
{
    public bool down;
    public float relative_y;
    public float treshholdHeight = 20f;
    public bool up = true;

    private new void Start()
    {
        base.Start();
    }

    private new void Update()
    {
        base.Update();
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
    }
}