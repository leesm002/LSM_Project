using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialToErin : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Managers.GetMySceneManager.UnLoadScene(Define.Scene.Tutorial);
        Managers.GetMySceneManager.LoadScene(Define.Scene.WorldErin);

    }



}
