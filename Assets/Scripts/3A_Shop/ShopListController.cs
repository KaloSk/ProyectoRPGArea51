using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ShopListController : MonoBehaviour {

    public Sprite[] ItemImages;
    public GameObject ContentPanel;
    public GameObject ListItemPrefab;

    List<Item> ItemsInShop;

    void Start() {



        // 1. Get the data to be displayed
        ItemsInShop = new List<Item>(){
            new Item(1, ItemImages[0],
                       "Green Potion",
                       "Restores\t:\t10HP"),
            new Item(2, ItemImages[1],
                       "Blue Potion",
                       "Restores\t:\t10MP"),
            new Item(3, ItemImages[2],
                       "Red Potion",
                        "Heals\t:\t5HP"),
            new Item(4, ItemImages[3],
                       "Antidote",
                       "Heals you from poisoning"),
            new Item(5, ItemImages[4],
                       "Fenix feather",
                       "Revives one character in battle"),
            new Item(1, ItemImages[5],
                       "Attack Pill",
                       "Increases attack by 10%"),
            new Item(1, ItemImages[6],
                       "Defense Pill",
                       "Increases defense by 10%"),
            new Item(1, ItemImages[7],
                       "Speed Pill",
                       "Increases speed by 10%"),
            new Item(1, ItemImages[8],
                       "Especial Attack Pill",
                       "Increases ESP.Attack by 10%"),
            new Item(1, ItemImages[9],
                       "Especial Defense Pill",
                       "Increases ESP.Defense by 10%"),
            new Item(1, ItemImages[10],
                       "Lucky Pill",
                       "Increases Luck by 10%"),
            new Item(1, ItemImages[11],
                       "Fire Scroll",
                       "Deals fire damage"),
            new Item(1, ItemImages[12],
                       "Wind Scroll",
                       "Deals wind damage"),
            new Item(1, ItemImages[13],
                       "Water Scroll",
                       "Deals water damage"),
            new Item(1, ItemImages[14],
                       "Earth Scroll",
                       "Deals earth damage"),
            new Item(1, ItemImages[15],
                       "Dark Scroll",
                       "Deals dark magic damage"),
            new Item(1, ItemImages[16],
                       "Light Scroll",
                       "Deals light magic damage"),
            new Item(1, ItemImages[17],
                       "Daggers",
                       "Refill Daggers(Phantom Dagger Only)"),
            new Item(1, ItemImages[18],
                       "Orb",
                       "Level Up character"),
            new Item(1, ItemImages[19],
                       "Max Potion",
                       "Restores all HP"),
        };

        // 2. Iterate through the data, 
        //    instantiate prefab, 
        //    set the data, 
        //    add it to panel
        foreach (Item shopItem in ItemsInShop)
        {
            GameObject newShopItem = Instantiate(ListItemPrefab) as GameObject;
            ShopListItemControler controller = newShopItem.GetComponent<ShopListItemControler>();
            controller.Icon.sprite = shopItem.Icon;
            controller.Name.text = shopItem.Name;
            controller.Description.text = shopItem.Description;
            newShopItem.transform.parent = ContentPanel.transform;
            newShopItem.transform.localScale = Vector3.one;
            var newi = shopItem.ID;
            UnityAction<int> action = new UnityAction<int>(BuyItem);
            newShopItem.GetComponent<Button>().onClick.AddListener(delegate
            {
                action.Invoke(newi);
            });
        }
    }

    void BuyItem (int itemID){
        Debug.Log("comprando" + itemID) ;

        GameController gameController = new GameController();

        gameController.GetCharacterItemList().Add(new PlayerItem()
        {
            Quantity = 1,
            Item = ItemsInShop.Find(it => it.ID == itemID)
        });

    }
}
