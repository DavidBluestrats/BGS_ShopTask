using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Cheats : MonoBehaviour
{

    [Header("Cheats Panel")]

    public Inventory vendorInventory;
    public Button addGoldToPlayer;
    public Button removeGoldFromPlayer;

    public Button addGoldToVendor;
    public Button removeGoldFromVendor;

    public TMP_Text playerGold;

    private void Start()
    {
        addGoldToPlayer.onClick.AddListener(OnAddGoldPlayerClick);
        removeGoldFromPlayer.onClick.AddListener(OnRemoveGoldPlayerClick);
        addGoldToVendor.onClick.AddListener(OnAddGoldVendorClick);
        removeGoldFromVendor.onClick.AddListener(OnRemoveGoldVendorClick);
    }

    #region Cheats

    private void OnAddGoldPlayerClick()
    {
        PlayerData.Ins.inventory.GiveGold(100);
        playerGold.text = PlayerData.Ins.inventory.gold.ToString();
    }
    private void OnRemoveGoldPlayerClick()
    {
        PlayerData.Ins.inventory.RemoveGold(100);
        playerGold.text = PlayerData.Ins.inventory.gold.ToString();
    }
    private void OnAddGoldVendorClick()
    {
        vendorInventory.GiveGold(100);
    }
    private void OnRemoveGoldVendorClick()
    {
        vendorInventory.RemoveGold(100);
    }

    #endregion

}
