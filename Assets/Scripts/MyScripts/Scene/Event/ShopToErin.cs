using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopToErin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Managers.GetMySceneManager.UnLoadScene(Define.Scene.Shop);
        Managers.GetMySceneManager.LoadScene(Define.Scene.WorldErin);
    }
}
