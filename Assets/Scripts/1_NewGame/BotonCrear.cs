using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BotonCrear : MonoBehaviour {

    public static string nameSave;
    public InputField name;
    public GameObject warning;
    public int indice;

    public void startNewGame(){
        if (name.text == ""){
            warning.SetActive (true);
        }
        else{
            nameSave = name.text;
            SceneManager.LoadScene(indice);
        }
    }
}
