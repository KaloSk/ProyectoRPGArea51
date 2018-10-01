using UnityEngine;

public class TargetCharacterAction : MonoBehaviour
{

    public int targetNumber = 1;
    void OnMouseDown()
    {
        Debug.Log("TARGET: " + targetNumber);
        GameObject.Find("Canvas").GetComponent<RTP>().SetTargetCharacterSelected(targetNumber);
    }
}
