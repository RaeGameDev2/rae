// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// enum Direction
// {
//     LEFT,
//     RIGHT
// }

// public class ICE_MOB2 : MonoBehaviour
// {
//     [SerializeField] GameObject Player;

//     // Hits/second
//     [SerializeField] private float attackSpeed;

//     [SerializeField] private float patrolRange;
//     [SerializeField] private float patrolSpeed;

//     [SerializeField] private SpriteRenderer spriteRenderer;
//     [SerializeField] private Sprite idleSprite;
//     [SerializeField] private Sprite attackSprite;

//     public float health;

//     private Vector3 spawnPosition;
//     private Direction patrolDirection;
//     private Vector3 movementAxis;

//     private bool isAttacking;
//     // Attack rate: seconds till next hit
//     private float attackTimer;

//     void Start()
//     {
//         patrolRange = 5;
//         patrolSpeed = 5;

//         spawnPosition = transform.position;
//         patrolDirection = Direction.LEFT;
//         // It moves only on X axis
//         movementAxis = new Vector3(1, 0, 0);

//         health = 20;

//         // 1 hit per 2 seconds
//         attackSpeed = 0.5f;
//         isAttacking = false;
//         attackTimer = 1f / attackSpeed;
//     }

//     void Update()
//     {
//         if (!isAttacking)
//             Patrol();
//         else
//             Attack();

//         if (health <= 0)
//         {
//             Destroy(this.gameObject);
//         }

//         Debug.Log(health);
//     }

//     void Patrol()
//     {
//         if (patrolDirection == Direction.RIGHT)
//             transform.position += movementAxis * patrolSpeed * Time.deltaTime;
//         else if (patrolDirection == Direction.LEFT)
//             transform.position -= movementAxis * patrolSpeed * Time.deltaTime;

//         if (Mathf.Abs(transform.position.x - spawnPosition.x) >= patrolRange)
//         {
//             // Switch direction
//             patrolDirection = 1 - patrolDirection;
//             spriteRenderer.flipX = patrolDirection == Direction.RIGHT;
//         }
//     }

//     void Attack()
//     {
//         attackTimer -= Time.deltaTime;

//         if (attackTimer <= 0)
//         {
//             Debug.Log(name + ": I attacked!");
//             attackTimer = 1f / attackSpeed;
//         }
//     }

//     private void OnTriggerEnter2D(Collider2D collision)
//     {
//         if (collision.tag == "Player")
//         {
//             spriteRenderer.sprite = attackSprite;
//             isAttacking = true;
//         }

//         if (Weapons_Handler.is_attacking)
//         {
//             if (collision.name == "Sword")
//             {
//                 health -= 1;
//             }
//             if (collision.name == "Scythe")
//             {
//                 health -= 2;
//             }
//             if (collision.name == "Spear")
//             {
//                 health -= 3;
//             }
//             if (collision.name == "Basic_Staff")
//             {
//                 health -= 4;
//             }
//             if (collision.name == "Ancient_Staff")
//             {
//                 health -= 5;
//             }
//         }
//     }

//     private void OnTriggerExit2D(Collider2D collision)
//     {
//         if (collision.tag == "Player")
//         {
//             spriteRenderer.sprite = idleSprite;
//             isAttacking = false;
//         }
//     }
// }
