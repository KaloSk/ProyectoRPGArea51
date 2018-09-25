using UnityEngine;

public class Item  {

    public int ID { get; set; }
    public string Name { get; set; }
    public int Type { get; set; }
    public Sprite Icon { get; set; }
    public string Description { get; set; }
    public string Formula { get; set; }
    public Stats Stats { get; set; }    
    public int Cost { get; set; }

    public Item()
    {}

    public Item(int id, Sprite icon, string name, string description, int cost)
    {
        ID = id;
        Icon = icon;
        Name = name;
        Description = description;
        Cost = cost;
    }

    public Item(int iD, string name, int type, string description, string formula, Stats stats, int cost)
    {
        ID = iD;
        Name = name;
        Type = type;
        Description = description;
        Formula = formula;
        Stats = stats;
        Cost = cost;
    }
}
