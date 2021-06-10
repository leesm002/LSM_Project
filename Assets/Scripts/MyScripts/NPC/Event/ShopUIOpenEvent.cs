using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUIOpenEvent : MonoBehaviour
{
    UI_ShopperBuyAndSell uI_ShopperBuyAndSell;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collider Trigger");
        uI_ShopperBuyAndSell = Managers.GetUIManager.ShowPopupUI<UI_ShopperBuyAndSell>("UI_ShopperBuyAndSell");
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Trigger Exit");
        Managers.GetUIManager.ClosePopupUI(uI_ShopperBuyAndSell);
    }
}
