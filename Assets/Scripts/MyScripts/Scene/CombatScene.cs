using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatScene : BaseScene
{


    public override void Clear()
    {
        Debug.Log("CombatScene Clear!");
    }

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Combat;

        GameObject gameObj = Managers.GetResourceManager.Instantiate("MyPrefabs/SD_Player_Combat");

        gameObj.name = "Player";

        gameObj.transform.position = new Vector3(0, 1, 0);

    }
}
