using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialToErin : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        SceneManager.UnloadSceneAsync("Tutorial");
        SceneManager.LoadScene("WorldErin");

    }



}
