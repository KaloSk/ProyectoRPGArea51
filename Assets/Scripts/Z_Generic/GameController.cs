﻿using System.Collections.Generic;

[System.Serializable]
public class GameController {
    
    static string GroupName { get; set; }

    static int Money { get; set; }

	static int GameTurn { get; set;}

    static List<Character> Characters { get; set; }

    static List<int> CurrentCharacters { get; set; }

    static List<PlayerItem> CharacterItem { get; set; }

    static List<Item> Items { get; set; }

    static List<Enemy> Enemies { get; set; }
    
    public GameController()
    {
        
    }

    public GameController(List<UnityEngine.Sprite> ItemsSprite)
    {
        ItemTest(ItemsSprite);
    }

    public void NewGame(){

        Characters = new List<Character>
        {
            new Character()
            {
                ID = 1,
                Level = 1,
                Name = "Sir Stone",
                IsPlayer = true,
                Formation = 1,
                Stats = new Stats()
                {
                    HP = 8,
                    MP = 0,
                    ATK = 5,
                    DEF = 9,
                    MAG = 0,
                    MDF = 7,
                    SPE = 5,
                    LUK = 0
                },

            }
        };

        /*Characters.Add(new Character()
        {
            ID = 2,
            Level = 1,
            Name = "D. Healer",
            IsPlayer = true,
            Formation = 1,
            Stats = new Stats()
            {
                HP = 4,
                MP = 6,
                ATK = 2,
                DEF = 3,
                MAG = 6,
                MDF = 6,
                SPE = 4,
                LUK = 0
            },

        });

        Characters.Add(new Character()
        {
            ID = 3,
            Level = 1,
            Name = "P. Dagger",
            IsPlayer = true,
            Formation = 1,
            Stats = new Stats()
            {
                HP = 6,
                MP = 0,
                ATK = 8,
                DEF = 3,
                MAG = 0,
                MDF = 4,
                SPE = 8,
                LUK = 0
            },

        });*/

        CurrentCharacters = new List<int>
        {
            1
        };


        Enemies = new List<Enemy>(){
            new Enemy(){
                ID = 1,
                IsPlayer = false,
                Name = "Slime",
                Stats = new Stats()
                {
                    HP = 10,
                    MP = 0,
                    ATK = 7,
                    DEF = 2,
                    MAG = 0,
                    MDF = 2,
                    SPE = 1,
                    LUK = 0
                }
            }
        };
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

       

        

    }

    public int GetMoney()
    {
        return Money;
    }

    public List<Character> GetCharactersList(){
        return Characters;
    }

    public List<Enemy> GetEnemiesList()
    {
        return Enemies;
    }

    public List<int> GetCurrentCharactersList(){
        return CurrentCharacters;
    }

    public List<PlayerItem> GetCharacterItemList()
    {
        if (CharacterItem == null) CharacterItem = new List<PlayerItem>();/*CHECK LATER*/
        return CharacterItem;
    }

    public void SetGroupName(string name)
    {
        GroupName = name;
    }

}
