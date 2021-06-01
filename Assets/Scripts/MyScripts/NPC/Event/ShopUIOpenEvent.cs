using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUIOpenEvent : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collider Trigger");
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
