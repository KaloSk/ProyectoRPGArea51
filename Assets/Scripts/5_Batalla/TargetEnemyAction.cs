using UnityEngine;

public class TargetEnemyAction : MonoBehaviour
{

    public int targetNumber = 1;

    void OnMouseDown()
    {
        GameObject.Find("Canvas").GetComponent<RTP>().SetTargetEnemySelected(targetNumber);
    }
}
