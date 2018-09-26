using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour {

private static InventoryScript instance;

public static InventoryScript MyInstance
{
	get
	{
		if (instance == null)
		{
			instance = FindObjectOfType<InventoryScript>();
		}

		return instance;
	}
}

private List <Bag> bags = new List<Bag>();

    private Bag selectedBag;

	[SerializeField]
	private BagButton[] bagButtons;

	
	[SerializeField]
	private ItemV2[] items;

    public bool CanAddBag
    {
        get { return bags.Count < 6; }
    }

	private void Awake()
	{
		Bag bag = (Bag)Instantiate(items[0]);
		bag.Initialize(20);
		bag.Use();
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.J))
		{
		
		}

        if(Input.GetKeyDown(KeyCode.K))
        {
            Bag bag = (Bag)Instantiate(items[0]);
            bag.Initialize(20);
            AddItem(bag);
        }
	}

	public void UpdateStackSize(IClickable clickable)
	{
		if (clickable.MyCount == 0)
		{
			clickable.MyIcon.color = new Color(0, 0, 0, 0);
		}
	}

	public void AddBag (Bag bag)
	{ 
		foreach (BagButton bagButton in bagButtons)
		{
			if (bagButton.MyBag == null)
			{
				bagButton.MyBag = bag;
				bags.Add(bag);
				break;
			}
		}
	}

	public void AddItem (ItemV2 item)
	{
		foreach (Bag bag in bags)
		{
			if (bag.MyBagScript.AddItem(item))
			{
				return;
			}
		}
	}

	public void OpenClose()
	{
        Bag[] openBags = bags.FindAll(x => x.MyBagScript.IsOpen).ToArray();

        foreach (Bag bag in openBags)
		{
		    bag.MyBagScript.OpenClose();
		}
	}

    public void CreateNewBag () {
        Bag bag = (Bag)Instantiate(items[0]);
        bag.Initialize(20);
        bag.Use();
    }
}
