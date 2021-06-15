using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_BarController : UI_Base
{
    enum GameObjects
    {
        HPBar
    }
    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Transform parent = transform.parent;
        transform.position = parent.position + new Vector3(0f, 1.4f, 0f);
        transform.rotation = Camera.main.transform.rotation;
    }
}
