using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterListForItem : MonoBehaviour {

    GameController gc = new GameController();

    public GameObject ContentCharacterFace;
    public GameObject CharacterFacePrefab;

    // Use this for initialization
    void Start () {

        if (gc.GetShopItems() == null)
        {
            SceneManager.LoadScene(0);
        }
        else
        {

            for (var i = 0; i < ContentCharacterFace.transform.childCount; i++)
            {
                Destroy(ContentCharacterFace.transform.GetChild(i));
            }

            gc = new GameController();

            foreach (var i in gc.GetCharactersList())
            {
                GameObject face = Instantiate(CharacterFacePrefab);
                face.name = "CharacterFace" + i.ID;
                face.transform.Find("Panel/Text").GetComponent<Text>().text = i.Name;
                face.transform.GetComponent<Image>().sprite = i.Face;
                face.transform.parent = ContentCharacterFace.transform;

                /*var newi = i.ID;
                UnityAction<int> action = new UnityAction<int>(AddCharacter);
                face.GetComponent<Button>().onClick.AddListener(delegate { action.Invoke(newi); });*/
            }

            /*foreach (var i in game.GetCurrentCharactersList())
            {
                AddSprite(i);
            }*/
        }
    }
}
