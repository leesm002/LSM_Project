using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySceneManager
{
    private Define.Scene PreviousSceneNum = Define.Scene.Unknown;
    private Define.Scene SceneNum = Define.Scene.Unknown;

    public void SetSceneNum(Define.Scene str)
    {
        switch (str)
        {
            case Define.Scene.Unknown:
                this.SceneNum = Define.Scene.Unknown;
                break;
            case Define.Scene.Login:
                this.SceneNum = Define.Scene.Login;
                break;
            case Define.Scene.Lobby:
                this.SceneNum = Define.Scene.Lobby;
                break;
            case Define.Scene.Tutorial:
                this.SceneNum = Define.Scene.Tutorial;
                break;
            case Define.Scene.WorldErin:
                this.SceneNum = Define.Scene.WorldErin;
                break;
            case Define.Scene.Shop:
                this.SceneNum = Define.Scene.Shop;
                break;
            default:
                Debug.Log("Define.Scene 타입이 아닙니다!");
                break;
        }
    }

    public void UpdateSceneNum()
    {
        if (this.PreviousSceneNum != this.SceneNum)
            this.PreviousSceneNum = this.SceneNum;
    }


}
