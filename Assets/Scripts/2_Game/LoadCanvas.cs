using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadCanvas : MonoBehaviour {

    GameController gc = new GameController();

	// Use this for initialization
	void Start () {

        if (gc.GetAllCharactersList() == null)
        {
            SceneManager.LoadScene(GameConstants.SCENE_INTRO);
        }
        else
        {
            transform.Find("InfoPanel/GroupText").GetComponent<Text>().text = "Player: " + gc.GetGroupName();
            transform.Find("InfoPanel/MoneyText").GetComponent<Text>().text = "Money: " + gc.GetMoney() + "G";
        }
    }	
}
