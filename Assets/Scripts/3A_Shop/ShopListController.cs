using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ShopListController : MonoBehaviour {

    public Sprite[] ItemImages;
    public GameObject ListItemPrefab;

    public List<AudioClip> BuySoundList;

    GameController gc = new GameController();

    void Start() {        
        if (gc.GetShopItems() == null) {
            SceneManager.LoadScene(0);
        }
        else
        {
            foreach (Item shopItem in gc.GetShopItems())
            {
                GameObject newShopItem = Instantiate(ListItemPrefab) as GameObject;
                newShopItem.name = "Item" + shopItem.ID;
                ShopListItemControler controller = newShopItem.GetComponent<ShopListItemControler>();
                controller.Icon.sprite = shopItem.Icon;
                controller.Name.text = shopItem.Name;
                controller.Description.text = shopItem.Description;
                controller.Price.text = shopItem.Cost.ToString() + "G";
                newShopItem.transform.parent = transform;
                newShopItem.transform.localScale = Vector3.one;
                var newi = shopItem.ID;
                UnityAction<int> action = new UnityAction<int>(BuyItem);
                newShopItem.GetComponent<Button>().onClick.AddListener(delegate
                {
                    action.Invoke(newi);
                });
            }
        }       
    }

    void BuyItem (int itemID){

        var item = gc.GetCharacterItemList().Find(ii => ii.Item.ID == itemID);

        var audioSource = gameObject.GetComponent<AudioSource>();

        var buyThis = gc.GetShopItems().Find(it => it.ID == itemID);

        var CustomText = GameObject.Find("CustomText").GetComponent<CustomMessage>();

        if (gc.GetMoney() < buyThis.Cost)
        {
            CustomText.NotCashItem();
            audioSource.PlayOneShot(BuySoundList[1]);            
        }
        else
        {
            CustomText.BuyItem();
            audioSource.PlayOneShot(BuySoundList[0]);
            if (item != null)
            {
                item.Quantity++;
            }
            else
            {
                gc.GetCharacterItemList().Add(new PlayerItem()
                {
                    Quantity = 1,
                    Item = gc.GetShopItems().Find(it => it.ID == itemID)
                });
            }
            gc.PayItem(buyThis.Cost);
        }
    }
    
}
