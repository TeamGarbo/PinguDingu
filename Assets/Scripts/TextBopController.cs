using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBopController : MonoBehaviour
{

    Vector2 floatY;
    float originalY;
    public float bobStrength;
    public float offset;
    // Start is called before the first frame update
    void Start()
    {
        this.originalY = this.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        floatY = transform.position;
        floatY.y = originalY + (Mathf.Sin(Time.time + offset) * bobStrength);
        transform.position = floatY;
    }
}
