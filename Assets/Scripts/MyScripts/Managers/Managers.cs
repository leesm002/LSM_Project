using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{

    static Managers s_Instance;
    public static Managers GetInstance { get { Init(); return s_Instance; } }

    InputManager inputManager = new InputManager();
    public static InputManager GetInputManager { get { return GetInstance.inputManager; } }

    ResourceManager resourceManager = new ResourceManager();
    public static ResourceManager GetResourceManager { get { return GetInstance.resourceManager; } }

    MySceneManager mySceneManager = new MySceneManager();
    public static MySceneManager GetMySceneManager { get { return GetInstance.mySceneManager; } }

    UIManager uiManager = new UIManager();
    public static UIManager GetUIManager { get { return GetInstance.uiManager; } }

    DataManager dataManager = new DataManager();
    public static DataManager GetDataManager { get { return GetInstance.dataManager; } }

    void Awake()
    {
        Init();
    }

    void Update()
    {
        inputManager.OnUpdate();
    }

    static void Init()
    {
        if (s_Instance == null)
        {
            GameObject gameObj = GameObject.Find("@Managers");
            if (gameObj == null)
            {
                gameObj = new GameObject { name = "@Managers" };
                gameObj.AddComponent<Managers>();
            }
            DontDestroyOnLoad(gameObj);
            s_Instance = gameObj.GetComponent<Managers>();

            // 각 매니저의 Init 함수 실행부
            s_Instance.dataManager.Init();
            //s_Instance.poolManager.Init();
            //s_Instance.soundManager.Init();
            
            
        }
    }
    
}
