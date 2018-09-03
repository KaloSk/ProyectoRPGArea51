using UnityEngine;

public class ItemType {

    public int ID { get; set; }
    public string Name { get; set; }
    public bool IsWeapon { get; set; }
    public bool IsArmor { get; set; }
    public bool IsOther { get; set; }

    public ItemType()
    {
    }

    public ItemType(int iD, string name, bool isWeapon, bool isArmor, bool isOther)
    {
        ID = iD;
        Name = name;
        IsWeapon = isWeapon;
        IsArmor = isArmor;
        IsOther = isOther;
    }

}
