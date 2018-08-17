using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventoryManager : MonoBehaviour {


    public Sprite Backgroundsprite;
    public Sprite SlotSprite;


    Transform X1Y1;
    Transform X2Y1;
    Transform X1Y2;

    public Transform ItemsParent;
    public Transform SlotsParent;

    public GameObject ItemGoPrefab;
    public GameObject InventorySlotPrefab;


    public int Rows = 4;
    public int Columns = 5;
    public int SlotSize = 100;
    public int SpacingBetweenSlots = 30;
    public int TopBottomMargin;
    public int RightLeftMargin;
    public int TopBottomSpace = 100;
    public int RightLeftSpace = 100;


    int MaxNumberOfItemsALLinventory;


    void TransformsLoader () {
        if (ItemsParent == null)
        {
            ItemsParent = transform.Find("ItemsParent");
        }
        if (SlotsParent == null)
        {
            SlotsParent = transform.Find("SlotsParent");
        }
	}

	private void Start()
	{
        TransformsLoader();
        PrefabLoader();
        MaxNumberOfItemsALLinventory = Columns * Rows;
        StartCoroutine(AssignXYPos());
	}

    IEnumerator AssignXYPos()
    {
        yield return new WaitForEndOfFrame();
        if (Columns == 1 && SlotsParent.childCount > 1)
        {
            X1Y1 = SlotsParent.GetChild(0);
            X1Y2 = SlotsParent.GetChild(1);
            X2Y1 = SlotsParent.GetChild(0);
        }
        else if (Rows == 1 && SlotsParent.childCount > 1)
        {
            X1Y1 = SlotsParent.GetChild(0);
            X1Y2 = SlotsParent.GetChild(0);
            X2Y1 = SlotsParent.GetChild(1);
        }
        else if (SlotsParent.childCount > Columns + 1)
        {
            X1Y1 = SlotsParent.GetChild(0);
            X1Y2 = SlotsParent.GetChild(Columns + 1);
            X2Y1 = SlotsParent.GetChild(1);
        }
    }

	// Update is called once per frame
	void Update () {
        TransformsLoader();
        PrefabLoader();
        MaxNumberOfItemsALLinventory = Columns * Rows;
        StartCoroutine(AssignXYPos());

	}

    void PrefabLoader()
    {
        if (ItemGoPrefab == null)
        {
            ItemGoPrefab = Resources.Load<GameObject>("Prefabs/ItemInventoryGO");
        }
        if (InventorySlotPrefab == null)
        {
            InventorySlotPrefab = Resources.Load<GameObject>("Prefabs/InventorySlot");
        }
    }
}
