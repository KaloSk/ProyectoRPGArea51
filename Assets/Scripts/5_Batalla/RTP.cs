using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTP : MonoBehaviour {


    float C_ZERO = 0f;

    public Transform target1;
    public Transform target2;

	// Use this for initialization
	void Start () {
        target2TP = target2.Find("TargetPlace").GetComponent<Transform>();
	}


    Transform target2TP;
    bool targetReach = false;
    bool stopAction = false;


	// Update is called once per frame
	void Update () {

        if(!stopAction){
            float distanceTarget = Vector2.Distance(target1.position, target2TP.position);
            if (distanceTarget.CompareTo(C_ZERO) != 0)
            {
                target1.position = Vector2.MoveTowards(target1.position, target2TP.position, 5 * Time.deltaTime);
            }
            else
            {
                targetReach = true;
            }

            if (targetReach)
            {
                target1.GetComponent<Animator>().SetTrigger("DealDamage");
                stopAction = true;
            } 
        }
	}
}
