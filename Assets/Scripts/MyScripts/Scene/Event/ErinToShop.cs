using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ErinToShop : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Managers.GetUIManager.CloseAllPopupUI();
        Managers.GetMySceneManager.LoadScene(Define.Scene.Shop);
        //Managers.GetMySceneManager.UnLoadScene(Define.Scene.WorldErin);
    }
}
