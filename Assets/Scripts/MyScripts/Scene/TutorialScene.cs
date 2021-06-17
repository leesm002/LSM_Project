using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScene : BaseScene
{
    public override void Clear()
    {
        
    }

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Tutorial;

        GameObject gameObj = Managers.GetResourceManager.Instantiate("MyPrefabs/Player");

        gameObj.name = "Player";

        gameObj.transform.position = new Vector3(0, 1, 0);
        
    }

    void Start()
    {
       
    }

    

}
