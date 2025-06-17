using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class WebRequestManager : MonoBehaviour
{
    public static WebRequestManager Instance { get; private set; }
    public ServerConnection serverConn;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator PostRequest(string url, string jsonData, System.Action<string> onSuccess, System.Action<string> onError)
    {
        using UnityWebRequest request = new(url, "POST");
        byte[] body = Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(body);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            onSuccess?.Invoke(request.downloadHandler.text);
        }
        else
        {
            onError?.Invoke(request.error);
        }    
    }

    public IEnumerator GetRequest(string url, System.Action<string> onSuccess, System.Action<string> onError)
    {
        using UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            onSuccess(request.downloadHandler.text);
        }
        else
        {
            onError?.Invoke(request.error);
        }
    }
}
