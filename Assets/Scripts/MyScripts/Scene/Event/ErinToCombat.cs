using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErinToCombat : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Managers.GetUIManager.CloseAllPopupUI();
        Managers.GetMySceneManager.LoadScene(Define.Scene.Combat);
        //Managers.GetMySceneManager.UnLoadScene(Define.Scene.WorldErin);
    }

}
