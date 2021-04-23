﻿using System.Collections;
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

	void setupFocusObject(string name)
	{
		this.focusObj = new GameObject(name);

		return;
	}

	void Start()
	{
		focusObj = GameObject.Find("Player");

		if (this.focusObj == null)
			this.setupFocusObject("Player");

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
		transform.LookAt(focusObj.transform.position);
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
		this.focus.y += 2.0f;

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

		return;
	}

	public void cameraRotate(Vector3 diff)
	{
		transform.RotateAround(focusObj.transform.position, Vector3.up, diff.x);


		//Transform focusTrans = this.transform;
		//focusTrans.localEulerAngles = focusTrans.localEulerAngles + eulerAngle;

		//this.transform.LookAt(this.focus);

		return;
	}
	

}
