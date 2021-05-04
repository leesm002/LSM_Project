using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
	const float MinZoom = 1.0f;
	const float MaxZoom = 8.0f;

	enum MouseButtonDown
	{
		MBD_LEFT = 0,
		MBD_RIGHT,
	};

	
	private Vector3 focus;
	private GameObject focusObj;
	private Vector3 oldPos;
	private Vector3 dumpPos;
	private GameObject focusAxis;

	void setupFocusObject(string name)
	{
		this.focusObj = new GameObject(name);

		return;
	}

	void Start()
	{
		focusObj = GameObject.Find("Player");
		focusAxis = GameObject.Find("PlayerAxis");

		if (this.focusObj == null)
			this.setupFocusObject("Player");

		if (this.focusAxis == null)
			this.setupFocusObject("PlayerAxis");

		//chasePlayer() 함수를 위한 초기화
		dumpPos = focusObj.transform.position;

        return;
	}

	void LateUpdate()
	{
		this.chasePlayer();
		this.mouseEvent();

		return;
	}

	void chasePlayer()
    {
		transform.LookAt(focus);

        if (focusObj.transform.position != dumpPos)	//캐릭터가 움직이면 따라 움직임
        {
			dumpPos -= focusObj.transform.position;

			transform.position -= dumpPos;

			dumpPos = focusObj.transform.position;
        }
	}

	void mouseEvent()
	{
		//확대&축소
		float delta = Input.GetAxis("Mouse ScrollWheel");	
		if (delta != 0.0f)
			this.mouseWheelEvent(delta);

		//마우스 클릭 이벤트
		if (Input.GetMouseButtonDown((int)MouseButtonDown.MBD_LEFT) ||
			Input.GetMouseButtonDown((int)MouseButtonDown.MBD_RIGHT))
			this.oldPos = Input.mousePosition;

		//마우스 드래그 이벤트
		this.mouseDragEvent(Input.mousePosition);

		return;
	}

	void mouseDragEvent(Vector3 mousePos)
	{
		Vector3 diff = mousePos - oldPos;

		//캐릭터의 위치에서 y값을 +1 조정해서 캐릭터중간을 포커스로 둠
		this.focus = this.focusObj.transform.position;
		this.focus.y += 1.0f;

		if (Input.GetMouseButton((int)MouseButtonDown.MBD_LEFT))
		{
			
		}
		else if (Input.GetMouseButton((int)MouseButtonDown.MBD_RIGHT))
		{
			if (diff.magnitude > Vector3.kEpsilon)
				this.cameraRotate(diff);
		}

		this.oldPos = mousePos;

		return;
	}

	public void mouseWheelEvent(float delta)
	{
		//delta 값	: 마우스 휠 위로 0.1 / 마우스 휠 아래로 -0.1
		//			: 확대 0.1 / 축소 -0.1

		Vector3 focusToPosition = this.transform.position - this.focus;

		Vector3 post = focusToPosition * (1.0f - delta);

		if (post.magnitude > MinZoom && post.magnitude < MaxZoom)
			this.transform.position = this.focus + post;

        if (post.magnitude < MinZoom)		//만약 너무 가까우면 최소값까지 축소
            while (post.magnitude > MinZoom)
				this.transform.position = this.focus + (focusToPosition * (1.0f + 0.1f));
		if (post.magnitude > MaxZoom)       //만약 너무 멀면 최댓값까지 확대
			while (post.magnitude < MinZoom)
				this.transform.position = this.focus + (focusToPosition * (1.0f - 0.1f));


		return;
	}

	public void cameraRotate(Vector3 diff)
	{
		transform.RotateAround(focusAxis.transform.position, Vector3.up, diff.x);

		focusAxis.transform.localRotation = Camera.main.transform.localRotation;

		Debug.Log(Mathf.Clamp(Camera.main.transform.localRotation.x,0.0f,70.0f));
		
		//캐릭터를 시작점, 카메라가 바라보는 방향의 오른쪽을 끝점으로 선을 그었을 때 그 선이 축이 된다.
		Debug.DrawLine(focusAxis.transform.localPosition,focusAxis.transform.localRotation * Vector3.right);

		transform.RotateAround(focusAxis.transform.position, focusAxis.transform.localRotation * Vector3.right, -diff.y);
		
		//transform.RotateAround(회전할 기준 좌표, 회전할 기준 축, 회전할 각도);
		//transform.RotateAround(focusObj.transform.position, Vector3.zero, -diff.y);
        
		/*
			0.0f < rotation.y && rotation.y < 90.0f			오른쪽앞 방향		↗
			90.0f < rotation.y && rotation.y < 180.0f		오른쪽뒤 방향		↘
			0.0f > rotation.y && -90.0f > rotation.y		왼쪽앞 방향		↖
			-90.0f > rotation.y && -180.0f > rotation.y		왼쪽뒤 방향		↙

			0			앞 방향
			90			오른쪽 방향
			180 && -180	뒤 방향
			-90			왼쪽 방향
		 */

		//diff.x 값이 양수 = 오른쪽으로 드래그
		//diff.x 값이 음수 = 왼쪽으로 드래그
		//diff.y 값이 양수 = 위로 드래그
		//diff.y 값이 음수 = 아래로 드래그

		//Transform focusTrans = this.transform;
		//focusTrans.localEulerAngles = focusTrans.localEulerAngles + eulerAngle;

		//this.transform.LookAt(this.focus);

		return;
	}

}
