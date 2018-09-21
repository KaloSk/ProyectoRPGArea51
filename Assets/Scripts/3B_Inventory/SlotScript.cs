using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotScript : MonoBehaviour
{
	private Stack<ItemV2> items = new Stack<ItemV2>();

	[SerializeField]
	private Image icon;

	public bool IsEmpty
	{
		get
		{
			return items.Count == 0;
		}
	}

	public bool AddItem(ItemV2 item)
	{
		items.Push(item);
		icon.sprite = item.MyIcon;
		return true;
	}
}
