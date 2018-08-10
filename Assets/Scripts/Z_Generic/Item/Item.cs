using UnityEngine;

public class Item  {

    public int ID { get; set; }
    public string Name { get; set; }
    public ItemType Type { get; set; }
    public Sprite Icon { get; set; }
    public string Description { get; set; }
    public string Formula { get; set; }
    public Stats Stats { get; set; }

}
