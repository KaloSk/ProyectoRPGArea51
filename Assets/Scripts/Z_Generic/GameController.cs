using System.Collections.Generic;

[System.Serializable]
public class GameController {
    
    static string GroupName { get; set; }

    static int Money { get; set; }

	static int GameTurn { get; set;}

    static List<Character> AllCharacters { get; set; }

    static List<Character> Characters { get; set; }

    static List<int> CurrentCharacters { get; set; }

    static List<PlayerItem> CharacterItem { get; set; }

    static List<Item> Items { get; set; }

    static List<Item> ShopItem { get; set; }

    static List<Enemy> Enemies { get; set; }

    public GameController()
    {
        
    }

    public GameController(List<UnityEngine.Sprite> ItemsSprite)
    {
        ItemTest(ItemsSprite);
    }

    public void NewGame(){

        AllCharacters = new List<Character>
        {
            new Character()
            {
                ID = 1,
                Level = 1,
                Name = "Sir Stone",
                IsPlayer = true,
                IsRange = false,
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

        AllCharacters.Add(new Character()
        {
            ID = 2,
            Level = 1,
            Name = "D. Healer",
            IsPlayer = true,
            IsRange = false,
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

        AllCharacters.Add(new Character()
        {
            ID = 3,
            Level = 1,
            Name = "P. Dagger",
            IsPlayer = true,
            IsRange = true,
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
            Skills = new List<Skill>()
            {
                new Skill()
                {
                    ID = 1,
                    Name = "Blind Spot",
                    Damage = 0,
                    Formula = "CHARACTER|RAISE|CRITICAL",
                    Level = 1,                    
                    Type = GameConstants.SKILL_TYPE_FOR_ENEMY
                },
            }
        });

        CurrentCharacters = new List<int>();
        CurrentCharacters.Add(3);

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

        #region "ITEMS"

        // 1. Get the data to be displayed
        Items = new List<Item>(){
            new Item(1, "Potion" , 1, "Restore 100HP to an ally", "ALLY|HEAL|100", null, 50),
            new Item(2, "Potion +", 1, "Restore 500HP to an ally", "ALLY|HEAL|500", null, 150),
            new Item(3, "Potion + +", 1,"Restore 1000HP to an ally", "ALLY|HEAL|1000", null, 500),
            new Item(4,  "Max. Potion", 1,"Restore fully HP to an ally", "ALLY|HEAL|9999", null, 1000),
            new Item(5,  "Blue Potion", 1, "Restore 100MP to an ally", "ALLY|MP|100", null, 5000),
            new Item(6,  "Antidote",  1,"Heal you from poisoning", "ALLY|RECOVER|POISON", null, 100),
            new Item(7,  "Fenix Feather",  1,"Revive one character in battle", "ALLY|RECOVER|KO", null, 250),
            new Item(8,  "Attack Pill",  1,"Increase attack by 10% during battle", "ALLY|INCREASE|ATK", null, 500),
            new Item(9,  "Defense Pill",  1,"Increase defense by 10% during battle", "ALLY|INCREASE|DEF", null, 500),
            new Item(10,  "Speed Pill", 1, "Increase speed by 10% during battle", "ALLY|INCREASE|SPE", null, 500),
            new Item(11,  "Magic Pill", 1, "Increase magic attack by 10% during battle", "ALLY|INCREASE|MAG", null, 500),
            new Item(12,  "Spirit Pill", 1, "Increase magic defense by 10% during battle", "ALLY|INCREASE|RES", null, 500),
            new Item(13,  "Fire Scroll", 2, "Deal fire damage", "ENEMIES|DAMAGE|1.5", null, 1000),
            new Item(14,  "Wind Scroll",  2,"Deal wind damage", "ENEMIES|DAMAGE|1.5", null, 1000),
            new Item(15,  "Water Scroll",  2,"Deal water damage", "ENEMIES|DAMAGE|1.5", null, 1000),
            new Item(16,  "Earth Scroll",  2,"Deal earth damage", "ENEMIES|DAMAGE|1.5", null, 1000),
            new Item(17,  "Dark Scroll",  2,"Deal dark magic damage", "ENEMIES|DAMAGE|1.5", null, 1000),
            new Item(18,  "Light Scroll",  2,"Deal light magic damage", "ENEMIES|DAMAGE|1.5", null, 1000),
            new Item(19,  "Daggers **",  3,"Refill Daggers\n**Phantom Dagger Only", "", null, 100),
            new Item(20,  "Orb", 0, "Level Up character","", null, 9999)
        };

        ShopItem = new List<Item>()
        {
            Items[0],Items[1],Items[5],Items[6],Items[7],Items[8],
            Items[13],Items[14]
        };

        #endregion

        #region "MONEY"

        Money = 1000;

        #endregion

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
    /***MONEY***/
    public int GetMoney()
    {
        return Money;
    }

    public void PayItem(int cost)
    {
        Money -=cost;
    }

    public List<Character> GetAllCharactersList()
    {
        return AllCharacters;
    }

    public List<Character> GetCharactersList(){
        if (Characters == null) Characters = new List<Character>();
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

    /*****ITEMS****/
    public List<Item> GetShopItems()
    {
        return ShopItem;
    }

    public void SetGroupName(string name)
    {
        GroupName = name;
    }

}
