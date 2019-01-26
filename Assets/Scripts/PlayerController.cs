using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float jumpPower;
    [SerializeField] private float velocityLimit;
    public float velocity;

    // Start is called before the first frame update
    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        // ----------------------------------MOVEMENT CODE----------------------------------
        if (Input.GetKey(KeyCode.Space)) {
            rb.AddForce(Vector3.up * jumpPower);
        }
        if (Input.GetKey(KeyCode.LeftControl)) {
            rb.AddForce(Vector3.down * jumpPower);
        }
        velocity = rb.velocity.y;
        if (rb.velocity.y > velocityLimit) {
            rb.velocity = new Vector3(rb.velocity.x, velocityLimit, rb.velocity.z);
        }
        else if (rb.velocity.y < -velocityLimit) {
            rb.velocity = new Vector3(rb.velocity.x, -velocityLimit, rb.velocity.z);
        }
        // ------------------------------------------------------------------------------

        if (Input.GetKey(KeyCode.E)) {
            // DO PICKUP STUFF I GUESS
        }

    }
}
