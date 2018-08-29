using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrenderPersonajes : Personaje {

    public int id;

    public void OnPlay(){
        //GameObject.Find("Character" + id);
        prender(GameObject.Find("Character" + id).gameObject);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
