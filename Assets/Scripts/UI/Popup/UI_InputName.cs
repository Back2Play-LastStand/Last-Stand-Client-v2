using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UI_InputName : UI_Popup
{
    enum GameObjects
    {
        InputName,
        NameButton,
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        GetObject((int)GameObjects.NameButton).AddUIEvent((PointEventData) => { StartCoroutine(PostPlayerName()); });
    }

    public IEnumerator PostPlayerName()
    {
        string playerId = Managers.UI.m_login.PlayerId;
        string playerName = GetObject((int)GameObjects.InputName).GetComponent<InputField>().text;

        string sessionId = PlayerPrefs.GetString("SESSION_ID");
        string url = WebRequestManager.Instance.serverConn.PostPlayerNameUrl;

        var requestData = new PlayerDataRequest
        {
            PlayerId = playerId,
            PlayerName = playerName
        };
        string json = JsonUtility.ToJson(requestData);

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Session-Id", sessionId);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Player created: " + request.downloadHandler.text);
            StartCoroutine(Managers.UI.GetPlayerName(playerId, (playerName) =>
            {
                ClosePopupUI();
                // 첫 로그인이면 보내기
                Protocol.REQ_ENTER pkt = new()
                {
                    Name = Managers.UI.m_playerName,
                };
                Managers.Network.Send(pkt, (ushort)PacketId.PKT_REQ_ENTER);
            }));
        }
        else
        {
            Debug.LogError($"Error: {request.responseCode} - {request.downloadHandler.text}");

            if (request.responseCode == 401)
            {
                Debug.LogWarning("Session expired. Please log in again.");
                PlayerPrefs.DeleteKey("SESSION_ID");
                // 로그인 씬 이동 처리 (예: SceneManager.LoadScene("LoginScene"))
            }
        }
    }
}

[System.Serializable]
public class PlayerDataRequest
{
    public string PlayerId; 
    public string PlayerName;
}
