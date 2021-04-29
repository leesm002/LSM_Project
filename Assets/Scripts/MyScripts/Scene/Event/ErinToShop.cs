using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ErinToShop : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Managers.GetMySceneManager.UpdateSceneNum();
        SceneManager.UnloadSceneAsync("WorldErin");
        Managers.GetMySceneManager.SetSceneNum(Define.Scene.Shop);
        SceneManager.LoadSceneAsync("Shop");
    }
}
