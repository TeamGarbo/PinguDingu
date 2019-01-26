using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private readonly float jumpPower = 100;
    private readonly float velocityLimit = 4;
    [SerializeField] private readonly float waterOffset = -1.6f;
    [SerializeField] private float itemPickUpRange = 1f;
    private float belowWaterAmount;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        GameController.SetPlayer(gameObject);
        belowWaterAmount = GameController.GetWater().transform.position.y + waterOffset;
    }

    // Update is called once per frame
    void Update() {
        if (transform.position.y < belowWaterAmount) {
            RenderSettings.fogDensity = 0.1f;
            // ----------------------------------WATER CODE----------------------------------
            if (Input.GetKey(KeyCode.Space)) {
                rb.AddForce(Vector3.up * jumpPower);
            }
            if (Input.GetKey(KeyCode.LeftControl)) {
                rb.AddForce(Vector3.down * jumpPower);
            }

            if (rb.velocity.y > velocityLimit) {
                rb.velocity = new Vector3(rb.velocity.x, velocityLimit, rb.velocity.z);
            }
            else if (rb.velocity.y < -velocityLimit) {
                rb.velocity = new Vector3(rb.velocity.x, -velocityLimit, rb.velocity.z);
            }
            // ------------------------------------------------------------------------------
        }
        else {
            RenderSettings.fogDensity = 0.035f;
        }

        if (Input.GetKey(KeyCode.E)) {
            foreach (GameObject current in GameController.GetItems()) {
                if (Vector3.Distance(gameObject.transform.position, current.transform.position) < itemPickUpRange) {
                    current.SetActive(false);
                }
            }
        }
    }
}
