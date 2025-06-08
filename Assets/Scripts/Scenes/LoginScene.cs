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
        Managers.UI.ShowSceneUI<UI_Login>();
    }

    public void TurnGameScene()
    {
        Managers.Scene.LoadScene(Define.Scene.Game);
    }

    public override void Clear()
    {

    }
}
