using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesScript : MonoBehaviour
{
    Image[] lives;
    public Transform life;
    // Start is called before the first frame update
    void Start()
    {
        lives = new Image[GameController.getMaxLives()];
        Debug.Log(GameController.getMaxLives());
        for (int i=0; i<lives.Length; i++){
            Transform lifeInstance = Instantiate(life, new Vector3 (0,0,0), Quaternion.identity);
            lifeInstance.parent = gameObject.transform;
            //lifeInstance.localPosition = new Vector3 (0,0,0);
            lifeInstance.localPosition = new Vector3 (-105*(i+0),-5,0);
            lives[i] = lifeInstance.GetComponent<Image>();
        }        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i=0; i<GameController.getLives(); i++){
            lives[i].enabled = true;
        }

        for(int i=GameController.getLives(); i<lives.Length; i++){
            lives[i].enabled = false;
        }
    }
}
