// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Weapons_Melee : MonoBehaviour
// {
//     public GameObject Player;
//     private static float attackTime = 0.5f;
//     private float remainingAttackTime;
//     public static float damage = 20;
//     public SpriteRenderer SpriteRender;

//     // Start is called before the first frame update
//     void Start()
//     {
//         remainingAttackTime = attackTime;
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         switch (Weapons_Handler.current_melee_weapon)
//         {
//             case Weapons_Handler.Weapons.SWORD: damage = 20; break;
//             case Weapons_Handler.Weapons.SCYTHE: damage = 50; break;
//             case Weapons_Handler.Weapons.SPEAR: damage = 30; break;
//             case Weapons_Handler.Weapons.ANCIENT_STAFF: damage = 60; break;
//             case Weapons_Handler.Weapons.BASIC_STAFF: damage = 10; break;
//         }
//         if (Weapons_Handler.is_attacking == true)
//         {
//             remainingAttackTime -= Time.deltaTime;

//             if(remainingAttackTime <= 0)
//             {
//                 Weapons_Handler.is_attacking = false;
//                 remainingAttackTime = attackTime;
//                 Weapons_Enemy.Hit_Detected = false;
//             }
//         }

//         if(Weapons_Handler.melee_weapon_equiped == true)
//         {
//             if (Weapons_Handler.is_attacking == false)
//             {
//                 transform.localPosition = new Vector2(-2.5f, 1);
//             }
//             else
//             {
//                 Attack();
//             }
//         }        
//     }

//     void Attack()
//     {

//         if (Weapons_Handler.melee_atack_direction == Weapons_Handler.Directions.UP)
//             transform.localPosition = new Vector2(1, 3.5f);
//         else 
//             transform.localPosition = new Vector2(2.5f, 1);
//     }
// }
