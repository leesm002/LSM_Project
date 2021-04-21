using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextMapCollisionEvent : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        SceneManager.UnloadSceneAsync("Tutorial");
        SceneManager.LoadSceneAsync("WorldErin");
    }

}
