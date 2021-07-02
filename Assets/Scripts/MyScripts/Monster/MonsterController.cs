using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    private const float maxLength = 6.0f, minLength = 1.0f;

    private GameObject player;
    private Vector3 ResetPos;
    private Stat stat;
    private float previousHp;
    private Define.MonsterState state = Define.MonsterState.Idle;
    private Define.MonsterState preState = Define.MonsterState.Unknown;

    private float magLength;
    private Vector3 normRot;

    //애니메이션 관련
    private Animator anim;
    private AnimatorStateInfo currentState;
    private AnimatorStateInfo previousState;

    private void Awake()
    {
        //최초 위치 저장
        ResetPos = transform.position;
        stat = gameObject.GetComponent<Stat>();
        player = GameObject.FindGameObjectWithTag("Player");

        previousHp = stat.Hp;

        //** 애니메이션 관련 변수 초기화
        anim = GetComponent<Animator>();
        currentState = anim.GetCurrentAnimatorStateInfo(0);
        previousState = currentState;
    }

    private void LateUpdate()
    {
        digitization();

        switch (state)
        {
            case Define.MonsterState.Idle:
                detectPlayer(magLength);
                break;
            case Define.MonsterState.Walk:
                ChasePlayer(magLength, normRot);
                break;
            case Define.MonsterState.Attack:
                AttackPlayer(magLength);
                break;
            case Define.MonsterState.GetHit:
                OnGetAttack();
                break;
            case Define.MonsterState.Dizzy:
                break;
            case Define.MonsterState.Defend:
                break;
            default:
                break;
        }

    }

    private void digitization()
    {
        magLength = (transform.position - player.transform.position).magnitude;
        normRot = (transform.position - player.transform.position).normalized;
    }

    private void detectPlayer(float Len)
    {
        if (anim.name != "Idle")
            anim.Play("Idle");

        CompareHp();

        if (Len < maxLength && Len > minLength)
            state = Define.MonsterState.Walk;
    }

    private void ChasePlayer(float Len, Vector3 Rot)
    {
        if (anim.name != "Walk")
            anim.Play("Walk");

        CompareHp();

        // 플레이어가 범위를 벗어났을 때
        if (Len >= maxLength)
        {
            
            state = Define.MonsterState.Idle;
            return;
        }

        // 플레이어가 공격범위 내에 있을 때
        if (Len <= minLength)
        {
            state = Define.MonsterState.Attack;
            return;
        }


        if (Len < maxLength && Len > minLength)
        {
            transform.position -= Rot * stat.MoveSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(-Rot), 0.2f);
        }
    }

    private void AttackPlayer(float Len)
    {
        if (anim.name != "Attack01")
            anim.Play("Attack01");

        CompareHp();

        // 플레이어가 범위를 벗어났을 때
        if (Len >= maxLength)
        {
            state = Define.MonsterState.Idle;
            return;
        }


    }

    //공격 이후 범위 내에 없을 때
    private void afterAttackEvent()
    {
        if (magLength < maxLength && magLength > minLength)
        {
            state = Define.MonsterState.Walk;
            return;
        }
    }

    //현재 체력과 이전 체력의 값이 다를 때
    private void CompareHp()
    {
        if (previousHp != stat.Hp)
        {
            previousHp = stat.Hp;
            preState = state;
            state = Define.MonsterState.GetHit;
            return;
        }
    }

    private void OnGetAttack()
    {
        if (anim.name != "GetHit")
            anim.Play("GetHit");

        //GetHitEvent


    }

    private void GetHitEvent()
    {
        state = preState;
    }
}