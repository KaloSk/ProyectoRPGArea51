using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem {

    public Sprite Icon;
    public string Name, Description;

    public ShopItem(Sprite icon, string name, string description) {
        Icon = icon;
        Name = name;
        Description = description;
    }
}
