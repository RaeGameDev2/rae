using UnityEngine;

public class FIRE_MOB3 : Enemy
{
    // Start is called before the first frame update
    public static float speedY = 20f;
    public bool down;
    private Resources playerResources;
    public float relative_y;
    public float treshholdHeight = 20f;
    public bool up = true;

    private void Start()
    {
        base.Start();
        playerResources = FindObjectOfType<Resources>();
    }
    
    private void Update()
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
            relative_y += speedY * Time.deltaTime;
            transform.Translate(0, speedY * Time.deltaTime, 0);
        }
        else if (down)
        {
            relative_y -= speedY * Time.deltaTime;
            transform.Translate(0, -1 * speedY * Time.deltaTime, 0);
        }
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;
        if (FindObjectOfType<PlayerSpells>().phaseWalkActive) return;

        playerResources.TakeDamage(damageOnTouch, transform.position);
    }
}