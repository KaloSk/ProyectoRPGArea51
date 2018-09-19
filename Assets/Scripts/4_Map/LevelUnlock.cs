﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelUnlock : MonoBehaviour {

	public int victory = 0;

    public int indice;

	public Sprite levelPrend;
	
	void OnMouseDown () {
		if (victory == 1){
			Debug.Log("Enter1");
			Debug.Log(GameObject.Find("Mundo1/Niveles/Nivel2").transform.name);
			GameObject.Find("Mundo1/Niveles/Nivel2").transform.GetComponent<SpriteRenderer>().sprite = levelPrend;
		}
		else if (victory == 2) {
			Debug.Log("Enter2");
			GameObject.Find("Mundo1/Niveles/Nivel3").transform.GetComponent<SpriteRenderer>().sprite = levelPrend;
		}
		else if (victory == 3) {
			Debug.Log("Enter3");
			GameObject.Find("Mundo1/Boss/Boss1").transform.GetComponent<SpriteRenderer>().sprite = levelPrend;
		}
	}
    void nivel () {
        if (GameObject.Find("Mundo1/Niveles/Nivel2").transform.GetComponent<SpriteRenderer>().sprite == levelPrend){
            SceneManager.LoadScene(indice);
        }
        if (GameObject.Find("Mundo1/Niveles/Nivel3").transform.GetComponent<SpriteRenderer>().sprite == levelPrend){
            SceneManager.LoadScene(indice);
        }
        if (GameObject.Find("Mundo1/Niveles/Boss1").transform.GetComponent<SpriteRenderer>().sprite == levelPrend){
            SceneManager.LoadScene(indice);
        }
    }
}
