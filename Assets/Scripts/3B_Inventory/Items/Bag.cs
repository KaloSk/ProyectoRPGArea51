using System.Collections;
using System.Collections.Generic;
using UnityEngine;

  [CreateAssetMenu(fileName ="Bag",menuName ="Items/Bag",order = 1)]
	public class Bag : ItemV2, IUseable
{
	private int slots;

	[SerializeField]
	private GameObject bagPrefab;

	/*public Sprite MyIcon{
		get;
	}*/



	public BagScript MyBagScript { get; set; }

	public int Slots
	{
		get
		{
			return slots;
		}
	}

	public void Initialize(int slots)
	{
		this.slots = slots;
	}

	public void Use()
	{
		if(InventoryScript.MyInstance.CanAddBag)
		{ 
			Remove();
			MyBagScript = GameObject.Instantiate(bagPrefab, InventoryScript.MyInstance.transform).GetComponent<BagScript>();
			MyBagScript.AddSlots(slots);

			InventoryScript.MyInstance.AddBag(this);
		}
	}
}
