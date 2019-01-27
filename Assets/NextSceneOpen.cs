using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneOpen : MonoBehaviour
{
    public GameObject pingu;

    private Animator an;

    void Awake() {
        an = pingu.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(an.GetCurrentAnimatorStateInfo(0).IsName("NextScene")) {
            SceneManager.LoadScene(1);
        }
    }
}
