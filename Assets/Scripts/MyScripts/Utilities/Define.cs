using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum Scene
    {
        Unknown,
        Login,
        Lobby,
        Tutorial,
        Erin
    }

    public enum PlayerState
    {
        Idle,
        Walking,
        Running,
        Jumping
    }
}
