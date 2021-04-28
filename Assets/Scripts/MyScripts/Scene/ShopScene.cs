using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScene : BaseScene
{
    public override void Clear()
    {

    }

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Shop;

        GameObject gameObj = Managers.GetResourceManager.Instantiate("MyPrefabs/SD_Player");

        gameObj.name = "Player";

        gameObj.transform.position = new Vector3(0, 1, 0);

    }

}
