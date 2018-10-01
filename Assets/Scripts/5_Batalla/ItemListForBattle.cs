using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ItemListForBattle : MonoBehaviour {

    AudioSource audioSource;

    GameController gc = new GameController();

    public GameObject ItemShortPrefab;
    public AudioClip ItemSound;

    // Use this for initialization
    void Start () {
        if (gc.GetShopItems() == null)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            audioSource = new AudioSource();

            for (var i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i));
            }

            gc = new GameController();

            foreach (var i in gc.GetCharacterItemList())
            {
                GameObject face = Instantiate(ItemShortPrefab);
                face.transform.localScale.Set(1, 1, 1);
                face.name = "ItemShort" + i.Item.ID;
                face.transform.Find("Text").GetComponent<Text>().text = i.Item.Name;                
                face.transform.Find("Total/Text").GetComponent<Text>().text = i.Quantity.ToString();
                face.transform.Find("Image").GetComponent<Image>().sprite = i.Item.Icon;
                face.transform.parent = transform;

                var newi = i.Item.ID;
                UnityAction<int> action = new UnityAction<int>(UseItem);
                face.GetComponent<Button>().onClick.AddListener(delegate { action.Invoke(newi); });
            }

            /*foreach (var i in game.GetCurrentCharactersList())
            {
                AddSprite(i);
            }*/
        }
    }	

    public void UseItem(int id)
    {
        var item = gc.GetCharacterItemList().Find(o => o.Item.ID == id).Item;
        GameObject.Find("BattlePanel/ObjectPanel/DescripcionPanel/Text").GetComponent<Text>().text = item.Description;

        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.PlayOneShot(ItemSound);

        GameObject.Find("Canvas").GetComponent<RTP>().SetBattleStatusForItem(item);

    }
}
