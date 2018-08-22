using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCamera : MonoBehaviour {

    public int border;
    public float camSpeed = 2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if (CheckBorder ( 0, Input.mousePosition.y)) {
            Camera.main.transform.Translate(Vector3.down * camSpeed * Time.deltaTime);
        }
        if (CheckBorder(Screen.height, Input.mousePosition.y)) {
            Camera.main.transform.Translate(Vector3.up * camSpeed * Time.deltaTime);
        }
        if (CheckBorder(0, Input.mousePosition.x)) {
            Camera.main.transform.Translate(Vector3.left * camSpeed * Time.deltaTime);
        }
        if (CheckBorder(Screen.width, Input.mousePosition.x)) {
            Camera.main.transform.Translate(Vector3.right * camSpeed * Time.deltaTime);
        }

    }

    bool CheckBorder(float limit, float targetValue){
        float difference = ((limit - border) <= 0) ? border : -border;
        if (difference > limit) {
            return targetValue > limit && targetValue < limit + difference;
        } else {
            return targetValue < limit && targetValue > limit + difference;
        }

    }
}
