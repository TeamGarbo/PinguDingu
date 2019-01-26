using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Animator an;
    private readonly float jumpPower = 100;
    private readonly float velocityLimit = 4;
    private readonly float waterOffset = -1.6f;
    private readonly float itemPickUpRange = 2f;
    private float belowWaterAmount;
    public GameObject holdingItem = null;
    public bool insideIgloo = false;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        an = transform.GetChild(1).GetComponent<Animator>();
        GameController.SetPlayer(gameObject);
        belowWaterAmount = GameController.GetWater().transform.position.y + waterOffset;
    }

    // Update is called once per frame
    void Update() {
        if (transform.position.y < belowWaterAmount) {
            RenderSettings.fogDensity = 0.1f;
            if (an.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
                an.Play("SwimTransition");
            else if (an.GetCurrentAnimatorStateInfo(0).IsName("Swim") && an.gameObject.transform.rotation.x < 120 && an.gameObject.transform.rotation.x > 60) {
                an.gameObject.transform.Rotate(new Vector3(90, 0, 0));
            }
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
            if (an.GetCurrentAnimatorStateInfo(0).IsName("Swim"))
                an.Play("WalkTransition");
            else if (an.GetCurrentAnimatorStateInfo(0).IsName("Swim") && an.gameObject.transform.rotation.x != 0) {
                an.gameObject.transform.Rotate(new Vector3(-90, 0, 0));
            }
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            if (holdingItem == null) {
                GameObject itemToGrab = null;
                if (GameController.GetItems().Length > 0)
                    foreach (GameObject current in GameController.GetItems())
                        if (current != null)
                            if (Vector3.Distance(gameObject.transform.position, current.transform.position) < itemPickUpRange) {
                                itemToGrab = current;
                                holdingItem = current;
                            }

                if (itemToGrab != null) {
                    // GameController.RemoveItem(itemToGrab);
                    itemToGrab.GetComponent<ItemController>().DoTheThing();

                }
            } else {
                if (insideIgloo){
                    Debug.Log("you put something inside the igloo :)");
                }
                holdingItem.GetComponent<ItemController>().Drop();
                holdingItem = null;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Iceplace"){
            insideIgloo = true;
        };

        // change the camera
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Iceplace"){
            insideIgloo = false;
        };
    }
}
