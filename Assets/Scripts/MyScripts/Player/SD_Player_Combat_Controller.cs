using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SD_Player_Combat_Controller : MonoBehaviour
{
    private CharacterController controller;

    float f_speed = 5.0f;

    const float f_gravity = 9.8f;


    private Animator anim;
    private AnimatorStateInfo currentState;
}