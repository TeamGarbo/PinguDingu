﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private readonly float jumpPower = 100;
    private readonly float velocityLimit = 4;
    private readonly float waterOffset = -1.6f;
    private readonly float itemPickUpRange = 2f;
    private float belowWaterAmount;
    private GameObject holdingItem = null;

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
            GameObject itemToGrab = null;
            if (GameController.GetItems().Length > 0)
                foreach (GameObject current in GameController.GetItems())
                    if (current != null)
                        if (Vector3.Distance(gameObject.transform.position, current.transform.position) < itemPickUpRange) {
                            itemToGrab = current;
                            holdingItem = current;
                        }

            if (itemToGrab != null) {
                GameController.RemoveItem(itemToGrab);
                itemToGrab.GetComponent<ItemController>().DoTheThing();

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Iceplace"){
            if (holdingItem != null){
                holdingItem.GetComponent<ItemController>().PlaceInsideHouse();
                holdingItem = null;
            }
        };
    }
}
