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

    private UI_Equipment UI_Equ;
    private UI_Inventory UI_Inv;

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
            if (UI_Equ == null)
            {
                UI_Equ = Managers.GetUIManager.ShowPopupUI<UI_Equipment>("EquipUI");
            }
            else 
            { 
                Managers.GetUIManager.ClosePopupUI(UI_Equ);
            }

        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (UI_Inv == null)
                UI_Inv = Managers.GetUIManager.ShowPopupUI<UI_Inventory>("InventoryUI");
            else
                Managers.GetUIManager.ClosePopupUI(UI_Inv);
        }
    }

    private void OnDestroy()
    {
        Managers.GetUIManager.CloseAllPopupUI();
        Managers.GetInputManager.KeyAction -= UI_Key;
    }

    protected void DestroyControl()
    {
        Managers.GetUIManager.CloseAllPopupUI();
        Managers.GetInputManager.KeyAction -= UI_Key;

        return;
    }
}
