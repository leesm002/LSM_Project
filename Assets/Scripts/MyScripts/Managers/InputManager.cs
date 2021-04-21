﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    public Action KeyAction = null;

    public void OnUpdate()
    {
        if (KeyAction != null)
        {
            KeyAction.Invoke();
        }
    }
}