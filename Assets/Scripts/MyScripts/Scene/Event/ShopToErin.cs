using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopToErin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Managers.GetMySceneManager.UpdateSceneNum();
        SceneManager.UnloadSceneAsync("Shop");
        Managers.GetMySceneManager.SetSceneNum(Define.Scene.WorldErin);
        SceneManager.LoadSceneAsync("WorldErin");
    }
}
