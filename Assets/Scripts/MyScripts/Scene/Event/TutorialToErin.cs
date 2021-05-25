using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialToErin : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Managers.GetMySceneManager.LoadScene(Define.Scene.WorldErin);
        //Managers.GetMySceneManager.UnLoadScene(Define.Scene.Tutorial);

    }



}
