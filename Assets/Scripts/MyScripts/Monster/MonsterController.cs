using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    private const float maxLength = 6.0f, minLength = 1.0f;

    private GameObject player;
    private Vector3 ResetPos;
    private Stat mobStat;
    private PlayerStat _stat;
    private float previousHp;
    private bool isDizzy = false;
    
    private Define.MonsterState state = Define.MonsterState.Idle;
    private Define.MonsterState preState = Define.MonsterState.Idle;

    private float magLength;
    private Vector3 normRot;

    //애니메이션 관련
    private Animator anim;
    private AnimatorStateInfo currentState;
    private AnimatorStateInfo previousState;

    private void Start()
    {
        //최초 위치 저장
        ResetPos = transform.position;
        mobStat = gameObject.GetComponent<Stat>();
        _stat = gameObject.GetComponent<PlayerStat>();
        player = GameObject.FindGameObjectWithTag("Player");

        previousHp = mobStat.Hp;
        isDizzy = false;

        //** 애니메이션 관련 변수 초기화
        anim = GetComponent<Animator>();
        currentState = anim.GetCurrentAnimatorStateInfo(0);
        previousState = currentState;
    }

    private void Update()
    {
        digitization();
        GetDizzy();

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
                OnDizzy();
                break;
            case Define.MonsterState.Defend:
                break;
            case Define.MonsterState.Die:
                break;
            default:
                detectPlayer(magLength);
                break;
        }
        

    }

    private void digitization()
    {
        magLength = (transform.position - player.transform.position).magnitude;
        normRot = (transform.position - player.transform.position).normalized;
    }

    #region 이동관련
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
            transform.position -= Rot * mobStat.MoveSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(-Rot), 0.2f);
        }
    }

    #endregion

    #region 전투(공격&체력)관련
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

    //공격 했을 때 이벤트
    private void AttackEvent()
    {
        GameObject[] obj = GameObject.FindGameObjectsWithTag("MobWeapon");

        foreach (var item in obj)
        {
            if (item.activeInHierarchy)
            {

                Collider[] collider = Physics.OverlapBox(item.transform.position, item.transform.localScale, new Quaternion(0.3f, 0.3f, 0.3f, 0.3f));
                foreach (Collider col in collider)
                {
                    if (col.tag == "Player")
                    {
                        PlayerStat _stat = col.GetComponentInChildren<PlayerStat>();
                        //최소 데미지 1
                        _stat.Hp -= Mathf.Max(1, mobStat.Attack - _stat.Defense);
                    }
                }

            }
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

        if (previousHp != mobStat.Hp)
        {
            previousHp = mobStat.Hp;
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
    #endregion

    #region 전투(상태이상)관련
    private void GetDizzy()
    {
        if (isDizzy)
        {
            preState = state;
            state = Define.MonsterState.Dizzy;
        }
    }

    private void OnDizzy()
    {
        if (anim.name != "Dizzy")
            anim.Play("DIzzy");

        //GetDizzyEvent

    }
    private void GetDizzyEvent()
    {
        Debug.Log("Dizzy");
    }
    #endregion
}