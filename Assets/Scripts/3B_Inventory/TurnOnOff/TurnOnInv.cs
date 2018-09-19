using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnOnInv : MonoBehaviour {

    public Sprite pers;
    public string namePers;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
    }
    public void OnPlay(){
        GameObject.Find("BackGround/Frame/Personaje/Image").transform.GetComponent<Image>().sprite = pers;
        GameObject.Find("BackGround/Frame/Personaje/Text").transform.GetComponent<Text>().text = namePers;
    } 
}
