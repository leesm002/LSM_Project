using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Popup : UI_Base
{
    public override void Init()
    {
        Managers.GetUIManager.SetCanvas(gameObject, true);
    }

    public virtual void ClosePopupUI()
    {
        Managers.GetUIManager.ClosePopupUI(this);
    }
}
