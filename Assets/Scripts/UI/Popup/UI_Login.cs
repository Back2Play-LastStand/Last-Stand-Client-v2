using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Login : UI_Popup
{
    enum GameObjects
    {
        InputId,
        IdPlaceholder,
        InputPassword,
        PasswordPlaceholder,
        LoginButton,
        JoinButton,
    }

    enum Texts
    {
        IdText,
        PasswordText,
        LoginText,
        JoinText,
        FindText,
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));
        Bind<Text>(typeof(Texts));

        var login = Managers.Scene.CurrentScene.GetComponent<LoginScene>();
        GetObject((int)GameObjects.LoginButton).AddUIEvent((PointerEventData) => { LoginReq((success, message) => { if (success) login.TurnScene(); }); });
        GetObject((int)GameObjects.JoinButton).AddUIEvent((PointerEventData) => { Managers.UI.ShowPopupUI<UI_Register>(); });
        GetText((int)Texts.FindText).gameObject.AddUIEvent((PointerEventData) => { Managers.UI.ShowPopupUI<UI_FindId>(); });
    }

    public string PlayerId;

    public void LoginReq(System.Action<bool, string> callback)
    {
        PlayerId = GetObject((int)GameObjects.InputId).GetComponent<InputField>().text;
        string password = GetObject((int)GameObjects.InputPassword).GetComponent<InputField>().text;

        LoginRequest request = new LoginRequest
        {
            playerId = PlayerId,
            password = password
        };

        string json = JsonUtility.ToJson(request);

        StartCoroutine(WebRequestManager.Instance.PostRequest(WebRequestManager.Instance.serverConn.LoginUrl, json,
            onSuccess: (response) =>
            {
                LoginResponse res = JsonUtility.FromJson<LoginResponse>(response);

                if (res == null)
                {
                    Debug.LogError("LoginResponse 파싱 실패");
                    callback(false, "파싱 실패");
                    return;
                }

                if (string.IsNullOrEmpty(res.sessionId))
                {
                    Debug.LogError("세션이 비어있음: " + res.message);
                    callback(false, res.message);
                    return;
                }

                PlayerPrefs.SetString("SESSION_ID", res.sessionId);
                PlayerPrefs.SetString("USER_ID", res.playerId);
                Managers.UI.m_isNewAccount = res.isNewAccount;

                callback(true, response);
            },
            onError: (error) => callback(false, error)
            ));
    }
}

[System.Serializable]
public class LoginRequest
{
    public string playerId;
    public string password;
}

[System.Serializable]
public class LoginResponse
{
    public string playerId;
    public string sessionId;
    public bool isNewAccount;
    public string message;
}
