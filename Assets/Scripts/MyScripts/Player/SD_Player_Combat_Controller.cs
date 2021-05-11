using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SD_Player_Combat_Controller : MonoBehaviour
{
    //캐릭터 이동 관련
    float f_speed = 5.0f;
    private CharacterController controller;

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

    private void LateUpdate()
    {
        this.mouseEvent();
    }

    void OnKeyboard()
    {

        //** Walk 상태
        if (Input.GetKey(KeyCode.A))
        {
            controller.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
            controller.Move(Vector3.left * Time.deltaTime * f_speed);
            anim.SetBool("Walk", true);
        }
        else if (!Input.GetKey(KeyCode.D))
        {
            anim.SetTrigger("Idle");
            anim.SetBool("Walk", false);
        }
        if (Input.GetKey(KeyCode.D))
        {
            controller.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
            controller.Move(Vector3.right * Time.deltaTime * f_speed);
            anim.SetBool("Walk", true);
        }
        else if (!Input.GetKey(KeyCode.A))
        {
            anim.SetTrigger("Idle");
            anim.SetBool("Walk", false);
        }

    }

    void mouseEvent()
    {
        //마우스 클릭 이벤트
        if (Input.GetMouseButton((int)Define.MouseButtonDown.MBD_LEFT)) { this.LeftMouseButton(); }
        else { anim.SetBool("PrickAttack", false); }
        if (Input.GetMouseButton((int)Define.MouseButtonDown.MBD_RIGHT)) { this.RightMouseButton(); }
        else { anim.SetBool("ContinuousAttack", false); }
        
    }

    void LeftMouseButton()
    {
        anim.SetBool("PrickAttack", true);
    }

    void RightMouseButton()
    {
        anim.SetBool("ContinuousAttack", true);
    }
}