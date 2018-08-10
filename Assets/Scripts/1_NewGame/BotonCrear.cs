using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BotonCrear : MonoBehaviour {

    public InputField name;
    public GameObject warning;
    public int indice;

    public void startNewGame(){
        if (name.text == ""){
            warning.SetActive (true);
        }
        else{
            SceneManager.LoadScene(indice);
        }
    }
}
