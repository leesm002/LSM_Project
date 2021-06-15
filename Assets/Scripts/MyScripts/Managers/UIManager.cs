using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    int _order = 10;
    private int Icount = 0;

    Stack<UI_Popup> _popupStack = new Stack<UI_Popup>();
    Stack<UI_Popup> _popupStackDump = new Stack<UI_Popup>();
    UI_Scene _sceneUI = null;

    public GameObject Root
    {
        get
        {
			GameObject root = GameObject.Find("@UI_Root");
			if (root == null)
				root = new GameObject { name = "@UI_Root" };
            return root;
		}
    }

    public void SetCanvas(GameObject go, bool sort = true)
    {
        Canvas canvas = Util.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        if (sort)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }

    public T MakeWorldSpaceUI<T>(Transform parent = null, string name = null) where T : UI_Base
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.GetResourceManager.Instantiate($"UI/WorldSpaceUI/{name}");
        if (parent != null)
            go.transform.SetParent(parent);

        Canvas canvas = go.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.worldCamera = Camera.main;

        return Util.GetOrAddComponent<T>(go);
    }

    public T MakeSubItem<T>(Transform parent = null, string name = null) where T : UI_Base
	{
		if (string.IsNullOrEmpty(name))
			name = typeof(T).Name;

		GameObject go = Managers.GetResourceManager.Instantiate($"UI/SubItem/{name}");
		if (parent != null)
			go.transform.SetParent(parent);

		return Util.GetOrAddComponent<T>(go);
	}

	public T ShowSceneUI<T>(string name = null) where T : UI_Scene
	{
		if (string.IsNullOrEmpty(name))
			name = typeof(T).Name;

		GameObject go = Managers.GetResourceManager.Instantiate($"UI/Scene/{name}");
		T sceneUI = Util.GetOrAddComponent<T>(go);
        _sceneUI = sceneUI;

		go.transform.SetParent(Root.transform);

		return sceneUI;
	}

	public T ShowPopupUI<T>(string name = null) where T : UI_Popup
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.GetResourceManager.Instantiate($"UI/Popup/{name}");
        T popup = Util.GetOrAddComponent<T>(go);
        _popupStack.Push(popup);

        go.transform.SetParent(Root.transform);

		return popup;
    }


    public void ClosePopupUI(UI_Popup popup)
    {
		if (_popupStack.Count == 0)
			return;

        if (_popupStack.Peek() != popup)            // 스택 가장 위에 원하는 Popup창이 있지 않을 때
        {
            Icount = _popupStack.Count;
            for (int i = 0; i < Icount; i++)
            {
                UI_Popup dumpPop = _popupStack.Pop();
                _order--;

                if (dumpPop != popup)               // 원하는 Popup창이 아닐때
                {
                    _popupStackDump.Push(dumpPop);  // 덤프스택에 저장해둠
                }
                else
                {
                    Managers.GetResourceManager.Destroy(dumpPop.gameObject);    //원하는 Popup창 일때 해당 창 제거
                    dumpPop = null;
                    _order--;
                    
                    int IcountDump = _popupStackDump.Count;                     // 저장해 둔 덤프스택을 그대로 가져옴
                    for (int o = 0; o < IcountDump; o++)
                    {
                        _popupStack.Push(_popupStackDump.Pop());
                        _order++;
                    }
                }
            }
            return;
        }
        else
        {
            ClosePopupUI();                 // 스택 가장 위에 원하는 Popup창이 있을 때 그 창을 닫음
        }
                                                    
    }

    public void ClosePopupUI()
    {
        if (_popupStack.Count == 0)
            return;

        UI_Popup popup = _popupStack.Pop();
        Managers.GetResourceManager.Destroy(popup.gameObject);
        popup = null;
        _order--;
        return;
    }

    public void CloseAllPopupUI()
    {
        while (_popupStack.Count > 0)
        {
            ClosePopupUI();
        }

        return;
    }
}
