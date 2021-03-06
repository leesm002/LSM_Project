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
        WorldErin,
        Shop,
        Combat

    }

    public enum PlayerState
    {
        Idle,
        Walking,
        Running,
        Jumping,
        Posing
    }
    public enum MonsterState
    {
        Idle,
        Walk,
        Attack,
        GetHit,
        Dizzy,
        Defend,
        Die,
        Unknown
    }

    public enum PlayerCombatState
    {
        Idle,
        Walking,
        Running,
        PrickAttack,
        ContinuousAttack,
        Hitosasi,
        Dizzy,
        Slide,
        Die,
        Unknown

    }

    public enum MouseButtonDown
    {
        MBD_LEFT = 0,
        MBD_RIGHT,
    }

    public enum UIEvent
    {
        Click,
        Drag,
    }
    
}
