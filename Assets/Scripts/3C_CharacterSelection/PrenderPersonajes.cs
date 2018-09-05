using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrenderPersonajes : Personaje {

    public int id;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void slot() {
        Transform formationList = GameObject.Find("Formationlist").transform;
        if (!formationList.Find("Character1").gameObject.activeInHierarchy) {
            Debug.Log("Encontrado1");
        }
        else if (!formationList.Find("Character2").gameObject.activeInHierarchy){
            Debug.Log("Encontrado2");
        }
        else if (!formationList.Find("Character3").gameObject.activeInHierarchy){
            Debug.Log("Encontrado3");
        }
    }
    public void OnPlay(){
        //GameObject.Find("Character" + id);
        prender(GameObject.Find("Character" + id).gameObject);
    }
}
