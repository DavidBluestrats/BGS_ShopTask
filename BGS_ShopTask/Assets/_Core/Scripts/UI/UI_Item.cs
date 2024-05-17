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
    public Image background;
    public Color unequippedColor;
    public Color equippedColor;

    [Header("Class Variables")]
    public StoreTypeInteraction itemProperty;
    public Apparel apparelItem;
    private VendorData vendorData;

    [Header("Events")]
    public Action<Apparel, UI_Item> OnItemEquipped;

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

    //Simply send the apparel data to visual manager and update character.
    private void EquipItem()
    {
        PlayerData.Ins.playerVisualManager.UpdateApparel(apparelItem);
        OnItemEquipped?.Invoke(apparelItem, this);
    }

    private void PurchaseItem()
    {
        //Check if gold is enough and call inventory management methods.

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
        //Check if gold is enough and call inventory management methods.

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

    //Switch color depending on equipped status.
    public void SetEquipped(bool _isEquipped)
    {
        background.color = _isEquipped ? equippedColor : unequippedColor;
    }
}
