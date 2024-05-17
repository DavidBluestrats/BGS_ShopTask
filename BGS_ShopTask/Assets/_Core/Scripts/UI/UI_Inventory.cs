using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    [Header("Player's Containers, Scrollers & UI Elements")]
    public GameObject uiContainer;
    public GameObject playerItemList;
    public ScrollRect playerScroll;
    public TMP_Text playerDialogue;
    public TMP_Text playerGold;

    [Header("Prefabs")]
    public UI_Item itemCellPrefab;

    [Header("Class variables")]
    public Dictionary<ApparelType, UI_Item> equippedItemsCells;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (UiIsOpen())
            {
                CloseView();
            }
            else
            {
                if(!PlayerData.Ins.playerUiIsBusy)
                    InvokeView();
            }
        }
    }

    public bool UiIsOpen()
    {
        return uiContainer.activeInHierarchy;
    }

    public void InvokeView()
    {
        //Setup UI data.
        uiContainer.SetActive(true);
        
        playerGold.text = PlayerData.Ins.inventory.gold.ToString();
        playerDialogue.text = "My Inventory";

        equippedItemsCells = new Dictionary<ApparelType, UI_Item>();

        BuildPlayerItems();
        PlayerData.Ins.playerUiIsBusy = true;
    }

    public void CloseView()
    {
        uiContainer.SetActive(false);
        PlayerData.Ins.playerUiIsBusy = false;
    }

    private void BuildPlayerItems()
    {
        //Clear previous list.
        foreach (Transform child in playerItemList.transform)
        {
            Destroy(child.gameObject);
        }

        //Populate the inventory scroller with each item in the player's inventory.
        foreach (var item in PlayerData.Ins.inventory.apparelInventory)
        {
            UI_Item storeItem = Instantiate(itemCellPrefab, playerItemList.transform);
            storeItem.Setup(item.Value, StoreTypeInteraction.EQUIP);
            storeItem.OnItemEquipped += OnItemEquipped;

            if (PlayerData.Ins.playerVisualManager.PlayerIsWearingApparel(item.Value))
            {
                equippedItemsCells.Add(item.Value.type, storeItem);
                storeItem.SetEquipped(true);
            }
        }
        //Reset the scroller so that it shows the item at the top of the list.
        playerScroll.verticalNormalizedPosition = 1f;
    }

    private void OnItemEquipped(Apparel _apparel, UI_Item _itemCell)
    {
        //Give feedback of equipped item.
        playerDialogue.text = "Equiped: "+_apparel.apparelName;

        if (equippedItemsCells.ContainsKey(_apparel.type))
        {
            equippedItemsCells[_apparel.type].SetEquipped(false);
            equippedItemsCells.Remove(_apparel.type);

            equippedItemsCells.Add(_itemCell.apparelItem.type, _itemCell);
            _itemCell.SetEquipped(true);
        }
    }

}
