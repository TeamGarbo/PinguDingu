using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinDetector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] allItems = GameObject.FindGameObjectsWithTag("Item");
        int numCollected = 0;
        foreach (GameObject item in allItems) {
            if(item.transform.parent == GameController.GetIgloo().transform){
                numCollected++;
            }
        }
        if (numCollected == allItems.Length){
            gameWon();
        }
    }

    private void gameWon(){
        SceneManager.LoadScene("VictoryScene");
    }
}
