using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnInv : Personaje {


    public int id;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
    }
    public void OnPlay(){
        //GameObject.Find("Character" + id);
        apagar(GameObject.Find("Character" + id).gameObject);
        prender(GameObject.Find("Character" + id).gameObject);
    } 
}
