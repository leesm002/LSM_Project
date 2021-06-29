using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    [SerializeField]
    int _level;
    [SerializeField]
    float _hp;
    [SerializeField]
    float _maxHp;
    [SerializeField]
    float _attack;
    [SerializeField]
    float _defense;
    [SerializeField]
    float _moveSpeed;

    public int Level { get { return _level; } set { _level = value; } }
    public float Hp { get { return _hp; } set { _hp = value; } }
    public float MaxHp { get { return _maxHp; } set { _maxHp = value; } }
    public float Attack { get { return _attack; } set { _attack = value; } }
    public float Defense { get { return _defense; } set { _defense = value; } }
    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }


    private void Start()
    {
        Init();
    }

    protected void Init()
    {
        _level = 1;
        _hp = 100;
        _maxHp = 100;
        _attack = 10;
        _defense = 5;
        _moveSpeed = 5.0f;
    }
}
