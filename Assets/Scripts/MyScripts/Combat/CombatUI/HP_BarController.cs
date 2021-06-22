using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP_BarController : UI_Base
{
    PlayerStat _stat;
    Stat mobStat;
    Slider sli;

    enum GameObjects
    {
        HPBar
    }
    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
    }
    private void Start()
    {
        _stat = transform.parent.GetComponent<PlayerStat>();
        mobStat = transform.parent.GetComponent<Stat>();
        sli = transform.GetComponentInChildren<Slider>();
    }

    void Update()
    {
        Transform parent = transform.parent;

        if (parent.name == "SD_Player_Combat")
        {
            
            sli.maxValue = _stat.MaxHp;
            sli.value = _stat.Hp;
        }
        else
        {
            sli.maxValue = mobStat.MaxHp;
            sli.value = mobStat.Hp;
        }

        transform.position = parent.position + new Vector3(0f, 1.4f, 0f);
        transform.rotation = Camera.main.transform.rotation;
    }
}
