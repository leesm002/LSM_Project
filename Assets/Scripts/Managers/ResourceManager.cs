using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject gameObj = Load<GameObject>("$Prefabs/{path}");

        if (gameObj == null)
        {
            Debug.Log("$There is no Prefabs in {path}");
            return null;
        }

        
        return Object.Instantiate<GameObject>(gameObj, parent);
    }

    public void Destory(GameObject gameObj)
    {
        if (gameObj == null)
            return;

        Object.Destroy(gameObj);
    }

}










































//public T Load<T>(string path) where T : Object
//{
//    return Resources.Load<T>(path);
//}

//public GameObject Instantiate(string path, Transform parent = null)
//{
//    GameObject prefab = Load<GameObject>("$Prefabs /{ path }");
//    if (prefab == null)
//    {
//        Debug.Log($"Failed to load prefab : {path}");
//        return null;
//    }

//    return Object.Instantiate(prefab, parent);
//}

//public void Destroy(GameObject gameObj)
//{
//    if (gameObj == null)
//        return;

//    Object.Destroy(gameObj);
//}