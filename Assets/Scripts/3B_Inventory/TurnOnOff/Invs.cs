using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Invs : MonoBehaviour {


    public void apagar(GameObject id){
        id.SetActive(false);
    }

    public void prender(GameObject id2){
        id2.SetActive(true);
    }
}
