using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    Vector3 pose;

    const float cameraPositionY = 1.95f;
    const float cameraPositionZ = -2.0f;

    Quaternion qt = new Quaternion(18.423f, 0, 0, 180);


    private void Update()
    {
        pose = GameObject.Find("Player").transform.position;

        pose.y += cameraPositionY;
        pose.z += cameraPositionZ;

        Camera.main.transform.position = pose;
        Camera.main.transform.rotation = qt;

    }

    

}
