using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Vendor : MonoBehaviour
{
    [Header("Vendor's Containers, Scrollers & UI Elements")]
    public GameObject vendorItemList;
    public ScrollRect vendorScroll;
    public TMP_Text vendorDialogue;
    public TMP_Text vendorGold;

    [Header("Player's Containers, Scrollers & UI Elements")]
    public GameObject playerItemList;
    public ScrollRect playerScroll;
    public TMP_Text playerDialogue;
    public TMP_Text playerGold;


    [Header("Prefabs")]
    public UI_Item storeItemPrefab;

    [Header("References")]
    public VendorData vendorData;

    void Start()
    {
        vendorData.vendorInventory.OnItemGiven += OnVendorGetsItem;
        vendorData.vendorInventory.OnItemRemoved += OnVendorSellsItem;
    }

    private void OnDestroy()
    {
        vendorData.vendorInventory.OnItemGiven -= OnVendorGetsItem;
        vendorData.vendorInventory.OnItemRemoved -= OnVendorSellsItem;
    }

    public void InvokeView()
    {
        gameObject.SetActive(true);
        vendorGold.text = vendorData.vendorInventory.gold.ToString();
        playerGold.text = PlayerData.Ins.inventory.gold.ToString();
        vendorDialogue.text = "Greetings, what can I do for you?";
        BuildVendorItemList();
        BuildPlayerItemList();
    }

    public void CloseView()
    {
        gameObject.SetActive(false);
    }

    private void BuildVendorItemList()
    {
        //Clear previous list.
        foreach (Transform child in vendorItemList.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in vendorData.vendorInventory.apparelInventory)
        {
            UI_Item storeItem = Instantiate(storeItemPrefab, vendorItemList.transform);
            storeItem.Setup(item.Value, StoreTypeInteraction.BUY, vendorData);
        }

        vendorScroll.verticalNormalizedPosition = 1f;
    }

    private void BuildPlayerItemList()
    {
        //Clear previous list.
        foreach (Transform child in playerItemList.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in PlayerData.Ins.inventory.apparelInventory)
        {
            if (item.Value.id == 0 || item.Value.id == 1 || item.Value.id == 2 || item.Value.id == 3) continue;
            else
            {
                UI_Item storeItem = Instantiate(storeItemPrefab, playerItemList.transform);
                storeItem.Setup(item.Value, StoreTypeInteraction.SELL, vendorData);
            }
        }

        playerScroll.verticalNormalizedPosition = 1f;

    }

    private void OnVendorGetsItem(Apparel _apparel)
    {
        //Spawn item on Vendor UI
        UI_Item storeItem = Instantiate(storeItemPrefab, vendorItemList.transform);
        storeItem.Setup(_apparel, StoreTypeInteraction.BUY, vendorData);

        //Update gold data
        vendorData.vendorInventory.RemoveGold(_apparel.price);
        PlayerData.Ins.inventory.GiveGold(_apparel.price);
        vendorGold.text = vendorData.vendorInventory.gold.ToString();
        playerGold.text = PlayerData.Ins.inventory.gold.ToString();
        VendorBuyItemDialogue();
    }

    private void OnVendorSellsItem(Apparel _apparel)
    {
        //Spawn item on Player UI
        UI_Item storeItem = Instantiate(storeItemPrefab, playerItemList.transform);
        storeItem.Setup(_apparel, StoreTypeInteraction.SELL, vendorData);

        //Update gold data
        vendorData.vendorInventory.GiveGold(_apparel.price);
        PlayerData.Ins.inventory.RemoveGold(_apparel.price);
        vendorGold.text = vendorData.vendorInventory.gold.ToString();
        playerGold.text = PlayerData.Ins.inventory.gold.ToString();
        VendorSellItemDialogue();
    }

    public void RejectItemDialogue()
    {
        vendorDialogue.text = GetRandomDIalogueFromArray(itemRejectionQueues);
    }

    public void RejectPurchaseDialogue()
    {
        vendorDialogue.text = GetRandomDIalogueFromArray(purchaseRejectionQueues);
    }

    public void VendorSellItemDialogue()
    {
        vendorDialogue.text = GetRandomDIalogueFromArray(purchaseSellQueues);
    }

    public void VendorBuyItemDialogue()
    {
        vendorDialogue.text = GetRandomDIalogueFromArray(itemBuyQueues);
    }

    private List<string> purchaseRejectionQueues = new List<string>{
        "Sorry mate, not enough gold.",
        "Are you mad? Get some gold and come back.",
        "My stuff is worth more than what you have...",
        "You jokin'? Get some gold..."
    };
    private List<string> itemRejectionQueues = new List<string>{
        "I don't have the money for that...",
        "Too expensive.",
        "Sorry, don't got enough gold.",
        "You kiddin'? That's too expensive!"
    };

    private List<string> purchaseSellQueues = new List<string>{
        "Thank you for your patronage.",
        "That'll look great on you.",
        "Thanks mate.",
        "Good choice!"
    };
    private List<string> itemBuyQueues = new List<string>{
        "I'll make good use of this...",
        "I love this!",
        "This'll make a great addition to my collection.",
        "It didn't even look that good on you, really."
    };

    private string GetRandomDIalogueFromArray(List<string> _dialogues)
    {
        System.Random rand = new System.Random();
        var randomIndex = rand.Next(_dialogues.Count);
        return _dialogues[randomIndex];
    }

}