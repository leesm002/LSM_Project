using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatCamera : CameraManager
{
	float f_x, f_y = 0;

	Vector3 mouseDiff;

	float mouseRotateSpeed = 5.0f;

	void Start()
    {
        focusObj = GameObject.Find("SD_Player_Combat");
		focusAxis = GameObject.Find("PlayerAxis");

		if (focusObj == null)
            setupFocusObject("SD_Player_Combat");
		if (this.focusAxis == null)
			this.setupFocusObject("PlayerAxis");

		//chasePlayer() 함수를 위한 초기화
		dumpPos = focusObj.transform.position;

        Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
		f_x = Input.GetAxis("Mouse X");
		f_y = Input.GetAxis("Mouse Y");

		mouseDiff = new Vector3(f_x * mouseRotateSpeed, f_y * mouseRotateSpeed);
	}

	override protected void mouseEvent()
	{
		//확대&축소
		float delta = Input.GetAxis("Mouse ScrollWheel");
		if (delta != 0.0f)
			this.mouseWheelEvent(delta);

		//마우스 드래그 이벤트
		this.mouseDragEvent(mouseDiff);

		return;
	}

	override protected void mouseDragEvent(Vector3 mousePos)
	{
		//캐릭터의 위치에서 y값을 +1 조정해서 캐릭터중간을 포커스로 둠
		focus = focusObj.transform.position;
		focus.y += 1.0f;

		if (mousePos.magnitude > Vector3.kEpsilon)
			cameraRotate(mousePos);

		return;
	}

}
