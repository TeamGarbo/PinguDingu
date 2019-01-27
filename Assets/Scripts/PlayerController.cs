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
    public Camera camera;
    private GameObject mainCam;
    public Transform successSmoke;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        an = transform.GetChild(1).GetComponent<Animator>();
        GameController.SetPlayer(gameObject);
        belowWaterAmount = GameController.GetWater().transform.position.y + waterOffset;
        mainCam = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update() {
        RaycastHit rayCastHit;
        int layerMask = 1 << 3;
        if (Physics.Raycast(transform.position, -(transform.position-camera.transform.position), out rayCastHit, layerMask))
            camera.transform.position = rayCastHit.point;
        else
            camera.transform.localPosition = new Vector3(-0.033f, 3.425f, -5.339f);

        if (transform.position.y < belowWaterAmount) {
            RenderSettings.fogDensity = 0.01f;
            
            GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter>().m_GroundCheckDistance = 100;
            if (an.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
                an.Play("SwimTransition");
            else if (an.GetCurrentAnimatorStateInfo(0).IsName("Swim") && an.gameObject.transform.localRotation.x < 10) {
                an.gameObject.transform.eulerAngles = new Vector3(90, transform.eulerAngles.y, 0);
                mainCam.transform.localEulerAngles = new Vector3(20, mainCam.transform.localEulerAngles.y, mainCam.transform.localEulerAngles.z);
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
            GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter>().m_GroundCheckDistance = 1f;
            RenderSettings.fogDensity = 0.007f;
            if (an.GetCurrentAnimatorStateInfo(0).IsName("Swim") || an.GetCurrentAnimatorStateInfo(0).IsName("SwimTransition")) {
                an.Play("WalkTransition");
                mainCam.transform.localEulerAngles = new Vector3(10, mainCam.transform.localEulerAngles.y, mainCam.transform.localEulerAngles.z);
            }
            else if (rb.velocity.magnitude < 0.2f && GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter>().m_IsGrounded) {
                an.Play("Idle1");
            }
            else if (!an.GetCurrentAnimatorStateInfo(0).IsName("WalkTransition") && !an.GetCurrentAnimatorStateInfo(0).IsName("Jump") && GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter>().m_IsGrounded) {
                an.Play("Walk");
            }

            if (Input.GetKeyDown(KeyCode.Space) && GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter>().m_IsGrounded) {
                an.Play("Jump");
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
                    MakeSmoke(new Vector3(0,0,0));
                }
                holdingItem.GetComponent<ItemController>().Drop(insideIgloo);
                holdingItem = null;
            }
        }
    }

    private void MakeSmoke(Vector3 localOffset){
        Transform successSmokeObject = Instantiate(successSmoke, new Vector3(0,0,0), Quaternion.identity);
        successSmokeObject.parent = GameController.GetIgloo().transform;
        // successSmokeObject.localPosition = new Vector3(0,0,0);
        Destroy(successSmokeObject.gameObject, 1.5f);
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
