using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UI_Base : MonoBehaviour
{
	protected Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();	//딕셔너리<자료형, Enum의 내용물들>

	public abstract void Init();

	protected void Bind<T>(Type type) where T : UnityEngine.Object
	{
		string[] names = Enum.GetNames(type);														// type의 값의 이름을 string[] 배열에 저장
		UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];	// 타입명의 길이만큼 objects에 동적할당
		_objects.Add(typeof(T), objects);																	// 타입 - 자료 맵을 Dictionary에 추가

		//자식을 objects에 추가하는 작업
		for (int i = 0; i < names.Length; i++)
		{
			if (typeof(T) == typeof(GameObject))														// Bind<GameObject> 일 때,
				objects[i] = Util.FindChild(gameObject, names[i], true);
			else																												// GameObject타입이 아닐 때,
				objects[i] = Util.FindChild<T>(gameObject, names[i], true);		

			if (objects[i] == null)																					// 해당하는 값이 없을 때,
				Debug.Log($"Failed to bind({names[i]})");
		}
	}

	protected T Get<T>(int idx) where T : UnityEngine.Object
	{
		UnityEngine.Object[] objects = null;
		if (_objects.TryGetValue(typeof(T), out objects) == false)		// objects 변수에 T 타입을 키값으로 받아오기에 실패하면
			return null;												// null값을 반환한다.

		return objects[idx] as T;										// T 타입을 키값으로 받아오기에 성공했을 때 value들반환
	}

	protected GameObject GetObject(int idx) { return Get<GameObject>(idx); }
	protected Text GetText(int idx) { return Get<Text>(idx); }
	protected Button GetButton(int idx) { return Get<Button>(idx); }
	protected Image GetImage(int idx) { return Get<Image>(idx); }

	public static void BindEvent(GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)
	{
		UI_EventHandler evt = Util.GetOrAddComponent<UI_EventHandler>(go);

		switch (type)
		{
			case Define.UIEvent.Click:
				evt.OnClickHandler -= action;
				evt.OnClickHandler += action;
				break;
			case Define.UIEvent.Drag:
				evt.OnDragHandler -= action;
				evt.OnDragHandler += action;
				break;
		}
	}
}
