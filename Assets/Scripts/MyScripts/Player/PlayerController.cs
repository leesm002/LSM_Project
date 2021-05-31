using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Animator))]
public class PlayerController : MonoBehaviour
{

    Define.PlayerState PlayerStateType = Define.PlayerState.Idle;
    Define.PlayerState dump_PlayerStateType = Define.PlayerState.Idle;

    float f_speed = 5.0f;
    float f_runSpeed = 7.0f;
    float f_jumpUpSpeed = 2.5f;     //올라갈때 속도

    float f_yPosition;              //점프 전 높이
    bool isJumpUp = false;
    bool isRun = false;

    const float f_gravity = 9.8f;

    //** 캐릭터 이동 관련 
    private CharacterController controller;
    private Quaternion rotCam;

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
        if (isJumpUp == false && controller.isGrounded)
        {
            f_yPosition = controller.transform.position.y;

            anim.SetInteger("Condition", 3);

            isJumpUp = true;
        }
    }

    void jumpingUp()
    {
        if (isJumpUp)
            controller.Move(Vector3.up * Time.deltaTime * f_jumpUpSpeed);
        else
            controller.Move(Vector3.down * Time.deltaTime * f_gravity);

        if (f_yPosition + 2.0f < (controller.transform.position.y) && isJumpUp)
            isJumpUp = false;
        

    }

    private void Start()
    {
        if (controller == null)
        {
            controller = GetComponent<CharacterController>();
        }
        //** 애니메이션 관련 변수 초기화
        anim = GetComponent<Animator>();
        currentState = anim.GetCurrentAnimatorStateInfo(0);
        previousState = currentState;

        Managers.GetInputManager.KeyAction -= OnKeyboard;
        Managers.GetInputManager.KeyAction += OnKeyboard;

    }

    private void Update()
    {


        if (PlayerStateType != dump_PlayerStateType)
        {

            switch (PlayerStateType)
            {
                case Define.PlayerState.Idle:
                    UpdateIdle();
                    break;
                case Define.PlayerState.Walking:
                    UpdateWalking();
                    break;
                case Define.PlayerState.Running:
                    UpdateRunning();
                    break;
                case Define.PlayerState.Jumping:
                    UpdateJumping();
                    break;
                default:
                    UpdateIdle();
                    break;
            }

            dump_PlayerStateType = PlayerStateType;
        }

        //땅에 발을 딛고 있지 않을 때 중력 작용
        jumpingUp();


    }

    void OnKeyboard()
    {
        //기본 유휴상태
        PlayerStateType = Define.PlayerState.Idle;

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
        if (Input.GetKey(KeyCode.W) && !isRun )
        {
            controller.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(rotCam * Vector3.forward), 0.2f);
            controller.Move((rotCam * Vector3.forward) * Time.deltaTime * f_speed);
            if (!currentState.IsName("Walk"))
                PlayerStateType = Define.PlayerState.Walking;
        }
        if (Input.GetKey(KeyCode.S) && !isRun )
        {
            controller.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(rotCam * Vector3.back), 0.2f);
            controller.Move((rotCam * Vector3.back) * Time.deltaTime * f_speed);
            if (!currentState.IsName("Walk"))
                PlayerStateType = Define.PlayerState.Walking;
        }
        if (Input.GetKey(KeyCode.A) && !isRun )
        {
            controller.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(rotCam * Vector3.left), 0.2f);
            controller.Move((rotCam * Vector3.left) * Time.deltaTime * f_speed);
            if (!currentState.IsName("Walk"))
                PlayerStateType = Define.PlayerState.Walking;
        }
        if (Input.GetKey(KeyCode.D) && !isRun )
        {
            controller.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(rotCam * Vector3.right), 0.2f);
            controller.Move((rotCam * Vector3.right) * Time.deltaTime * f_speed);
            if (!currentState.IsName("Walk"))
                PlayerStateType = Define.PlayerState.Walking;
        }
        //** Run 상태
        if (Input.GetKey(KeyCode.W) && isRun )
        {
            controller.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(rotCam * Vector3.forward), 0.2f);
            controller.Move((rotCam * Vector3.forward) * Time.deltaTime * f_runSpeed);
            if (!currentState.IsName("Run"))
                PlayerStateType = Define.PlayerState.Running;
        }
        if (Input.GetKey(KeyCode.S) && isRun )
        {
            controller.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(rotCam * Vector3.back), 0.2f);
            controller.Move((rotCam * Vector3.back) * Time.deltaTime * f_runSpeed);
            if (!currentState.IsName("Run"))
                PlayerStateType = Define.PlayerState.Running;
        }
        if (Input.GetKey(KeyCode.A) && isRun)
        {
            controller.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(rotCam * Vector3.left), 0.2f);
            controller.Move((rotCam * Vector3.left) * Time.deltaTime * f_runSpeed);
            if (!currentState.IsName("Run"))
                PlayerStateType = Define.PlayerState.Running;
        }
        if (Input.GetKey(KeyCode.D) && isRun)
        {
            controller.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(rotCam * Vector3.right), 0.2f);
            controller.Move((rotCam * Vector3.right) * Time.deltaTime * f_runSpeed);
            if (!currentState.IsName("Run"))
                PlayerStateType = Define.PlayerState.Running;
        }

        //** Jump 상태
        if (Input.GetKey(KeyCode.Space))
        {
            if (!currentState.IsName("Jump"))
                PlayerStateType = Define.PlayerState.Jumping;
        }


    }

    private void OnDestroy()
    {
        if (Managers.GetInstance != null)
            Managers.GetInputManager.KeyAction -= OnKeyboard;
    }

}
