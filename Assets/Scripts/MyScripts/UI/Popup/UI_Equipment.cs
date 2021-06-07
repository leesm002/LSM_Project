using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Equipment : UI_Popup
{
    enum Buttons
    {
        CloseEquipButton,
    }

    enum Panels
    {
        EquipPanel,
        RightAccEquipSlot,
        HelmetEquipSlot,
        LeftAccEquipSlot,
        RightHandEquipSlot,
        ClothesEquipSlot,
        LeftHandEquipSlot,
        GlovesEquipSlot,
        ShoesEquipSlot,
        RobeEquipSlot,
    }

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        //Bind<Text>(typeof(Texts));
        //Bind<Image>(typeof(Images));

        GetButton((int)Buttons.CloseEquipButton).gameObject.BindEvent(OnButtonClicked);

        GameObject go = GetButton((int)Buttons.CloseEquipButton).gameObject;
        BindEvent(go, (PointerEventData data) => { Managers.GetUIManager.ClosePopupUI(this); }, Define.UIEvent.Click);
    }


    public void OnButtonClicked(PointerEventData data)
    {
        Debug.Log("클릭 이벤트 발생!");
    }


}
