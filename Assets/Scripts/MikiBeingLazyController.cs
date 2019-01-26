using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MikiBeingLazyController : MonoBehaviour
{
    void Awake() {
        transform.localScale = new Vector3(Screen.width / 160 + 1, Screen.height / 90 + 1, 0);
    }
}
