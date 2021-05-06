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
        if (EventSystem.current.IsPointerOverGameObject())
	        return;

        if (KeyAction != null)
        {
            KeyAction.Invoke();
        }
    }
}
