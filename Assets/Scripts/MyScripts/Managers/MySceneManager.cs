using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager
{

    public void UnLoadScene(Define.Scene type) { SceneManager.UnloadSceneAsync(GetSceneName(type)); }

    public void LoadScene(Define.Scene type) { SceneManager.LoadSceneAsync(GetSceneName(type)); }

    public void ActiveScene(Define.Scene type) { SceneManager.SetActiveScene(SceneManager.GetSceneByName(GetSceneName(type))); }

    string GetSceneName(Define.Scene type)
    {
        string name = System.Enum.GetName(typeof(Define.Scene), type);
        return name;
    }

    public BaseScene CurrentScene { get { return GameObject.FindObjectOfType<BaseScene>(); } }


}
