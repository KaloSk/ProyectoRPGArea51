using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ItemListForItem : MonoBehaviour {

    GameController gc = new GameController();

    public GameObject ContentItemLarge;
    public GameObject ItemLargePrefab;

    // Use this for initialization
    void Start()
    {
        if (gc.GetShopItems() == null)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            for (var i = 0; i < ContentItemLarge.transform.childCount; i++)
            {
                Destroy(ContentItemLarge.transform.GetChild(i));
            }

            gc = new GameController();

            foreach (var i in gc.GetCharacterItemList())
            {
                ItemLargePrefab.transform.localScale.Set(1, 1, 1);
                GameObject face = Instantiate(ItemLargePrefab);
                face.transform.localScale.Set(1, 1, 1);
                face.name = "ItemLarge" + i.Item.ID;
                face.transform.Find("ItemText").GetComponent<Text>().text = i.Item.Name;
                face.transform.Find("DescriptionText").GetComponent<Text>().text = i.Item.Description;
                face.transform.Find("Total/Text").GetComponent<Text>().text = i.Quantity.ToString();
                //face.transform.GetComponent<Image>().sprite = i.Item.Icon;
                face.transform.parent = ContentItemLarge.transform;

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
