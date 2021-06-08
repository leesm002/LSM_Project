using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Inventory : UI_Popup
{
    enum Buttons
    {
        CloseInvenButton,
    }

    enum Images
    {
        ItemImage,
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
        Bind<Image>(typeof(Images));

        GetButton((int)Buttons.CloseInvenButton).gameObject.BindEvent(OnButtonClicked);
        GetImage((int)Images.ItemImage).gameObject.BindEvent(OnImageClicked);

        GameObject ButtonObj = GetButton((int)Buttons.CloseInvenButton).gameObject;
        GameObject ItemImageObj = GetImage((int)Images.ItemImage).gameObject;


        BindEvent(ButtonObj, (PointerEventData data) => { Managers.GetUIManager.ClosePopupUI(this); }, Define.UIEvent.Click);
        BindEvent(ItemImageObj, (PointerEventData data) => { ItemImageObj.transform.position = Input.mousePosition; }, Define.UIEvent.Click);
    }

    public void OnButtonClicked(PointerEventData data)
    {
        Debug.Log("클릭 이벤트 발생");
    }

    public void OnImageClicked(PointerEventData data)
    {
        Debug.Log("클릭 이벤트 발생!");
    }
}
