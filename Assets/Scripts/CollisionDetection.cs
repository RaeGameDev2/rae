using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{

    private Transform player;
    private ContactFilter2D filter2D;
    private List<RaycastHit2D> results = new List<RaycastHit2D>();
    private const float threshold = 0.1f;
    private const float distance = 1f;
    private List<Transform> originLeft;
    private List<Transform> originRight;
    private List<Transform> originUp;
    
    private void Start()
    {
        player = FindObjectOfType<PlayerSpells>().transform;
        filter2D = filter2D.NoFilter();
        originLeft = new List<Transform>(GetComponentsInChildren<Transform>().Where(child => child.tag == "Left"));
        originRight = new List<Transform>(GetComponentsInChildren<Transform>().Where(child => child.tag == "Right"));
        originUp = new List<Transform>(GetComponentsInChildren<Transform>().Where(child => child.tag == "Up"));
    }

    // private int frame;
    private void FixedUpdate()
    {
        // frame++;
        // Debug.Log(originLeft.Count + " " + player.position + player.GetComponent<Rigidbody2D>().velocity +
        //           Time.deltaTime);
        if (CheckCollision(Vector2.left))
        {
            // foreach (var hit2D in results.Where(hit2D => hit2D.collider.tag == "Ground"))
            // {
            //     print(frame + " " + player.position + " " + hit2D.collider.name + " " + hit2D.transform.position + " " + hit2D.point);
            // }
            while (CheckCollision(Vector2.left))
                player.position += Vector3.right * threshold;
            // Debug.Log("left " + player.position);
            
            player.position += Vector3.left * threshold;
        }

        if (CheckCollision(Vector2.right)) {
            while (CheckCollision(Vector2.right))
                player.position += Vector3.left * threshold;
            player.position += Vector3.right * threshold;
        }
        
        if (CheckCollision(Vector2.up))
        {
            while (CheckCollision(Vector2.up))
                player.position += Vector3.down * threshold;
            player.position += Vector3.up * threshold;
        }
    }

    private bool CheckCollision(Vector2 direction)
    {
        List<Transform> origins;
        Vector3 dir;
        if (direction == Vector2.up)
        {
            origins = originUp;
            dir = Vector3.up;
        }
        else if (direction == Vector2.left)
        {
            dir = Vector3.left; 
            origins = originLeft;
        }
        else
        {
            dir = Vector3.right;
            origins = originRight;
        }

        foreach (var child in origins)
        {
            results.Clear();
            Physics2D.Raycast(child.position, direction, filter2D, results, distance);
            
            if (results.Any(hit => hit.collider.tag == "Ground"))
                return true;
        }

        foreach (var child in origins)
            Debug.DrawRay(child.position, dir * distance, Color.blue);
        return false;
    }
}
