// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class Weapons_Switcher : MonoBehaviour
// {
//     public GameObject Player;
//     public GameObject Sword;
//     public GameObject Bow;

//     public static bool melee_weapon_equiped = true;
//     public static bool range_weapon_equiped = false;
//     public static bool is_attacking = false;

//     //Attack constraints
//     private static float attackTime = 0.5f;
//     private float remainingAttackTime;

//     private string melee_atack_direction;
//     public static float melee_damage = 20;
//     public static float spell_damage = 20;
//     public static string direction = "right";
//     private int object_rotated = 1;
//     public GameObject arrow_prefab;
//     private GameObject new_instance;
//     public int arrow_speed = 10;
//     public int flow = 10;

//     void weapon_switch()
//     {
//         if (melee_weapon_equiped == true)
//         {
//             melee_weapon_equiped = false;
//             range_weapon_equiped = true;
//             Sword.gameObject.SetActive(false);
//             Bow.gameObject.SetActive(true);
//         }
//         else if (range_weapon_equiped == true)
//         {
//             melee_weapon_equiped = true;
//             range_weapon_equiped = false;
//             Sword.gameObject.SetActive(true);
//             Bow.gameObject.SetActive(false);
//         }
//     }

//     // Start is called before the first frame update
//     void Start()
//     {
//         Sword.gameObject.SetActive(true);
//         Bow.gameObject.SetActive(false);

//         remainingAttackTime = attackTime;
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         if ((Input.GetKeyDown(KeyCode.T) || Input.GetAxis("Mouse ScrollWheel") > 0f  || (Input.GetAxis("Mouse ScrollWheel") < 0f ))  && is_attacking == false)
//         {
//             weapon_switch();
//         }
//         if (Input.GetKeyDown(KeyCode.P) && range_weapon_equiped == true)
//         {
//             new_instance = Instantiate(arrow_prefab,new Vector3(Player.transform.position.x + 1,Player.transform.position.y,Player.transform.position.z),Quaternion.identity);
//         }
//         if (Input.GetKeyDown(KeyCode.D))
//         {
//             if (direction == "left")
//                 object_rotated = 0;
//             direction = "right";
//         } else if (Input.GetKeyDown(KeyCode.A)) {
//             if (direction == "right")
//                 object_rotated = 0;
//             direction = "left";
//         }
//         if (Input.GetKey("left") && is_attacking == false && direction == "left")
//         {
//             is_attacking = true;
//             melee_atack_direction = "left";
//         } else if (Input.GetKey("right") && is_attacking == false && direction == "right")
//         {
//             is_attacking = true;
//             melee_atack_direction = "right";
//         } else if (Input.GetKey("up") && is_attacking == false)
//         {
//             is_attacking = true;
//             melee_atack_direction = "up";
//         }

//         if (is_attacking == true)
//         {
//             remainingAttackTime -= Time.deltaTime;
            
//             if(remainingAttackTime <= 0)
//             {
//                 is_attacking = false;
//                 remainingAttackTime = attackTime;
//                 Weapons_Enemy.Hit_Detected = false;
//             }
//         }

        
//         if(melee_weapon_equiped == true)
//         {
//             if (is_attacking == false)
//             {
//                 if (direction == "right")
//                 {
//                     Sword.transform.position = new Vector2(Player.transform.position.x - 3, Player.transform.position.y + 1);
//                     if(object_rotated == 0)
//                     {
//                         Sword.transform.localScale = new Vector3(Sword.transform.localScale.x * -1,Sword.transform.localScale.y,Sword.transform.localScale.z);
//                         object_rotated = 1;
//                         // Sword.transform.eulerAngles = new Vector3(Sword.transform.eulerAngles.x, Sword.transform.eulerAngles.y, -28.377f);
//                         // Debug.Log("Aici1");
//                         // object_rotated = 1;
//                     }
//                 } else if (direction == "left") {
//                     Sword.transform.position = new Vector2(Player.transform.position.x + 3, Player.transform.position.y + 1);
//                     if (object_rotated == 0)
//                     {
//                         Sword.transform.localScale = new Vector3(Sword.transform.localScale.x * -1,Sword.transform.localScale.y,Sword.transform.localScale.z);
//                         object_rotated = 1;
//                         // Sword.transform.eulerAngles = new Vector3(Sword.transform.eulerAngles.x, Sword.transform.eulerAngles.y, 31.623f);
//                         // object_rotated = 1;
//                         // Debug.Log("Aici2");
//                     }
//                 }
//             }
//             else
//             {
//                 Attack();
//             }
//         }
//         else if (range_weapon_equiped == true && is_attacking == false)
//         {
//             if (is_attacking == false)
//                 Bow.transform.position = new Vector2(Player.transform.position.x - 3, Player.transform.position.y + 1);
//         }
//     }

//     void Attack()
//     {
//         if (melee_atack_direction == "left")
//             Sword.transform.position = new Vector2(Player.transform.position.x - 3, Player.transform.position.y + 1);
//         else if (melee_atack_direction == "right")
//             Sword.transform.position = new Vector2(Player.transform.position.x + 3, Player.transform.position.y + 1);
//         else if (melee_atack_direction == "up")
//             Sword.transform.position = new Vector2(Player.transform.position.x, Player.transform.position.y + 4);
//     }
// }
