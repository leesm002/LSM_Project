using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerContol : MonoBehaviour
{
    protected Define.PlayerState PlayerStateType = Define.PlayerState.Idle;
    protected Define.PlayerState dump_PlayerStateType = Define.PlayerState.Idle;

    protected float f_speed = 5.0f;
    protected float f_runSpeed = 7.0f;

    protected bool isRun = false;

    private bool isOpenEquipUI = false;

    protected const float f_gravity = 9.8f;

    //** 캐릭터 이동 관련 
    protected CharacterController controller;
    protected Quaternion rotCam;

    //** 애니메이션 관련
    protected Animator anim;
    protected AnimatorStateInfo currentState;
    protected AnimatorStateInfo previousState;


    protected void Init()
    {
        Managers.GetInputManager.KeyAction -= UI_Key;
        Managers.GetInputManager.KeyAction += UI_Key;
    }

    private void UI_Key()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isOpenEquipUI == false)
            {
                Managers.GetUIManager.ShowPopupUI<UI_Equipment>("EquipUI");
                isOpenEquipUI = true;
            }
            else
            {
                Managers.GetUIManager.CloseAllPopupUI();
                isOpenEquipUI = false;
            }

        }
    }
}
