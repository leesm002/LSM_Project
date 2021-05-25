﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SD_Player_Combat_Controller : MonoBehaviour
{
    Define.PlayerCombatState PlayerStateType = Define.PlayerCombatState.Idle;
    Define.PlayerCombatState dump_PlayerStateType = Define.PlayerCombatState.Idle;

    //캐릭터 이동 관련
    float f_speed = 5.0f;
    float f_runSpeed = 7.0f;
    private CharacterController controller;

    bool isRun = false;
    const float f_gravity = 9.8f;


    private Quaternion rotCam;

    //애니메이션 관련
    private Animator anim;
    private AnimatorStateInfo currentState;
    private AnimatorStateInfo previousState;

    private void Start()
    {
        if (controller == null)
            controller = GetComponent<CharacterController>();

        //** 애니메이션 관련 변수 초기화
        anim = GetComponent<Animator>();
        currentState = anim.GetCurrentAnimatorStateInfo(0);
        previousState = currentState;

        Managers.GetInputManager.KeyAction -= OnKeyboard;
        Managers.GetInputManager.KeyAction += OnKeyboard;
    }


    void UpdateIdle() { anim.SetInteger("Condition", 0); }
    void UpdateWalking() { anim.SetInteger("Condition", 1); }
    void UpdateRunning() { anim.SetInteger("Condition", 2); }
    void UpdatePrickAttack() { anim.SetInteger("Condition", 3); }
    void UpdateContinuousAttack() { anim.SetInteger("Condition", 4); }
    void UpdateHitosasi() { anim.SetInteger("Condition", 5); }
    void UpdateDizzy() { anim.SetInteger("Condition", 6); }
    void UpdateSlide() { anim.SetInteger("Condition", 7); }


    void groundCharacter()
    {
        controller.Move(Vector3.down * Time.deltaTime * f_gravity);

    }

    private void Update()
    {
        if (PlayerStateType != dump_PlayerStateType)
        {
            //anim.SetInteger("Condition", (int)PlayerStateType);
            switch (PlayerStateType)
            {
                case Define.PlayerCombatState.Idle:
                    UpdateIdle();
                    break;
                case Define.PlayerCombatState.Walking:
                    UpdateWalking();
                    break;
                case Define.PlayerCombatState.Running:
                    UpdateRunning();
                    break;
                case Define.PlayerCombatState.PrickAttack:
                    UpdatePrickAttack();
                    break;
                case Define.PlayerCombatState.ContinuousAttack:
                    UpdateContinuousAttack();
                    break;
                case Define.PlayerCombatState.Hitosasi:
                    UpdateHitosasi();
                    break;
                case Define.PlayerCombatState.Dizzy:
                    UpdateDizzy();
                    break;
                case Define.PlayerCombatState.Slide:
                    UpdateSlide();
                    break;
                default:
                    UpdateIdle();
                    break;
            }

            dump_PlayerStateType = PlayerStateType;
        }

        //땅에 발을 딛고 있지 않을 때 중력 작용
        groundCharacter();
    }

    private void LateUpdate()
    {
        this.mouseEvent();
    }

    void OnKeyboard()
    {

        //기본 유휴상태
        PlayerStateType = Define.PlayerCombatState.Idle;

        //뛰는지 체크
        if ((Input.GetKey(KeyCode.LeftShift)))
            isRun = true;
        else
            isRun = false;

        //메인 카메라가 바라보는 방향값을 항상 받아옴
        rotCam = Camera.main.transform.localRotation;
        rotCam.z -= Camera.main.transform.localRotation.z;  // freeze z
        rotCam.x -= Camera.main.transform.localRotation.x;  // freeze x

        //** Walk 상태
        if (Input.GetKey(KeyCode.W) && !isRun)
        {
            controller.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(rotCam * Vector3.forward), 0.2f);
            controller.Move((rotCam * Vector3.forward) * Time.deltaTime * f_speed);
            if (!currentState.IsName("Walk"))
                PlayerStateType = Define.PlayerCombatState.Walking;
        }
        if (Input.GetKey(KeyCode.S) && !isRun)
        {
            controller.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(rotCam * Vector3.back), 0.2f);
            controller.Move((rotCam * Vector3.back) * Time.deltaTime * f_speed);
            if (!currentState.IsName("Walk"))
                PlayerStateType = Define.PlayerCombatState.Walking;
        }
        if (Input.GetKey(KeyCode.A) && !isRun)
        {
            controller.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(rotCam * Vector3.left), 0.2f);
            controller.Move((rotCam * Vector3.left) * Time.deltaTime * f_speed);
            if (!currentState.IsName("Walk"))
                PlayerStateType = Define.PlayerCombatState.Walking;
        }
        if (Input.GetKey(KeyCode.D) && !isRun)
        {
            controller.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(rotCam * Vector3.right), 0.2f);
            controller.Move((rotCam * Vector3.right) * Time.deltaTime * f_speed);
            if (!currentState.IsName("Walk"))
                PlayerStateType = Define.PlayerCombatState.Walking;
        }
        //** Run 상태
        if (Input.GetKey(KeyCode.W) && isRun)
        {
            controller.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(rotCam * Vector3.forward), 0.2f);
            controller.Move((rotCam * Vector3.forward) * Time.deltaTime * f_runSpeed);
            if (!currentState.IsName("Run"))
                PlayerStateType = Define.PlayerCombatState.Running;
        }
        if (Input.GetKey(KeyCode.S) && isRun)
        {
            controller.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(rotCam * Vector3.back), 0.2f);
            controller.Move((rotCam * Vector3.back) * Time.deltaTime * f_runSpeed);
            if (!currentState.IsName("Run"))
                PlayerStateType = Define.PlayerCombatState.Running;
        }
        if (Input.GetKey(KeyCode.A) && isRun)
        {
            controller.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(rotCam * Vector3.left), 0.2f);
            controller.Move((rotCam * Vector3.left) * Time.deltaTime * f_runSpeed);
            if (!currentState.IsName("Run"))
                PlayerStateType = Define.PlayerCombatState.Running;
        }
        if (Input.GetKey(KeyCode.D) && isRun)
        {
            controller.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(rotCam * Vector3.right), 0.2f);
            controller.Move((rotCam * Vector3.right) * Time.deltaTime * f_runSpeed);
            if (!currentState.IsName("Run"))
                PlayerStateType = Define.PlayerCombatState.Running;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (!currentState.IsName("Slide"))
                PlayerStateType = Define.PlayerCombatState.Slide;
        }

    }

    void mouseEvent()
    {
        //마우스 클릭 이벤트
        if (Input.GetMouseButton((int)Define.MouseButtonDown.MBD_LEFT)) { this.LeftMouseButton(); }
        if (Input.GetMouseButton((int)Define.MouseButtonDown.MBD_RIGHT)) { this.RightMouseButton(); }
        
    }

    void LeftMouseButton()
    {
        if (!currentState.IsName("PrickAttack"))
            PlayerStateType = Define.PlayerCombatState.PrickAttack;
    }

    void RightMouseButton()
    {
        if (!currentState.IsName("ContinuousAttack"))
            PlayerStateType = Define.PlayerCombatState.ContinuousAttack;
    }

    private void OnDestroy()
    {
        Managers.GetInputManager.KeyAction -= OnKeyboard;
    }

}