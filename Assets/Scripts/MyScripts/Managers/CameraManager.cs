using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    Vector3 pose;

    Vector3 PreviousMousePos;
    Vector3 CurrentMousePos;

    const float cameraPositionY = 1.95f;
    const float cameraPositionZ = -2.0f;
    const float CAMERA_MOVEMENT_SPEED = 0.4f;

    Quaternion qt = new Quaternion(18.423f, 0, 0, 180);
    

    private void Update()
    {
        pose = GameObject.Find("Player").transform.position;

        pose.y += cameraPositionY;
        pose.z += cameraPositionZ;

        Camera.main.transform.position = pose;
        Camera.main.transform.rotation = qt;

        if (Input.GetKeyDown(KeyCode.Mouse1))       // 마우스 우클릭 하는 순간
        {
            PreviousMousePos = Input.mousePosition;
            Debug.Log($"{PreviousMousePos}");
        }
        if (Input.GetMouseButton(1))                // 마우스 우클릭 누르고있는 동안
        {
            CurrentMousePos = Input.mousePosition;

            if (PreviousMousePos.x != CurrentMousePos.x)
            {
                if (PreviousMousePos.x < CurrentMousePos.x)
                    transform.Rotate((PreviousMousePos.y - CurrentMousePos.y) * CAMERA_MOVEMENT_SPEED, (CurrentMousePos.x - PreviousMousePos.x) * CAMERA_MOVEMENT_SPEED, transform.rotation.z);
                    //qt.y += ( PreviousMousePos.x - CurrentMousePos.x ) * 0.01f;

                if (PreviousMousePos.x > CurrentMousePos.x)
                    transform.Rotate((PreviousMousePos.y - CurrentMousePos.y) * CAMERA_MOVEMENT_SPEED, (CurrentMousePos.x - PreviousMousePos.x) * CAMERA_MOVEMENT_SPEED, transform.rotation.z);
                //qt.y -= (CurrentMousePos.x - PreviousMousePos.x) * 0.01f;
            }
        }
    }

    


}
