using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInController : MonoBehaviour
{
    public float fadeSpeed = 1;
    public Image image;

    void Awake() {
        image = GetComponent<Image>();
        var tempColor = image.color;
        tempColor.a = 0f;
        image.color = tempColor;
    }
    void Update() {
        var tempColor = image.color;
        tempColor.a += Time.deltaTime * fadeSpeed;
        image.color = tempColor;
    }
    
}
