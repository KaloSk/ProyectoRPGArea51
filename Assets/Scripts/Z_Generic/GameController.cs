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

    static int LastLevel { get; set; }

    static int OpenLevel { get; set; }

    public GameController()
    {
        
    }

    public GameController(List<UnityEngine.Sprite> ItemsSprite)
    {
    }

    public void NewGame(){

        LastLevel = 1;
        OpenLevel = 1;

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
                    HP = 80,
                    MP = 0,
                    ATK = 6,
                    DEF = 9,
                    MAG = 0,
                    MDF = 7,
                    SPE = 5,
                    LUK = 0
                },
                StatsByLevel = new Stats()
                {
                    HP = 50,
                    MP = 0,
                    ATK = 5,
                    DEF = 7,
                    MAG = 0,
                    MDF = 6,
                    SPE = 4,
                    LUK = 0
                }
            }
        };

        AllCharacters.Add(new Character()
        {
            ID = 2,
            Level = 1,
            Name = "D. Healer",
            IsPlayer = true,
            IsRange = true,
            Formation = 1,
            Stats = new Stats()
            {
                HP = 40,
                MP = 60,
                ATK = 2,
                DEF = 3,
                MAG = 6,
                MDF = 6,
                SPE = 4,
                LUK = 0
            },
            StatsByLevel = new Stats()
            {
                HP = 10,
                MP = 0,
                ATK = 2,
                DEF = 5,
                MAG = 5,
                MDF = 6,
                SPE = 3,
                LUK = 0
            }
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
                HP = 60,
                MP = 0,
                ATK = 8,
                DEF = 3,
                MAG = 0,
                MDF = 4,
                SPE = 8,
                LUK = 0
            },
            StatsByLevel = new Stats()
            {
                HP = 25,
                MP = 0,
                ATK = 8,
                DEF = 3,
                MAG = 0,
                MDF = 4,
                SPE = 9,
                LUK = 0
            }
        });

        CurrentCharacters = new List<int>
        {
            1
        };

        Enemies = new List<Enemy>{
            new Enemy{
                ID = 100,
                IsPlayer = false,
                Name = "Slime",
                Stats = new Stats
                {
                    HP = 10,
                    MP = 0,
                    ATK = 12,
                    DEF = 2,
                    MAG = 0,
                    MDF = 2,
                    SPE = 1,
                    LUK = 0
                },
                Money = 100
            },
            new Enemy{
                ID = 101,
                IsPlayer = false,
                Name = "Fire Spirit",
                Stats = new Stats
                {
                    HP = 15,
                    MP = 0,
                    ATK = 18,
                    DEF = 3,
                    MAG = 0,
                    MDF = 2,
                    SPE = 1,
                    LUK = 0
                },
                Money = 150
            },
            new Enemy{
                ID = 102,
                IsPlayer = false,
                Name = "King Slime",
                Stats = new Stats
                {
                    HP = 100,
                    MP = 0,
                    ATK = 30,
                    DEF = 10,
                    MAG = 0,
                    MDF = 10,
                    SPE = 10,
                    LUK = 0
                },
                Money = 150,
                IsBoss = true
            },
            new Enemy{
                ID = 104,
                IsPlayer = false,
                Name = "?????",
                IsRange = true,
                Stats = new Stats
                {
                    HP = 30,
                    MP = 0,
                    ATK = 35,
                    DEF = 8,
                    MAG = 0,
                    MDF = 10,
                    SPE = 10,
                    LUK = 0
                },
                Money = 150,
                IsBoss = false
            }
        };

        #region "ITEMS"

        // 1. Get the data to be displayed
        Items = new List<Item>(){
            new Item(1, "Potion" , 0, "Restore 100HP to an ally", "ALLY|HEAL|100", null, 50),
            new Item(2, "Potion +", 0, "Restore 500HP to an ally", "ALLY|HEAL|500", null, 150),
            new Item(3, "Potion + +", 0,"Restore 1000HP to an ally", "ALLY|HEAL|1000", null, 500),
            new Item(4,  "Max. Potion", 0,"Restore fully HP to an ally", "ALLY|HEAL|9999", null, 1000),
            new Item(5,  "Blue Potion", 1, "Restore 100MP to an ally", "ALLY|MP|100", null, 5000),
            new Item(6,  "Antidote",  2,"Heal you from poisoning", "ALLY|RECOVER|POISON", null, 100),
            new Item(7,  "Fenix Feather", 3,"Revive one character in battle", "ALLY|RECOVER|KO", null, 250),
            new Item(8,  "Attack Pill",  4,"Increase attack by 10% during battle", "ALLY|INCREASE|ATK", null, 500),
            new Item(9,  "Defense Pill",  4,"Increase defense by 10% during battle", "ALLY|INCREASE|DEF", null, 500),
            new Item(10,  "Speed Pill", 4, "Increase speed by 10% during battle", "ALLY|INCREASE|SPE", null, 500),
            new Item(11,  "Magic Pill", 4, "Increase magic attack by 10% during battle", "ALLY|INCREASE|MAG", null, 500),
            new Item(12,  "Spirit Pill", 4, "Increase magic defense by 10% during battle", "ALLY|INCREASE|RES", null, 500),
            new Item(13,  "Fire Scroll", 5, "Deal fire damage", "ENEMIES|DAMAGE|1.5", null, 1000),
            new Item(14,  "Wind Scroll",  5,"Deal wind damage", "ENEMIES|DAMAGE|1.5", null, 1000),
            new Item(15,  "Water Scroll",  5,"Deal water damage", "ENEMIES|DAMAGE|1.5", null, 1000),
            new Item(16,  "Earth Scroll",  5,"Deal earth damage", "ENEMIES|DAMAGE|1.5", null, 1000),
            new Item(17,  "Dark Scroll",  5,"Deal dark magic damage", "ENEMIES|DAMAGE|1.5", null, 1000),
            new Item(18,  "Light Scroll",  5,"Deal light magic damage", "ENEMIES|DAMAGE|1.5", null, 1000),
            new Item(19,  "Daggers **",  6,"Refill Daggers\n**Phantom Dagger Only", "", null, 100),
            new Item(20,  "Orb", 7, "Level Up character","", null, 9999)
        };

        ShopItem = new List<Item>()
        {
            Items[0],Items[1]
        };

        #endregion

        #region "MONEY"

        Money = 2000;

        #endregion

    }

    public void RestartEnemies()
    {
        Enemies[0].Stats.HP = 10;
        Enemies[1].Stats.HP = 15;
        Enemies[2].Stats.HP = 50;
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

    public List<Item> GetItemList()
    {
        return Items;
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

    public string GetGroupName()
    {
        return GroupName;
    }

    public void SetGroupName(string name)
    {
        GroupName = name;
    }

    /******LEVEL*****/
    public int GetLastLevel()
    {
        return LastLevel;
    }

    public void SetLastLevel(int level)
    {
        LastLevel = level;
    }

    public void IncreaseLevel()
    {
        LastLevel++;
    }

    public int GetOpenLevel()
    {
        return OpenLevel;
    }

    public void SetOpenLevel(int level)
    {
        OpenLevel = level;
    }
}
