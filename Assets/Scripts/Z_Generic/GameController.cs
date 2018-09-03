using System.Collections.Generic;

[System.Serializable]
public class GameController {

    static List<Character> Characters { get; set; }

    static List<PlayerItem> CharacterItem { get; set; }

    static List<Item> Items { get; set; }
    
    public GameController()
    {
    }

    public GameController(List<UnityEngine.Sprite> ItemsSprite)
    {
        ItemTest(ItemsSprite);
    }

    void ItemTest(List<UnityEngine.Sprite> ItemsSprite)
    {

        #region "ITEM TYPE LIST"

        var ItemTypeList = new List<ItemType>
        {
            new ItemType()
            {
                ID = 1,
                Name = "Potion",
                IsWeapon = false,
                IsArmor = false,
                IsOther = false
            },
            new ItemType()
            {
                ID = 2,
                Name = "Weapon",
                IsWeapon = true,
                IsArmor = false,
                IsOther = false
            }
        };

        #endregion

        Items = new List<Item>
        {
            new Item()
            {
                ID = 1,
                Name = "Poción 1",
                Description = "Restaura el 20% de vida",
                Type = ItemTypeList[0],
                Formula = "HEAL|ALLY|20%",
                Icon = ItemsSprite[1]
            }
        };


        CharacterItem = new List<PlayerItem>()
        {
           new PlayerItem()
           {
                Item = Items[0],
                Quantity = 10
           }
        };

        

    }

}
