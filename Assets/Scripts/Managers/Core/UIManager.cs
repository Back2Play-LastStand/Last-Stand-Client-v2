using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class PlayerNameResponse
{
    public string PlayerName;
}

public class UIManager
{
    int m_order = 10;

    Stack<UI_Popup> m_popupStack = new();
    UI_Scene m_sceneUI = null;

    public UI_Interface m_Interface;
    public UI_Lobby m_lobby;
    public UI_Login m_login;

    public string m_playerName;
    public bool m_isNewAccount = false;

    public void Init()
    {
    }

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

    public IEnumerator GetPlayerName(string playerId)
    {
        string sessionId = PlayerPrefs.GetString("SESSION_ID");
        string url = WebRequestManager.Instance.serverConn.GetPlayerNameUrl(playerId);

        UnityWebRequest request = UnityWebRequest.Get(url);
        request.SetRequestHeader("Session-Id", sessionId);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Player name response: " + request.downloadHandler.text);
            
            PlayerNameResponse res = JsonUtility.FromJson<PlayerNameResponse>(request.downloadHandler.text);
            m_playerName = res.PlayerName;
        }
        else
        {
            Debug.LogError($"Error: {request.responseCode} - {request.downloadHandler.text}");

            if (request.responseCode == 401)
            {
                Debug.LogWarning("Session expired. Please log in again.");
                PlayerPrefs.DeleteKey("SESSION_ID");
                // 로그인 화면으로 이동 처리 (예: SceneManager.LoadScene("LoginScene"))
            }
        }
    }

    public void SetCanvas(GameObject go, bool sort = true)
    {
        var canvas = Util.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        if (sort)
        {
            canvas.sortingOrder = m_order;
            m_order++;
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

        GameObject go = Managers.Resource.Instantiate($"UI/WorldSpace/{name}");
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

        GameObject go = Managers.Resource.Instantiate($"UI/SubItem/{name}");
        if (parent != null)
            go.transform.SetParent(parent);

        return Util.GetOrAddComponent<T>(go);
    }

    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/Scene/{name}");
        T sceneUI = Util.GetOrAddComponent<T>(go);
        m_sceneUI = sceneUI;

        go.transform.SetParent(Root.transform);

        return sceneUI;
    }

    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/Popup/{name}");
        T popup = Util.GetOrAddComponent<T>(go);
        m_popupStack.Push(popup);

        go.transform.SetParent(Root.transform);

        return popup;
    }

    public void ClosePopupUI(UI_Popup popup)
    {
        if (m_popupStack.Count == 0)
            return;

        if (m_popupStack.Peek() != popup)
        {
            Debug.Log("Close Popup Failed");
            return;
        }

        ClosePopupUI();
    }

    public void ClosePopupUI()
    {
        if (m_popupStack.Count == 0)
            return;

        UI_Popup popup = m_popupStack.Pop();
        Managers.Resource.Destroy(popup.gameObject);
        popup = null;
        m_order--;
    }

    public void CloseAllPopupUI()
    {
        while (m_popupStack.Count > 0)
            ClosePopupUI();
    }
}
