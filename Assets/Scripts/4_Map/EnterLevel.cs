using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterLevel : MonoBehaviour {

    public Collider2D enter;
    public int indice;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey (KeyCode.K)){
            SceneManager.LoadScene(indice);
        }
	}
    /*void OnTriggerStay(Collider2D){
        if (Input.GetKey(KeyCode.Mouse0)){
            SceneManager.LoadScene(indice);
        }
	}*/
}
