using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons_Melee : MonoBehaviour
{
    public GameObject Player;
    private static float attackTime = 0.5f;
    private float remainingAttackTime;
    public static float damage = 20;
    public SpriteRenderer SpriteRender;
    
    // Start is called before the first frame update
    void Start()
    {
        remainingAttackTime = attackTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(Weapons_Handler.current_melee_weapon == (int) Weapons_Handler.Weapons.SWORD)
        {
            damage = 20;
        }
        else if(Weapons_Handler.current_melee_weapon == (int) Weapons_Handler.Weapons.SCYTHE)
        {
            damage = 50;
        }
        else if(Weapons_Handler.current_melee_weapon == (int) Weapons_Handler.Weapons.SPEAR)
        {
            damage = 30;
        }
        else if(Weapons_Handler.current_melee_weapon == (int) Weapons_Handler.Weapons.ANCIENT_STAFF)
        {
            damage = 60;
        }
        else if(Weapons_Handler.current_melee_weapon == (int) Weapons_Handler.Weapons.BASIC_STAFF)
        {
            damage = 10;
        }
        if (Weapons_Handler.is_attacking == true)
        {
            remainingAttackTime -= Time.deltaTime;
            
            if(remainingAttackTime <= 0)
            {
                Weapons_Handler.is_attacking = false;
                remainingAttackTime = attackTime;
                Weapons_Enemy.Hit_Detected = false;
            }
        }
        
        if(Weapons_Handler.melee_weapon_equiped == true)
        {
            if (Weapons_Handler.is_attacking == false)
            {
                if (Weapons_Handler.direction == "right")
                {
                    transform.position = new Vector2(Player.transform.position.x - 4, Player.transform.position.y + 1);
                    SpriteRender.flipX = false;
                } 
                else if (Weapons_Handler.direction == "left") 
                {
                    transform.position = new Vector2(Player.transform.position.x + 4, Player.transform.position.y + 1);
                    SpriteRender.flipX = true;
                }
            }
            else
            {
                Attack();
            }
        }        
    }
    
    void Attack()
    {
        if (Weapons_Handler.melee_atack_direction == "left")
            transform.position = new Vector2(Player.transform.position.x - 4, Player.transform.position.y + 1);
        else if (Weapons_Handler.melee_atack_direction == "right")
            transform.position = new Vector2(Player.transform.position.x + 4, Player.transform.position.y + 1);
        else if (Weapons_Handler.melee_atack_direction == "up")
            transform.position = new Vector2(Player.transform.position.x, Player.transform.position.y + 4);
    }
}