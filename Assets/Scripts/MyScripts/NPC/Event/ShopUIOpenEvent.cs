using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUIOpenEvent : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        GameObject UIManager = Managers.GetUIManager.Root;
        Debug.Log("Collider Trigger");

    }
}
