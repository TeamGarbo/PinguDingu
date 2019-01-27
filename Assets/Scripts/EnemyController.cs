using UnityEngine;
using System.Collections;
using static GameController;
using System.Timers;
using System;

public class EnemyController : MonoBehaviour
{
    private Transform player;
    private bool chasing = false;
    private float distanceToPlayer;
    [SerializeField]  private float moveSpeed;
    [SerializeField]  private float seekDistance = 7.5f;
    [SerializeField]  private float chaseDistance = 10f;
    [SerializeField]  private float killDistance = 1.2f;
    [SerializeField] private Vector3[] patrolPoints;
    private int patrolIndex;

    private volatile bool takingABreak = false;
    private int MAX_DELAY = 2000;
    private int stopTheBreak = 0;

    void Start() {
        player = GetPlayer().transform.GetChild(1);
    }

    void FixedUpdate() {
        int t = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;

        if (t > stopTheBreak ) {
            distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (!chasing && distanceToPlayer < seekDistance) {
                chasing = true;
            }

            if(player.position.y > GameController.GetWater().transform.position.y + -1.6f){
                chasing = false;
            }

            else if (chasing && distanceToPlayer < killDistance) {
                // KILL
                chasing = false;
                takingABreak = true;

                GameController.removeLife();

                TimeSpan currentTime = (DateTime.UtcNow - new DateTime(1970, 1, 1));
                stopTheBreak = (int)currentTime.TotalSeconds + 2;
            }
            else if (chasing && distanceToPlayer > chaseDistance) {
                chasing = false;
            }

            if (chasing) {
                transform.LookAt(player);
                transform.Translate(Vector3.forward * moveSpeed);
            }

            if (!chasing) {
                transform.LookAt(patrolPoints[patrolIndex]);
                transform.Translate(Vector3.forward * moveSpeed / 2);
            }
        }
    }
}

