using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
	const float MinZoom = 1.0f;
	const float MaxZoom = 8.0f;

	//Define.MouseButtonDown mouseButtonDown = Define.MouseButtonDown.MBD_LEFT;

	protected Vector3 focus;
	protected GameObject focusObj;
	protected Vector3 oldPos;
	protected Vector3 dumpPos;
	/// <summary>
	/// 플레이어의 CameraArm & 축
	/// </summary>
	protected GameObject focusAxis;

	protected void setupFocusObject(string name)
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

		transform.position += new Vector3(0,3.0f,0);

		//chasePlayer() 함수를 위한 초기화
		dumpPos = focusObj.transform.position;

        return;
	}
    
	void Update()
    {
		focusAxis.transform.localRotation = Camera.main.transform.localRotation;
    }
    void LateUpdate()
	{

		this.cameraCorrection();
		this.chasePlayer();
		this.mouseEvent();
		return;
	}

	protected void chasePlayer()
    {
		transform.LookAt(focus);

        if (focusObj.transform.position != dumpPos)	//캐릭터가 움직이면 따라 움직임
        {
			dumpPos -= focusObj.transform.position;

			transform.position -= dumpPos;

			dumpPos = focusObj.transform.position;
        }
	}

	protected virtual void mouseEvent()
	{
		//확대&축소
		float delta = Input.GetAxis("Mouse ScrollWheel");	
		if (delta != 0.0f)
			this.mouseWheelEvent(delta);

		//마우스 드래그 이벤트
		this.mouseDragEvent(Input.mousePosition);

		return;
	}

	protected virtual void mouseDragEvent(Vector3 mousePos)
	{
		Vector3 diff = mousePos - oldPos;

		//캐릭터의 위치에서 y값을 +1 조정해서 캐릭터중간을 포커스로 둠
		this.focus = this.focusObj.transform.position;
		this.focus.y += 1.0f;

		if (Input.GetMouseButton((int)Define.MouseButtonDown.MBD_LEFT))
		{
			
		}
		else if (Input.GetMouseButton((int)Define.MouseButtonDown.MBD_RIGHT))
		{
			if (diff.magnitude > Vector3.kEpsilon)
				this.cameraRotate(diff);
		}

		this.oldPos = mousePos;

		return;
	}

	protected void mouseWheelEvent(float delta)
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

	protected void cameraRotate(Vector3 diff)
	{
		if (diff.y < -5.0f || 5.0f < diff.y)
        {
			if (diff.y < -5.0f)
				diff.y = -5.0f;
			if (5.0f < diff.y)
				diff.y = 5.0f;
        }

		//좌우 회전
		transform.RotateAround(focusAxis.transform.position, Vector3.up, diff.x);

		//카메라 회전값을 캐릭터축 회전값에 대입
		focusAxis.transform.localRotation = Camera.main.transform.localRotation;

		//캐릭터를 시작점, 카메라가 바라보는 방향의 오른쪽을 끝점으로 선을 그었을 때 그 선이 축이 된다.
		Debug.DrawLine(focusAxis.transform.localPosition,focusAxis.transform.localRotation * Vector3.right);

        //카메라의 Rotation.x 가 70도 & -70도로 제한되게 함
        if (Camera.main.transform.localRotation.eulerAngles.x < 70.0f || 355.0f < Camera.main.transform.localRotation.eulerAngles.x)			// -70 ~ 70	(정상범위)
			transform.RotateAround(focusAxis.transform.position, focusAxis.transform.localRotation * Vector3.right, -diff.y);

		//diff.x 값이 양수 = 오른쪽으로 드래그
		//diff.x 값이 음수 = 왼쪽으로 드래그
		//diff.y 값이 양수 = 위로 드래그
		//diff.y 값이 음수 = 아래로 드래그

		return;
	}

	protected void cameraCorrection()
    {

		if (70.0f <= Camera.main.transform.localRotation.eulerAngles.x && Camera.main.transform.localRotation.eulerAngles.x <= 90.0f)       // 70 ~ 90	(위로 너무 올라감)
			transform.RotateAround(focusAxis.transform.position, focusAxis.transform.localRotation * Vector3.right, -0.4f);

		if (270.0f <= Camera.main.transform.localRotation.eulerAngles.x && Camera.main.transform.localRotation.eulerAngles.x <= 355.0f) // -90 ~ -70 (아래로 너무 내려감)
			transform.RotateAround(focusAxis.transform.position, focusAxis.transform.localRotation * Vector3.right, 0.4f);
	}

}
