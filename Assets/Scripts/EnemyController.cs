using UnityEngine;
using System.Collections;
using static GameController;

public class EnemyController : MonoBehaviour
{
    private Transform player;
    private bool chasing = false;
    private float distanceToPlayer;
    [SerializeField] private float moveSpeed;
    private float seekDistance = 7.5f;
    private float chaseDistance = 10f;
    private float killDistance = 1.2f;


    void Start() {
        player = GetPlayer().transform.GetChild(1);
    }


    void FixedUpdate() {
        distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (!chasing && distanceToPlayer < seekDistance) {
            chasing = true;
            Debug.Log("Chasing! " + distanceToPlayer);
        }
        else if (chasing && distanceToPlayer < killDistance) {
            // KILL
            chasing = false;
            enabled = false;
        }
        else if (chasing && distanceToPlayer > chaseDistance) {
            chasing = false;
            Debug.Log("HE RAN AWAY! " + distanceToPlayer);
        }

        if (chasing) {
            transform.LookAt(player);
            transform.Translate(Vector3.forward * moveSpeed);
        }
    } 
}