using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Items")]
    public int gold;
    public Dictionary<int, Apparel> apparelInventory;

    [Header("Starting Inventory")]
    public List<Apparel> startingList;

    [Header("Events")]
    public Action<Apparel> OnItemGiven;
    public Action<Apparel> OnItemRemoved;

    void Start()
    {
        FillInitialItems();
    }

    private void FillInitialItems()
    {
        //Basically add the default clothes to the player's inventory
        apparelInventory = new Dictionary<int, Apparel>();

        foreach (var apparel in startingList)
        {
            apparelInventory.Add(apparel.id, apparel);
        }
    }

    /*
    Adding on give and remove events foreseeing selling, buying and equipping
    items interactions.

    Mainly to separate logic between inventory and UI so that the only way they
    get to know of each other's behaviour is through events.

    */

    public void GiveItem(Apparel _apparelItem)
    {
        apparelInventory.Add(_apparelItem.id, _apparelItem);
        OnItemGiven?.Invoke(_apparelItem);
    }

    public void RemoveItem(Apparel _apparelItem)
    {
        if (apparelInventory.ContainsKey(_apparelItem.id))
        {
            apparelInventory.Remove(_apparelItem.id);
            OnItemRemoved?.Invoke(_apparelItem);
        }

    }

    public void GiveGold(int _ammount)
    {
        gold += _ammount;
    }

    public void RemoveGold(int _ammount)
    {
        gold -= _ammount;
        gold = gold <= 0 ? 0 : gold;
    }
}
