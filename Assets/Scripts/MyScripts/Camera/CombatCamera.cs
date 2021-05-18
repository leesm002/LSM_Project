using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatCamera : CameraManager
{
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
		Cursor.lockState = CursorLockMode.Confined;
    }

	override protected void mouseDragEvent(Vector3 mousePos)
	{
		Vector3 diff = mousePos - oldPos;

		//캐릭터의 위치에서 y값을 +1 조정해서 캐릭터중간을 포커스로 둠
		focus = focusObj.transform.position;
		focus.y += 1.0f;

		if (diff.magnitude > Vector3.kEpsilon)
			cameraRotate(diff);

		oldPos = mousePos;

		return;
	}

}
