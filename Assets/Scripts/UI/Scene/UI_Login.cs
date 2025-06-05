using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Login : UI_Scene
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

        GetObject((int)GameObjects.LoginButton).AddUIEvent((PointerEventData) => { OnClickLoginButton(PointerEventData); });
        GetObject((int)GameObjects.JoinButton).AddUIEvent((PointerEventData) => { OnClickJoinButton(PointerEventData); });
    }

    public void OnClickLoginButton(PointerEventData evt)
    {
        string account = GetObject((int)GameObjects.InputId).GetComponent<InputField>().text;
        string password = GetObject((int)GameObjects.InputPassword).GetComponent<InputField>().text;
        string url = "http://localhost:3333/login";

        var res = PostLoginAsync(url, account, password);
    }
    public void OnClickJoinButton(PointerEventData evt)
    {
        string account = GetObject((int)GameObjects.InputId).GetComponent<InputField>().text;
        string password = GetObject((int)GameObjects.InputPassword).GetComponent<InputField>().text;
        string url = "http://localhost:3333/join";

        var res = PostJoinAsync(url, account, password);
    }
    
    static readonly HttpClient client = new HttpClient();

    public async Task<string> PostLoginAsync(string url, string account, string password)
    {
        string body = $"{account}&{password}";
        var content = new StringContent(body, Encoding.UTF8);

        HttpResponseMessage response = await client.PostAsync(url, content);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }
    public async Task<string> PostJoinAsync(string url, string account, string password)
    {
        string body = $"{account}&{password}";
        var content = new StringContent(body, Encoding.UTF8);

        HttpResponseMessage response = await client.PostAsync(url, content);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }
}
