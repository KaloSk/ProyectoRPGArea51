using UnityEngine;

public class TargetAction : MonoBehaviour {

    public int targetNumber = 1;

    void OnMouseDown()
    {
        Debug.Log("CHOOSE");
        GameObject.Find("Canvas").GetComponent<RTP>().SetTargetSelected(targetNumber);
    }
}
