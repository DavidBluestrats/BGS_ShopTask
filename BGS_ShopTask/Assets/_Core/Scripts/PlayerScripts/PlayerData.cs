using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Mainly a class to access all of the player's functions
public class PlayerData : MonoBehaviour
{
    public static PlayerData Ins;
    public Inventory inventory;
    public UI_Inventory inventoryUI;
    public PlayerVisualManager playerVisualManager;

    private void Awake()
    {
        Ins = this;
    }

    void Start()
    {
        inventory.OnItemRemoved += OnItemRemoved;
    }

    private void OnDestroy()
    {
        inventory.OnItemRemoved -= OnItemRemoved;
    }

    private void OnItemRemoved(Apparel _apparelItem)
    {
        //If player is wearing the item to be removed, then equip the player with their default outfit for that apparel slot.

        bool playerIsWearingApparel = playerVisualManager.PlayerIsWearingApparel(_apparelItem);
        if (playerIsWearingApparel)
        {
            int defaultApparelIndex;
            switch (_apparelItem.type)
            {
                case ApparelType.HEAD:
                    defaultApparelIndex = 0;
                    break;
                case ApparelType.TORSO:
                    defaultApparelIndex = 1;
                    break;
                case ApparelType.LEGS:
                    defaultApparelIndex = 2;
                    break;
                case ApparelType.FEET:
                    defaultApparelIndex = 3;
                    break;
                default:
                    defaultApparelIndex = -1;
                    break;
            }
            Apparel defaultApparel = inventory.apparelInventory[defaultApparelIndex];
            playerVisualManager.UpdateApparel(defaultApparel);
        }
    }

    private void OnItemAdded()
    {

    }
}
