using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float f_speed = 5.0f;
    [SerializeField]
    float f_runSpeed = 7.0f;
    [SerializeField]
    float f_jumpSpeed = 5.0f;

    private CharacterController controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (controller.isGrounded)
        {
            Managers.GetInputManager.KeyAction -= OnKeyboard;
            Managers.GetInputManager.KeyAction += OnKeyboard;
        }
    }


    void OnKeyboard()
    {

        if (Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
            controller.Move(Vector3.forward * Time.deltaTime * f_speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);
            controller.Move(Vector3.back * Time.deltaTime * f_speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
            controller.Move(Vector3.left * Time.deltaTime * f_speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
            controller.Move(Vector3.right * Time.deltaTime * f_speed);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            controller.Move(Vector3.up * Time.deltaTime * f_jumpSpeed);
        }

    }
}
