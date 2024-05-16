using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum StoreTypeInteraction { BUY, SELL, EQUIP };
public class UI_Item : MonoBehaviour
{
    [Header("UI Elements")]

    public TMP_Text itemName;
    public TMP_Text priceText;
    public Image itemIcon;
    public Button itemButton;

    [Header("Class Variables")]
    public StoreTypeInteraction itemProperty;
    private Apparel apparelItem;
    private VendorData vendorData;

    [Header("Events")]
    public Action<Apparel> OnItemEquipped;

    void Start()
    {
        itemButton.onClick.AddListener(OnItemClick);
    }

    private void OnDestroy()
    {
        OnItemEquipped = null;
    }

    private void OnDisable()
    {
        OnItemEquipped = null;
    }

    public void Setup(Apparel _apparel, StoreTypeInteraction _itemProperty, VendorData _vendorData = null)
    {
        itemName.text = _apparel.apparelName;
        priceText.text = _apparel.price.ToString();
        itemIcon.sprite = _apparel.icon;
        vendorData = _vendorData;
        itemProperty = _itemProperty;
        apparelItem = _apparel;
    }

    private void OnItemClick()
    {
        /*
         Since this is a small game with few interactions
        we can reuse the same item cell for several purposes.
         */
        if (itemProperty == StoreTypeInteraction.BUY)
        {
            PurchaseItem();
        }
        else if (itemProperty == StoreTypeInteraction.SELL)
        {
            SellItem();
        }
        else if (itemProperty == StoreTypeInteraction.EQUIP)
        {
            EquipItem();
        }
    }

    private void EquipItem()
    {
        PlayerData.Ins.playerVisualManager.UpdateApparel(apparelItem);
        OnItemEquipped?.Invoke(apparelItem);
    }

    private void PurchaseItem()
    {
        if (PlayerData.Ins.inventory.gold >= apparelItem.price)
        {
            PlayerData.Ins.inventory.GiveItem(apparelItem);
            vendorData.vendorInventory.RemoveItem(apparelItem);
            Destroy(gameObject);
        }
        else
        {
            vendorData.vendorUI.RejectPurchaseDialogue();
        }
    }

    private void SellItem()
    {
        if (vendorData.vendorInventory.gold >= apparelItem.price)
        {
            PlayerData.Ins.inventory.RemoveItem(apparelItem);
            vendorData.vendorInventory.GiveItem(apparelItem);
            Destroy(gameObject);
        }
        else
        {
            vendorData.vendorUI.RejectItemDialogue();
        }

    }
}
