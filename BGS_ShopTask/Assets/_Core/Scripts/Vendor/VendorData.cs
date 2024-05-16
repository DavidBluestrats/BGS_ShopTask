using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendorData : MonoBehaviour
{
    public UI_Vendor vendorUI;
    public Inventory vendorInventory;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) vendorUI.InvokeView();
        else if (Input.GetKeyDown(KeyCode.J)) vendorUI.CloseView();
    }
}
