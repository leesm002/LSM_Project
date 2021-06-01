using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Button : UI_Popup
{
    enum Buttons
    {
        BuyButton,
        SellButton,
        ExitButton
    }

    enum Texts
    {
        PointText,
        ScoreText,
    }

    enum GameObjects
    {
        TestObject,
    }

    enum Images
    {
        ItemIcon,
    }

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

		Bind<Button>(typeof(Buttons));
		Bind<Text>(typeof(Texts));
		Bind<GameObject>(typeof(GameObjects));
		Bind<Image>(typeof(Images));

		GetButton((int)Buttons.ExitButton).gameObject.BindEvent(OnButtonClicked);

		GameObject go = GetButton((int)Buttons.ExitButton).gameObject;
		BindEvent(go, (PointerEventData data) => { GameObject.Find("ShopperEntryUI").SetActive(false); }, Define.UIEvent.Click);
	}


    public void OnButtonClicked(PointerEventData data)
    {
        Debug.Log("클릭 이벤트 발생!");
    }

}
