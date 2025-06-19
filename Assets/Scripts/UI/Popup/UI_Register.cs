using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Register : UI_Popup
{
    enum GameObjects
    {
        InputId,
        InputPassword,
        InputEmail,
        RegisterButton,
        BackButton,
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        GetObject((int)GameObjects.RegisterButton).AddUIEvent((PointerEventData) => { Register((success, message) => { if (success) ClosePopupUI(); }); });
        GetObject((int)GameObjects.BackButton).AddUIEvent((PointerEventData) => { ClosePopupUI(); });
    }

    public void Register(System.Action<bool, string> callback)
    {
        string playerId = GetObject((int)GameObjects.InputId).GetComponent<InputField>().text;
        string password = GetObject((int)GameObjects.InputPassword).GetComponent<InputField>().text;
        string email = GetObject((int)GameObjects.InputEmail).GetComponent<InputField>().text;

        if (string.IsNullOrEmpty(playerId) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email))
        {
            callback(false, "필드를 입력하세요.");
            return;
        }

        var request = new RegisterRequest
        {
            playerId = playerId,
            password = password,
            email = email,
        };

        string json = JsonUtility.ToJson(request);

        StartCoroutine(WebRequestManager.Instance.PostRequest(WebRequestManager.Instance.serverConn.RegisterUrl, json,
            onSuccess: (response) =>
            {
                var res = JsonUtility.FromJson<RegisterResponse>(response);
                if (res == null)
                {
                    Debug.LogError("RegisterResponse 파싱 실패");
                    callback(false, "파싱 실패");
                    return;
                }

                if (string.IsNullOrEmpty(res.playerId))
                {
                    callback(false, res.message ?? "회원가입 실패");
                }
                else
                {
                    callback(true, res.message ?? "회원가입 성공");
                }
            },
            onError: (error) => callback(false, error)
        ));
    }
}

[System.Serializable]
public class RegisterRequest
{
    public string playerId;
    public string password;
    public string email;
}

[System.Serializable]
public class RegisterResponse
{
    public string playerId;
    public string email;
    public string message;
}