using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class OpenDoorController : MonoBehaviour
{
    public float openDistance = 5f;
    private Animator an;
    private GameObject player;

    private void Awake() {
        an = transform.GetComponent<Animator>();
        player = GameController.GetPlayer();
    }

    void Update() {
        if(Vector3.Distance(player.transform.position, transform.position) <= openDistance) {
            if(Input.GetKeyDown(KeyCode.F)) {
                if (gameObject.name.Equals("LeftDoor"))
                if(an.GetCurrentAnimatorStateInfo(0).IsName("DoorL Close")) {
                    an.Play("IglooDoorL Open");
                } else {
                    an.Play("DoorL Close");
                }
                else {
                    if (an.GetCurrentAnimatorStateInfo(0).IsName("DoorR Close")) {
                        an.Play("DoorR Open");
                    }
                    else {
                        an.Play("DoorR Close");
                    }
                }
            }
        }
    }
}
