using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_FindId : UI_Popup
{
    enum GameObjects
    {
        InputEmail,
        InputVerify,
        VerifyButton,
        SendButton,
        FindPassword,
        BackButton
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        GetObject((int)GameObjects.SendButton).AddUIEvent((PointerEventData) => { SendVerificationEmail(); });
        GetObject((int)GameObjects.VerifyButton).AddUIEvent((PointerEventData) => { SendVerifyCode(); });
        GetObject((int)GameObjects.FindPassword).AddUIEvent((PointerEventData) => { Managers.UI.ShowPopupUI<UI_FindPassword>(); });
        GetObject((int)GameObjects.BackButton).AddUIEvent((PointerEventData) => { ClosePopupUI(); });
    }

    public void SendVerificationEmail()
    {
        string email = GetObject((int)GameObjects.InputEmail).GetComponent<InputField>().text;
        string url = WebRequestManager.Instance.serverConn.GetVerifyEmailUrl(email);

        StartCoroutine(WebRequestManager.Instance.PostRequest(
                url,
                "",
                onSuccess: (response) =>
                {
                    Debug.Log("인증 이메일 전송 성공: " + response);
                },
                onError: (error) =>
                {
                    Debug.LogError(":x: 이메일 전송 실패: " + error);
                }));
    }

    public void SendVerifyCode()
    {
        string email = GetObject((int)GameObjects.InputEmail).GetComponent<InputField>().text;
        string code = GetObject((int)GameObjects.InputVerify).GetComponent<InputField>().text;

        VerifyCodeRequest req = new VerifyCodeRequest
        {
            email = email,
            code = code.Trim()
        };

        string json = JsonUtility.ToJson(req);

        StartCoroutine(WebRequestManager.Instance.PostRequest(
            WebRequestManager.Instance.serverConn.VerifyCodeUrl,
            json,
            onSuccess: (response) =>
            {
                Debug.Log("인증 성공: " + response);
            },
            onError: (error) =>
            {
                Debug.LogError("인증 실패: " + error);
            }));
    }
}


[System.Serializable]
public class VerifyCodeRequest
{
    public string email;
    public string code;
}