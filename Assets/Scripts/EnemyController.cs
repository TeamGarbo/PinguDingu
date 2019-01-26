using UnityEngine;
using System.Collections;
using static GameController;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public int moveSpeed = 5;
    public int maxDist = 10;
    public int minDist = 0;


    void Awake ()
    {
        player = GameController.GetPlayer().transform;
    }


    void FixedUpdate ()
    {
        transform.LookAt(player);

        // If the enemy is away from the player
        if (Vector3.Distance(transform.position, player.position) >= minDist) {
            // Move toward the player
            gameObject.transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed);
        } else {
            // die
        }
    } 
}