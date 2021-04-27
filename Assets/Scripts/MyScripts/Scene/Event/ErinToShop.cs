using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ErinToShop : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        SceneManager.UnloadSceneAsync("WorldErin");
        SceneManager.LoadSceneAsync("Shop");
    }
}
