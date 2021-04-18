using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    float f_speed = 5.0f;
    float f_runSpeed = 7.0f;
    float f_jumpUpSpeed = 2.5f;     //올라갈때 속도

    float f_yPosition;              //점프 전 높이
    bool isJumpUp = false;
    const float f_gravity = 9.8f;

    public enum PlayerState
    {
        Idle,
        Walking,
        Running,
        Jumping
    }

    PlayerState e_playerState = PlayerState.Idle;
    PlayerState dump_playerState = PlayerState.Idle;

    //** 캐릭터 이동 관련 
    private CharacterController controller;

    //** 애니메이션 관련
    private Animator anim;
    private AnimatorStateInfo currentState;
    private AnimatorStateInfo previousState;


    void UpdateIdle()
    {
        anim.SetInteger("Condition", 0);
    }
    void UpdateWalking()
    {
        anim.SetInteger("Condition", 1);
    }
    void UpdateRunning()
    {
        anim.SetInteger("Condition", 2);
    }
    void UpdateJumping()
    {
        f_yPosition = controller.transform.position.y;

        anim.SetInteger("Condition", 3);

        isJumpUp = true;
    }

    void jumpingUp()
    {
        if (isJumpUp)
            controller.Move(Vector3.up * Time.deltaTime * f_jumpUpSpeed);
        else
            controller.Move(Vector3.down * Time.deltaTime * f_gravity);

        if (f_yPosition + 2.0 < (controller.transform.position.y) && isJumpUp)
            isJumpUp = false;
        

    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();

        //** 애니메이션 관련 변수 초기화
        anim = GetComponent<Animator>();
        currentState = anim.GetCurrentAnimatorStateInfo(0);
        previousState = currentState;
    }

    private void Update()
    {

        Managers.GetInputManager.KeyAction -= OnKeyboard;
        Managers.GetInputManager.KeyAction += OnKeyboard;

        if (e_playerState != dump_playerState)
        {

            switch (e_playerState)
            {
                case PlayerState.Idle:
                    UpdateIdle();
                    break;
                case PlayerState.Walking:
                    UpdateWalking();
                    break;
                case PlayerState.Running:
                    UpdateRunning();
                    break;
                case PlayerState.Jumping:
                    UpdateJumping();
                    break;
                default:
                    UpdateIdle();
                    break;
            }

            dump_playerState = e_playerState;
        }

        //땅에 발을 딛고 있지 않을 때 중력 작용
        jumpingUp();


    }

    void OnKeyboard()
    {
        e_playerState = PlayerState.Idle;

        //** Walk 상태
        if (Input.GetKey(KeyCode.W) && ! (Input.GetKey(KeyCode.LeftShift)) )
        {
            controller.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
            controller.Move(Vector3.forward * Time.deltaTime * f_speed);
            if (!currentState.IsName("Walk"))
                e_playerState = PlayerState.Walking;
        }
        if (Input.GetKey(KeyCode.S) && !(Input.GetKey(KeyCode.LeftShift)))
        {
            controller.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);
            controller.Move(Vector3.back * Time.deltaTime * f_speed);
            if (!currentState.IsName("Walk"))
                e_playerState = PlayerState.Walking;
        }
        if (Input.GetKey(KeyCode.A) && !(Input.GetKey(KeyCode.LeftShift)))
        {
            controller.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
            controller.Move(Vector3.left * Time.deltaTime * f_speed);
            if (!currentState.IsName("Walk"))
                e_playerState = PlayerState.Walking;
        }
        if (Input.GetKey(KeyCode.D) && !(Input.GetKey(KeyCode.LeftShift)))
        {
            controller.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
            controller.Move(Vector3.right * Time.deltaTime * f_speed);
            if (!currentState.IsName("Walk"))
                e_playerState = PlayerState.Walking;
        }
        //** Run 상태
        if (Input.GetKey(KeyCode.W) && (Input.GetKey(KeyCode.LeftShift)))
        {
            controller.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
            controller.Move(Vector3.forward * Time.deltaTime * f_runSpeed);
            if (!currentState.IsName("Run"))
                e_playerState = PlayerState.Running;
        }
        if (Input.GetKey(KeyCode.S) && (Input.GetKey(KeyCode.LeftShift)))
        {
            controller.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);
            controller.Move(Vector3.back * Time.deltaTime * f_runSpeed);
            if (!currentState.IsName("Run"))
                e_playerState = PlayerState.Running;
        }
        if (Input.GetKey(KeyCode.A) && (Input.GetKey(KeyCode.LeftShift)))
        {
            controller.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
            controller.Move(Vector3.left * Time.deltaTime * f_runSpeed);
            if (!currentState.IsName("Run"))
                e_playerState = PlayerState.Running;
        }
        if (Input.GetKey(KeyCode.D) && (Input.GetKey(KeyCode.LeftShift)))
        {
            controller.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
            controller.Move(Vector3.right * Time.deltaTime * f_runSpeed);
            if (!currentState.IsName("Run"))
                e_playerState = PlayerState.Running;
        }

        //** Jump 상태
        if (Input.GetKey(KeyCode.Space))
        {
            if (!currentState.IsName("Jump"))
                e_playerState = PlayerState.Jumping;
        }


    }


}
