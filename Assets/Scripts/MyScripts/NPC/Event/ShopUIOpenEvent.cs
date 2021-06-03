using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUIOpenEvent : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collider Trigger");
        Managers.GetUIManager.ShowPopupUI<ShopperEntryUI>("ShopperEntryUI");
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Trigger Exit");
        Managers.GetUIManager.CloseAllPopupUI();
    }
}
