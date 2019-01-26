using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour {
    // private static Vector3[] locations = [
    //     new Vector3(0, 0, -1),
    //     new Vector3(-1, 0, -1)
    // ];

    private static int numberOfItemsInHouse = 0;

    public void DoTheThing() {
        transform.SetParent(GameController.GetPlayer().transform);
        transform.localPosition = new Vector3(0, 1, -1);
    }

    public void PlaceInsideHouse() {
        transform.SetParent(GameController.GetIgloo().transform);
        transform.localPosition = new Vector3(1.469f, 0.3104561f, 2.492f);
        transform.localPosition = new Vector3(0.827f, 0.09f, 0.125f);
    }
}
