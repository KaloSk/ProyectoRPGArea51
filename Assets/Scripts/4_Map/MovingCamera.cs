using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCamera : MonoBehaviour {

    public int border;
    public float camSpeed = 2;
    public bool overButton;

	// Use this for initialization
	void Start () {
        border = Screen.width / 10;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if (CheckBorder ( 0, Input.mousePosition.y) && Camera.main.transform.position.y >= -26.8 && !overButton) {
            Camera.main.transform.Translate(Vector3.down * camSpeed * Time.deltaTime);
        }
        if (CheckBorder(Screen.height, Input.mousePosition.y) && Camera.main.transform.position.y <= -7.08 && !overButton) {
            Camera.main.transform.Translate(Vector3.up * camSpeed * Time.deltaTime);
        }
        if (CheckBorder(0, Input.mousePosition.x) && Camera.main.transform.position.x >= 16.64 && !overButton) {
            Camera.main.transform.Translate(Vector3.left * camSpeed * Time.deltaTime);
        }
        if (CheckBorder(Screen.width, Input.mousePosition.x) && Camera.main.transform.position.x <= 167.86 && !overButton) {
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

    public void SetOverButton (bool state) {
        overButton = state;
    }
}
