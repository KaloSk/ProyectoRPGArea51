using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopListController : MonoBehaviour {

    public Sprite[] ItemImages;
    public GameObject ContentPanel;
    public GameObject ListItemPrefab;

    ArrayList ItemsInShop;

    void Start() {

        // 1. Get the data to be displayed
        ItemsInShop = new ArrayList(){
            new ShopItem(ItemImages[0],
                       "PocionVerde",
                       "Heals\t:\t10HP"),
            new ShopItem(ItemImages[1],
                       "PocionAzul",
                       "Restores\t:\t10MP"),
            new ShopItem(ItemImages[2],
                       "Objeto",
                       "Power\t:\t5\nAttack\t:\t5\nTame\t:\t10\nVenom\t:\t0"),
            new ShopItem(ItemImages[3],
                       "Objeto2",
                       "Power\t:\t5\nAttack\t:\t5\nTame\t:\t10\nVenom\t:\t0"),
            /*new ShopItem(ItemImages[4],
                       "Objeto3",
                       "Power\t:\t5\nAttack\t:\t5\nTame\t:\t10\nVenom\t:\t0"),
            new ShopItem(ItemImages[5],
                       "Objeto4",
                       "Power\t:\t5\nAttack\t:\t5\nTame\t:\t10\nVenom\t:\t0")*/
        };

        // 2. Iterate through the data, 
        //    instantiate prefab, 
        //    set the data, 
        //    add it to panel
        foreach (ShopItem shopItem in ItemsInShop)
        {
            GameObject newShopItem = Instantiate(ListItemPrefab) as GameObject;
            ShopListItemControler controller = newShopItem.GetComponent<ShopListItemControler>();
            controller.Icon.sprite = shopItem.Icon;
            controller.Name.text = shopItem.Name;
            controller.Description.text = shopItem.Description;
            newShopItem.transform.parent = ContentPanel.transform;
            newShopItem.transform.localScale = Vector3.one;
        }
    }
}
