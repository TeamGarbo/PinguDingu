using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour {

    public void DoTheThing() {
        transform.SetParent(GameController.GetPlayer().transform.GetChild(1));
        transform.localPosition = new Vector3(0, 2, -1);
    }
}
