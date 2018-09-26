using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BagButton : MonoBehaviour, IPointerClickHandler
{

	private Bag bag;

	[SerializeField]
	private Sprite full, empty;

	public Bag MyBag
	{
		get
		{
			return bag;
		}
		set
		{
			if (value != null)
			{
				GetComponent<Image>().sprite = full;
			}
			else
			{
				GetComponent<Image>().sprite = full;
			}
			bag = value;
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (bag != null)
		{
            InventoryScript.MyInstance.OpenClose();
			bag.MyBagScript.OpenClose();
        } else {
            InventoryScript.MyInstance.CreateNewBag();
        }
	}
}
