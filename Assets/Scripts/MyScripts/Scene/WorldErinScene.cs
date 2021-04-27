using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldErinScene : BaseScene
{
    public override void Clear()
    {
    }

    // Start is called before the first frame update
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.WorldErin;

        GameObject gameObj = Managers.GetResourceManager.Instantiate("MyPrefabs/Player");

        gameObj.name = "Player";

        gameObj.transform.position = new Vector3(71.62f, 25.25597f, 29.4f);
    }

    void Start()
    {
        
    }
}
