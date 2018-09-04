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

	[SerializeField]
	private ItemV2[] items;

	private void Awake()
	{
		Bag bag = (Bag)Instantiate(items[0]);
		bag.Initialize(20);
		bag.Use();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
