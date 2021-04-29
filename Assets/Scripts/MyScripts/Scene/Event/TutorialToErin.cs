﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialToErin : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Managers.GetMySceneManager.UpdateSceneNum();
        SceneManager.UnloadSceneAsync("Tutorial");
        Managers.GetMySceneManager.SetSceneNum(Define.Scene.WorldErin);
        SceneManager.LoadScene("WorldErin");

    }



}
