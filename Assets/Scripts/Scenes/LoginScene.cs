using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Login;
        Managers.UI.m_login = Managers.UI.ShowPopupUI<UI_Login>();
    }

    public void TurnScene()
    {
        Managers.Scene.LoadScene(Define.Scene.Lobby);
    }

    public override void Clear()
    {
        Managers.UI.ClosePopupUI();
    }
}
