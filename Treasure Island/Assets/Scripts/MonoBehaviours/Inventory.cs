using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    public Image[] itemImages = new Image[numItemSlots];
    public Item[] items = new Item[numItemSlots];

    public const int numItemSlots = 12;

    public void AddItem(Item itemToAdd)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                items[i] = itemToAdd;
                itemImages[i].sprite = itemToAdd.sprite;
                itemImages[i].enabled = true;
                return;
            }
        }
        Debug.Log("Inventory was full. Could not add " + itemToAdd.name + " to Inventory.");
    }

    public void RemoveItem(Item itemToRemove)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == itemToRemove)
            {
                items[i] = null;
                itemImages[i].sprite = null;
                itemImages[i].enabled = false;
                return;
            }
        }
        Debug.Log("Item could not be found (RemoveItem issue in class Inventory");
    }

    public bool CheckForItem (Item itemToCheck)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == itemToCheck)
            {
                return true;
            }
        }
        return false;
    }
}
