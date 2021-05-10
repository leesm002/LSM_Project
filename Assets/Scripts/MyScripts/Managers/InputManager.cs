using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager
{
    public Action KeyAction = null;


    public void OnUpdate()
    {
        /*
        if (EventSystem.current.IsPointerOverGameObject())      //버튼 눌렀을 때 마우스 클릭으로 인한 캐릭터 이동을 방지하는 코드
	        return;
        */

        if (KeyAction != null)
        {
            KeyAction.Invoke();
        }
    }
}
