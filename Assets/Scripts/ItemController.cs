using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour {

    private static int numberOfItemsInHouse = 0;

    [SerializeField] private Vector3 carryPosition;
    [SerializeField] private Vector3 carryRotation;
    private Vector3 originalRotation;
    [SerializeField] private Vector3 dropPosition;
    [SerializeField] private Vector3 dropRotation;

    public void DoTheThing() {
        transform.SetParent(GameController.GetPlayer().transform.GetChild(1));
        originalRotation = transform.eulerAngles;
        transform.localPosition = carryPosition;
        transform.eulerAngles = carryRotation;
        if (GetComponent<Collider>() != null) {
            GetComponent<Collider>().enabled = false;
        }
    }

    public void PlaceInsideHouse() {
        transform.SetParent(GameController.GetIgloo().transform);
        transform.localPosition = new Vector3(1.469f, 0.3104561f, 2.492f);
        transform.localPosition = new Vector3(0.827f, 0.09f, 0.125f);
    }

    private void setPositionToPlayer(){
        //transform.SetParent(GameController.GetIgloo().transform);
        transform.eulerAngles = originalRotation;
        transform.position = new Vector3(transform.position.x, GameController.GetPlayer().transform.position.y+0.5f, transform.position.z);
    }

    public void Drop(bool insideIgloo) {
        if (dropPosition.x == 0 && dropPosition.y == 0 && dropPosition.z == 0) {
            setPositionToPlayer();
            if (insideIgloo){
                transform.SetParent(GameController.GetIgloo().transform);
            }
        } else {
            if (insideIgloo){
                Debug.Log(dropPosition);

                transform.SetParent(GameController.GetIgloo().transform);
                transform.eulerAngles = originalRotation;
                // transform.position = new Vector3(transform.position.x, GameController.GetPlayer().transform.position.y+0.5f, transform.position.z);
                transform.localPosition = dropPosition;
                transform.localRotation = Quaternion.Euler(dropRotation.x, dropRotation.y, dropRotation.z);
            } else {
                setPositionToPlayer();
            }
        }

        
    }
}
