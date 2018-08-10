using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour{

    void Update(){

    }

    void QuitGameButton() {
        Application.Quit();
        Debug.Log("Game is exiting");
    }
}
