using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour {


    float C_ZERO = 0f;

    private Vector2 initialPosition;
    private bool isInitialPosition = true;

	// Use this for initialization
	void Start () {
        initialPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if(!isInitialPosition){
            Debug.Log("HEY PRRO");
            float distanceTarget = Vector2.Distance(transform.position, initialPosition);
            if (distanceTarget.CompareTo(C_ZERO) != 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, initialPosition, 10 * Time.deltaTime);
            }
            else
            {
                isInitialPosition = true;
            }
        }
	}

    public void returnOriginalPosition(){
        isInitialPosition = false;
    }
}
