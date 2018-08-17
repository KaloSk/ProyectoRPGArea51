using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopListController : MonoBehaviour {

    public Sprite[] ItemImages;
    public GameObject ContentPanel;
    public GameObject ListItemPrefab;

    ArrayList ItemsInShop;

    void Start()
    {

        // 1. Get the data to be displayed
        ItemsInShop = new ArrayList(){
            new ShopItem(ItemImages[0],
                       "Cat",
                       "Power\t:\t5\nAttack\t:\t5\nTame\t:\t10\nVenom\t:\t0"),
            new ShopItem(ItemImages[1],
                       "Dog",
                       "Power\t:\t5\nAttack\t:\t5\nTame\t:\t10\nVenom\t:\t0"),
            new ShopItem(ItemImages[2],
                       "Fish",
                       "Power\t:\t5\nAttack\t:\t5\nTame\t:\t10\nVenom\t:\t0"),
            new ShopItem(ItemImages[3],
                       "Parrot",
                       "Power\t:\t5\nAttack\t:\t5\nTame\t:\t10\nVenom\t:\t0"),
            new ShopItem(ItemImages[4],
                       "Rabbit",
                       "Power\t:\t5\nAttack\t:\t5\nTame\t:\t10\nVenom\t:\t0"),
            new ShopItem(ItemImages[5],
                       "Snail",
                       "Power\t:\t5\nAttack\t:\t5\nTame\t:\t10\nVenom\t:\t0")
        };

        // 2. Iterate through the data, 
        //    instantiate prefab, 
        //    set the data, 
        //    add it to panel
        foreach (ShopItem shopItem in ItemsInShop)
        {
            /*GameObject newShopItem = Instantiate(ListItemPrefab) as GameObject;
            ShopListItemControler controller = newShopItem.GetComponent();
            controller.Icon.sprite = shopItem.Icon;
            controller.Name.text = shopItem.Name;
            controller.Description.text = shopItem.Description;
            newShopItem.transform.parent = ContentPanel.transform;
            newShopItem.transform.localScale = Vector3.one;*/
        }
    }
}
