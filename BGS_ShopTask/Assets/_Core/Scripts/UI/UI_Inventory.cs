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

    void Start()
    {
        
    }

    private void OnDestroy()
    {
        
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (uiContainer.activeInHierarchy)
            {
                CloseView();
            }
            else
            {
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
        uiContainer.SetActive(true);
        playerGold.text = PlayerData.Ins.inventory.gold.ToString();
        BuildPlayerItems();
        playerGold.text = PlayerData.Ins.inventory.gold.ToString();
    }

    public void CloseView()
    {
        uiContainer.SetActive(false);
    }

    private void BuildPlayerItems()
    {
        //Clear previous list.
        foreach (Transform child in playerItemList.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in PlayerData.Ins.inventory.apparelInventory)
        {
            UI_Item storeItem = Instantiate(itemCellPrefab, playerItemList.transform);
            storeItem.Setup(item.Value, StoreTypeInteraction.EQUIP);
            storeItem.OnItemEquipped += OnItemEquipped;
        }

        playerScroll.verticalNormalizedPosition = 1f;
    }

    private void OnItemEquipped(Apparel _apparel)
    {
        playerDialogue.text = "Equiped: "+_apparel.apparelName;
    }
}
