using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PrenderPersonajes : Personaje {

    public int id;
    
    public Sprite pers;
    public Text namePers;
    public Text statsPers;

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

     public void cambioPers() {
        GameObject.Find("Character1").transform.GetComponent<Image>().sprite = pers;
        GameObject.Find("Character1/Name1/nombre1").transform.GetComponent<Text>().text = namePers.text ;
        GameObject.Find("Character1/Stats1/Number1").transform.GetComponent<Text>().text = statsPers.text;
    }
    public void cambioPers2()
    {
        GameObject.Find("Character2").transform.GetComponent<Image>().sprite = pers;
        GameObject.Find("Character2/Name2/nombre2").transform.GetComponent<Text>().text = namePers.text;
        GameObject.Find("Character2/Stats2/Number2").transform.GetComponent<Text>().text = statsPers.text;
    }
    public void cambioPers3()
    {
        GameObject.Find("Character3").transform.GetComponent<Image>().sprite = pers;
        GameObject.Find("Character3/Name3/nombre3").transform.GetComponent<Text>().text = namePers.text;
        GameObject.Find("Character3/Stats3/Number3").transform.GetComponent<Text>().text = statsPers.text;
    }

    public void OnPlay(){
        //GameObject.Find("Character" + id);
        prender(GameObject.Find("Character" + id).gameObject);
    }
}
